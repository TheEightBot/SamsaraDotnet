#!/usr/bin/env python3
"""Render the Samsara spec-drift report and the GitHub issue body.

Consumes the JSON emitted by the four checkers and produces:

  --full-out   the complete Markdown report (uploaded as a workflow artifact)
  --issue-out  a size-capped version for the GitHub issue body

The issue body is the hand-off artifact: it must contain everything an
implementer -- the Copilot coding agent, a local agent, or a human -- needs to
close the drift in one PR, without reading the workflow logs. Anything that does
not fit the size cap is truncated with an explicit "(+N more ...)" marker so a
reader can never mistake a truncated list for a complete one.

Usage:
  python3 tools/render-drift-report.py \
      --summary api-sync-summary.json \
      --sdk-sync sdk-sync.json \
      --fabrication fabrication.json \
      --model-sync model-sync.json \
      --run-url https://github.com/owner/repo/actions/runs/123 \
      --full-out drift-report.md --issue-out issue-body.md
"""

from __future__ import annotations

import argparse
import json
from collections import defaultdict
from pathlib import Path

BASELINE_REL = ".github/cache/samsara-api-baseline.json"

# Model findings rendered inline in the issue body. The rest go to the artifact.
# Keeps the issue readable and stops the byte-level truncation from eating the
# closing instructions' budget.
MAX_MODEL_FINDINGS = 60


def load(path: str | None) -> dict:
    if not path:
        return {}
    p = Path(path)
    if not p.exists():
        return {}
    try:
        return json.loads(p.read_text())
    except json.JSONDecodeError:
        return {}


def _model_findings(model: dict) -> list[dict]:
    """Normalise the model-sync payload into a flat list of active findings.

    Tolerates a few plausible key layouts so a shape tweak in the checker
    degrades this report rather than breaking the workflow.
    """
    for key in ("active", "findings", "active_findings"):
        val = model.get(key)
        if isinstance(val, list):
            return [f for f in val if isinstance(f, dict)]
    return []


def _count(val) -> str:
    """Render a checker field that may be a list of items or an already-counted int."""
    if val is None:
        return "?"
    if isinstance(val, (list, tuple, dict)):
        return str(len(val))
    return str(val)


def _sev(f: dict) -> str:
    return str(f.get("severity") or f.get("sev") or "UNKNOWN").upper()


def _fmt_finding(f: dict) -> str:
    sev = _sev(f)
    ftype = f.get("ftype") or f.get("type") or "finding"
    rec = f.get("sdk_type") or f.get("record") or "?"
    prop = f.get("prop") or f.get("property") or "*"
    detail = (f.get("detail") or "").strip()
    eps = f.get("endpoints") or []
    line = f"- `[{sev}]` **{ftype}** — `{rec}.{prop}`"
    if detail:
        line += f" — {detail}"
    if eps:
        shown = ", ".join(str(e) for e in eps[:3])
        more = f" (+{len(eps) - 3} more)" if len(eps) > 3 else ""
        line += f"\n  - seen on: {shown}{more}"
    return line


def _section(title: str, lines: list[str], empty: str = "_none_") -> list[str]:
    out = [f"### {title}", ""]
    out.extend(lines if lines else [empty])
    out.append("")
    return out


