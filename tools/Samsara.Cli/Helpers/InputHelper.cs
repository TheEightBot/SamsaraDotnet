using Spectre.Console;

namespace Samsara.Cli.Helpers;

/// <summary>
/// Helpers for collecting user input via Spectre.Console prompts.
/// </summary>
internal static class InputHelper
{
    public static string AskId(string prompt = "Enter ID")
        => AnsiConsole.Ask<string>($"[green]{prompt.EscapeMarkup()}:[/]");

    public static string? AskOptionalId(string prompt)
    {
        var val = AnsiConsole.Prompt(
            new TextPrompt<string>($"[grey]{prompt.EscapeMarkup()} [dim](leave blank to skip)[/]:[/]")
                .AllowEmpty());
        return string.IsNullOrWhiteSpace(val) ? null : val;
    }

    public static string AskOptionalString(string prompt)
        => AnsiConsole.Prompt(
            new TextPrompt<string>($"[grey]{prompt.EscapeMarkup()}:[/]")
                .AllowEmpty());

    public static bool Confirm(string action)
        => AnsiConsole.Confirm($"[yellow]Are you sure you want to {action.EscapeMarkup()}?[/]");

    /// <summary>
    /// Prompts the user to choose a time range filter. Returns start/end as UTC <see cref="DateTimeOffset"/>
    /// values, or <c>null</c> when no filter is desired.
    /// </summary>
    public static (DateTimeOffset? Start, DateTimeOffset? End) AskTimeRange(string context = "")
    {
        var label = string.IsNullOrWhiteSpace(context)
            ? "[bold]Select time range filter:[/]"
            : $"[bold]Time range for {context.EscapeMarkup()}:[/]";

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(label)
                .AddChoices(
                    "No filter (all data)",
                    "Last 1 hour",
                    "Last 24 hours",
                    "Last 7 days",
                    "Last 30 days",
                    "Custom range"));

        var now = DateTimeOffset.UtcNow;
        return choice switch
        {
            "Last 1 hour"    => (now.AddHours(-1),  now),
            "Last 24 hours"  => (now.AddHours(-24), now),
            "Last 7 days"    => (now.AddDays(-7),   now),
            "Last 30 days"   => (now.AddDays(-30),  now),
            "Custom range"   => AskCustomRange(),
            _                => ((DateTime?)null, (DateTime?)null),
        };
    }

    private static (DateTimeOffset? Start, DateTimeOffset? End) AskCustomRange()
    {
        AnsiConsole.MarkupLine("[grey]Enter dates as YYYY-MM-DD or YYYY-MM-DD HH:MM (UTC). Leave blank to skip.[/]");

        var startStr = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Start date/time:[/]").AllowEmpty());

        var endStr = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]End date/time[/] [grey](blank = now):[/]").AllowEmpty());

        DateTimeOffset? start = null;
        DateTimeOffset? end = null;

        if (!string.IsNullOrWhiteSpace(startStr))
        {
            if (DateTimeOffset.TryParse(startStr, null, System.Globalization.DateTimeStyles.AssumeUniversal, out var s))
                start = s.ToUniversalTime();
            else
                AnsiConsole.MarkupLine($"[red]Could not parse '[/][yellow]{startStr.EscapeMarkup()}[/][red]', ignoring start.[/]");
        }

        if (!string.IsNullOrWhiteSpace(endStr))
        {
            if (DateTimeOffset.TryParse(endStr, null, System.Globalization.DateTimeStyles.AssumeUniversal, out var e))
                end = e.ToUniversalTime();
            else
                AnsiConsole.MarkupLine($"[red]Could not parse '[/][yellow]{endStr.EscapeMarkup()}[/][red]', ignoring end.[/]");
        }

        return (start, end);
    }
}