def build_report(
    summary: dict, sdk: dict, fab: dict, model: dict, run_url: str,
    max_model_findings: int | None = None,
) -> tuple[list[str], list[str], list[str]]:
    """Return (header, body, footer). Only the body may be truncated.

    ``max_model_findings`` caps the inline finding list for the ISSUE body. The
    full artifact report passes None so it always carries every finding — it is
    the thing the issue points at when it says "not exhaustive".
    """
    counts = summary.get("counts", {})
    old_fp = summary.get("old_fingerprint", {})
    new_fp = summary.get("new_fingerprint", {})
    eps = summary.get("endpoints", {})
    schemas = summary.get("schemas", {})

    header = [
        "## Samsara API drift — implementation spec",
        "",
        f"**Baseline** `{summary.get('old_version', '?')}` · content `{old_fp.get('hash', '?')}` "
        f"({old_fp.get('ops', '?')} ops, {old_fp.get('schemas', '?')} schemas)  ",
        f"**Live** `{summary.get('new_version', '?')}` · content `{new_fp.get('hash', '?')}` "
        f"({new_fp.get('ops', '?')} ops, {new_fp.get('schemas', '?')} schemas)  ",
        f"**Detected** {summary.get('generated', '?')}"
        + (f" · [workflow run]({run_url})" if run_url else ""),
        "",
        f"**Classification:** {summary.get('classification', '?')} — "
        f"{counts.get('added', 0)} new / {counts.get('removed', 0)} removed / "
        f"{counts.get('changed', 0)} changed operations; "
        f"{counts.get('schemas_added', 0)} schemas added, "
        f"{counts.get('schemas_removed', 0)} removed, "
        f"{counts.get('schemas_changed', 0)} with property/required deltas.",
        "",
    ]
    if summary.get("old_version") == summary.get("new_version"):
        header += [
            "> ⚠️ `info.version` did **not** change. Samsara mutates the spec in place, so "
            "version-keyed checks under-trigger; the content hash above is the real signal.",
            "",
        ]

    sections: list[str] = []

    # ---- 1. endpoint diff -------------------------------------------------
    sections += ["---", "", "## 1. Endpoint diff", ""]

    by_tag: dict[str, list[dict]] = defaultdict(list)
    for e in eps.get("added", []):
        by_tag[(e.get("tags") or ["(untagged)"])[0]].append(e)
    added_lines: list[str] = []
    for tag in sorted(by_tag):
        added_lines.append(f"**{tag}** ({len(by_tag[tag])})")
        added_lines.append("")
        for e in by_tag[tag]:
            dep = " `[DEPRECATED]`" if e.get("deprecated") else ""
            s = f" — {e['summary']}" if e.get("summary") else ""
            added_lines.append(f"- `{e['key']}` (`{e.get('operationId', '')}`){dep}{s}")
        added_lines.append("")
    sections += _section(f"New operations ({counts.get('added', 0)})", added_lines)

    sections += _section(
        f"Removed operations ({counts.get('removed', 0)})",
        [f"- `{e['key']}` (`{e.get('operationId', '')}`)" for e in eps.get("removed", [])],
    )

    changed_lines: list[str] = []
    for e in eps.get("changed", []):
        changed_lines.append(f"- `{e['key']}` (`{e.get('operationId', '')}`)")
        for d in e.get("details", []):
            changed_lines.append(f"  - {d}")
    sections += _section(f"Changed operations ({counts.get('changed', 0)})", changed_lines)

    if eps.get("deprecated_added"):
        sections += _section(
            f"Newly deprecated ({len(eps['deprecated_added'])})",
            [f"- `{k}`" for k in eps["deprecated_added"]],
        )

    schema_lines: list[str] = []
    for name, delta in sorted((schemas.get("changed") or {}).items()):
        parts = []
        if delta.get("added_props"):
            parts.append("+props " + ", ".join(f"`{p}`" for p in delta["added_props"]))
        if delta.get("removed_props"):
            parts.append("−props " + ", ".join(f"`{p}`" for p in delta["removed_props"]))
        if delta.get("added_required"):
            parts.append("+required " + ", ".join(f"`{p}`" for p in delta["added_required"]))
        if delta.get("removed_required"):
            parts.append("−required " + ", ".join(f"`{p}`" for p in delta["removed_required"]))
        schema_lines.append(f"- `{name}`: " + "; ".join(parts))
    sections += _section(
        f"Schema property deltas ({counts.get('schemas_changed', 0)})",
        schema_lines,
    )
    sections += [
        f"_Added ({counts.get('schemas_added', 0)}) and removed ({counts.get('schemas_removed', 0)}) "
        "schema **names** are listed in the run artifact only — they are mostly per-operation "
        "request/response envelopes._",
        "",
    ]

    # ---- 2. SDK state -----------------------------------------------------
    sections += ["---", "", "## 2. SDK state vs the live spec", ""]
    gate = model.get("gate", {}) or {}
    sev_counts = model.get("severity_counts") or model.get("by_severity") or {}
    if not sev_counts:
        # Fall back to counting the findings list, so a key rename in the checker
        # degrades this line rather than silently printing an empty summary.
        tally: dict[str, int] = defaultdict(int)
        for f in _model_findings(model):
            tally[_sev(f)] += 1
        sev_counts = dict(tally)
    model_line = (
        " · ".join(f"{k} {v}" for k, v in sev_counts.items())
        if sev_counts
        else "_not run_"
    )
    if gate:
        model_line += (
            f" · gate `{gate.get('threshold')}` → "
            f"{'FAILED' if gate.get('failed') else 'passed'}"
        )
    sections += [
        f"- `check-sdk-sync`: matched **{sdk.get('matched', '?')}** / "
        f"{sdk.get('spec_operations', '?')} spec ops · "
        f"**{sdk.get('missing_count', '?')} unimplemented** · "
        f"**{len(sdk.get('mismatched', []))} mismatched** · "
        f"{len(sdk.get('unresolved', []))} unresolved",
        f"- `check-sdk-fabrication`: {_count(fab.get('duplicate_coverage'))} duplicate-coverage · "
        f"{_count(fab.get('tag_drift'))} client↔tag drift",
        f"- `check-model-sync`: {model_line}",
        "",
        "> Client↔tag drift against a *newer* spec usually just means a method now reaches a tag "
        "that did not exist in the baseline. Review, then run "
        "`python3 tools/check-sdk-fabrication.py --update-tags` in the implementation PR.",
        "",
    ]

    missing = sdk.get("missing", [])
    miss_by_tag: dict[str, list[dict]] = defaultdict(list)
    for m in missing:
        miss_by_tag[(m.get("tags") or ["(untagged)"])[0]].append(m)
    miss_lines: list[str] = []
    for tag in sorted(miss_by_tag):
        miss_lines.append(f"**{tag}** ({len(miss_by_tag[tag])})")
        miss_lines.append("")
        for m in miss_by_tag[tag]:
            dep = " `[DEPRECATED]`" if m.get("deprecated") else ""
            miss_lines.append(
                f"- `{m['verb']} {m['path']}` — `{m.get('operationId', '')}`{dep}"
            )
        miss_lines.append("")
    sections += _section(
        f"Unimplemented spec operations ({len(missing)})",
        miss_lines,
        "_none — every spec operation has an SDK method_",
    )

    sections += _section(
        f"SDK endpoint mismatches ({len(sdk.get('mismatched', []))})",
        [
            f"- `{m['file']}::{m['method']}` → `{m['verb']} /{m['path']}` "
            "— not an operation in the live spec (removed or renamed upstream)"
            for m in sdk.get("mismatched", [])
        ],
        "_none_",
    )

    findings = _model_findings(model)
    order = {"CRITICAL": 0, "HIGH": 1, "MEDIUM": 2, "LOW": 3}
    gating = [f for f in findings if _sev(f) in ("CRITICAL", "HIGH", "MEDIUM")]
    gating.sort(key=lambda f: (order.get(_sev(f), 9), str(f.get("sdk_type", ""))))

    # Cap this list explicitly rather than letting the byte-level truncation eat
    # it from the end. Model findings are by far the longest section, and a
    # positional cut would silently drop the most-severe-last ordering with no
    # marker at the cut point. An explicit cap keeps the highest severities and
    # states plainly how many were withheld.
    if max_model_findings is None:
        shown, withheld = gating, []
    else:
        shown, withheld = gating[:max_model_findings], gating[max_model_findings:]
    lines = [_fmt_finding(f) for f in shown]
    if withheld:
        by_sev: dict[str, int] = defaultdict(int)
        for f in withheld:
            by_sev[_sev(f)] += 1
        breakdown = ", ".join(f"{n} {s}" for s, n in sorted(by_sev.items()))
        lines.append(
            f"\n_+{len(withheld)} more not shown here ({breakdown}) — the complete list is "
            f"in the `api-sync-drift-*` run artifact. Do not treat this section as exhaustive._"
        )
    sections += _section(
        f"Model parity findings ({len(gating)} CRITICAL/HIGH/MEDIUM)",
        lines,
        "_none_",
    )

    # ---- 3. how to close --------------------------------------------------
    # Returned separately as the FOOTER: truncation must never drop the
    # instructions, because they are the whole point of the hand-off.
    footer = [
        "---",
        "",
        "## 3. How to close this issue (one PR)",
        "",
        f"1. **Refresh the baseline first**: `python3 tools/check-api-sync.py --update-baseline`. "
        f"The `{BASELINE_REL}` diff in the PR *is* the reviewable spec change.",
        "2. Implement everything in §2 — new client methods and interfaces, request/response "
        "records under `src/Samsara.Sdk/Models/**`, and register every new type in "
        "`SamsaraJsonContext.cs`. Follow \"Adding a New Domain\" in `docs/api-sync/README.md`.",
        "3. If a method legitimately reaches a new tag, run "
        "`python3 tools/check-sdk-fabrication.py --update-tags` and review the diff of "
        "`tools/sdk-client-tags.json`.",
        "4. Make all four checkers green **against the refreshed baseline**:",
        "",
        "```bash",
        f"python3 tools/check-sdk-sync.py --spec-file {BASELINE_REL} --fail-on-mismatch --fail-on-unimplemented",
        f"python3 tools/check-sdk-fabrication.py --spec-file {BASELINE_REL} --fail-on-issues",
        f"python3 tools/check-model-sync.py --spec-file {BASELINE_REL} --fail-on-severity MEDIUM",
        f"python3 tools/check-api-sync.py --spec-file {BASELINE_REL} --no-report --fail-on-diff",
        "```",
        "",
        "5. `dotnet build && dotnet test` green; add contract tests for any new records.",
        "6. Update `docs/api-sync/NN-*.md`, regenerate the README status block "
        "(`python3 tools/render-sync-status.py --write`), and add a `CHANGELOG.md` entry.",
        "7. Open the PR with `Closes #<this issue>`; the `sdk-sync` check must pass.",
        "",
        "> **Do not** hand-edit the baseline, and do not allowlist a checker finding to go green. "
        "Allowlist entries require a spec pointer proving the deviation is intentional — silencing "
        "a finding is the exact failure this tooling exists to prevent.",
        "",
        "_This issue is ready to be assigned to the Copilot coding agent, or handed to a local "
        "agent as-is._",
        "",
    ]

    return header, sections, footer


def truncate(
    header: list[str], sections: list[str], footer: list[str], max_bytes: int
) -> str:
    """Fit within max_bytes by trimming ONLY the body.

    The header (what changed) and the footer (how to close it) are always kept
    whole: a truncated issue that lost its instructions would be worse than a
    short one, since the instructions are the hand-off.
    """
    full = "\n".join(header + sections + footer)
    if len(full.encode("utf-8")) <= max_bytes:
        return full

    notice = [
        "",
        "---",
        "",
        "_⚠️ The findings above were truncated to fit GitHub's issue size limit. The "
        "complete report is attached to the workflow run as the `api-sync-drift-*` "
        "artifact — read it before assuming this list is exhaustive._",
        "",
    ]
    fixed = len(
        "\n".join(header + notice + footer).encode("utf-8")
    ) + 2  # joins between the three blocks
    budget = max_bytes - fixed
    kept: list[str] = []
    used = 0
    for line in sections:
        cost = len(line.encode("utf-8")) + 1
        if used + cost > budget:
            break
        kept.append(line)
        used += cost
    return "\n".join(header + kept + notice + footer)


def main() -> None:
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--summary", required=True)
    ap.add_argument("--sdk-sync")
    ap.add_argument("--fabrication")
    ap.add_argument("--model-sync")
    ap.add_argument("--run-url", default="")
    ap.add_argument("--full-out", required=True)
    ap.add_argument("--issue-out")
    ap.add_argument("--issue-max-bytes", type=int, default=60000)
    ap.add_argument("--max-model-findings", type=int, default=MAX_MODEL_FINDINGS,
                    help="cap on model findings listed inline in the issue body")
    args = ap.parse_args()

    summary = load(args.summary)
    if not summary:
        raise SystemExit(f"ERROR: could not read drift summary: {args.summary}")

    sdk, fab, model = load(args.sdk_sync), load(args.fabrication), load(args.model_sync)

    # Full report: every finding, no cap — it is the artifact the issue defers to.
    header, sections, footer = build_report(summary, sdk, fab, model, args.run_url)
    full = "\n".join(header + sections + footer)
    Path(args.full_out).parent.mkdir(parents=True, exist_ok=True)
    Path(args.full_out).write_text(full)
    print(f"Full report written to {args.full_out} ({len(full.encode('utf-8'))} bytes)")

    if args.issue_out:
        # Issue body: capped finding list, then a byte-level backstop.
        i_header, i_sections, i_footer = build_report(
            summary, sdk, fab, model, args.run_url,
            max_model_findings=args.max_model_findings,
        )
        body = truncate(i_header, i_sections, i_footer, args.issue_max_bytes)
        Path(args.issue_out).parent.mkdir(parents=True, exist_ok=True)
        Path(args.issue_out).write_text(body)
        print(
            f"Issue body written to {args.issue_out} "
            f"({len(body.encode('utf-8'))} bytes, cap {args.issue_max_bytes})"
        )


if __name__ == "__main__":
    main()
