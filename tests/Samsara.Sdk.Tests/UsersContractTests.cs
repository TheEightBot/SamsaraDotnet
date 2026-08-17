namespace Samsara.Sdk.Tests;

using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Organization;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Users domain, locking in the fix for a silent data-loss bug.
/// <para>
/// A single 3-property <c>UserRole</c> record was used for three divergent spec shapes, so
/// <c>User.roles</c> — whose wire shape nests the role under <c>role</c> — deserialized to
/// nothing, <c>expireAt</c> was dropped, and create/update serialized <c>id</c> where the
/// spec requires <c>roleId</c>. Every checker reported green: the union of the three shapes
/// contained every SDK property, so no missing/extra-property finding could fire.
/// </para>
/// </summary>
public sealed class UsersContractTests
{
    // ── GET /users — response nests role + tag ──────────────────────────────
    [Fact]
    public async Task GetAsync_BindsNestedRoleTagAndExpiry()
    {
        var resp = new
        {
            data = new
            {
                id = "usr-1",
                name = "Ada Lovelace",
                email = "ada@example.com",
                authType = "sso",
                roles = new object[]
                {
                    new
                    {
                        role = new { id = "role-1", name = "Fleet Admin" },
                        tag = new { id = "tag-9", name = "West Region", parentTagId = "tag-1" },
                        expireAt = "2026-12-31T23:59:59Z",
                    },
                    // Organizational role: no tag scope.
                    new { role = new { id = "role-2", name = "Read Only" } },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new UsersClient(TestFactory.CreateHttpClient(handler));

        var user = await client.GetAsync("usr-1");

        user.Roles.Should().HaveCount(2);

        var scoped = user.Roles[0];
        scoped.Role!.Id.Should().Be("role-1");
        scoped.Role.Name.Should().Be("Fleet Admin", "the role nests under `role`, not flat on the assignment");
        scoped.Tag!.Id.Should().Be("tag-9");
        scoped.Tag.Name.Should().Be("West Region");
        scoped.Tag.ParentTagId.Should().Be("tag-1");
        scoped.ExpireAt.Should().Be(DateTimeOffset.Parse("2026-12-31T23:59:59Z"));

        var organizational = user.Roles[1];
        organizational.Role!.Id.Should().Be("role-2");
        organizational.Tag.Should().BeNull("an organizational role is not scoped to a tag");
        organizational.ExpireAt.Should().BeNull();
    }

    // ── POST /users — request uses flat roleId/tagId ────────────────────────
    [Fact]
    public async Task CreateAsync_SerializesRoleIdNotId()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            data = new
            {
                id = "usr-2",
                name = "Grace Hopper",
                email = "grace@example.com",
                authType = "samsara",
                roles = Array.Empty<object>(),
            },
        });
        var client = new UsersClient(TestFactory.CreateHttpClient(handler));

        await client.CreateAsync(new CreateUserRequest
        {
            Name = "Grace Hopper",
            Email = "grace@example.com",
            AuthType = "samsara",
            Roles = new[]
            {
                new UserRoleInput { RoleId = "role-1", TagId = "tag-9" },
                new UserRoleInput { RoleId = "role-2" },
            },
        });

        using var doc = JsonDocument.Parse(handler.LastRequestBody!);
        var roles = doc.RootElement.GetProperty("roles");

        roles.GetArrayLength().Should().Be(2);
        roles[0].GetProperty("roleId").GetString().Should().Be("role-1");
        roles[0].GetProperty("tagId").GetString().Should().Be("tag-9");
        roles[0].TryGetProperty("id", out _).Should().BeFalse(
            "the spec's create shape is roleId/tagId; sending `id` silently assigned no role");

        // Organizational role: tagId omitted entirely rather than sent as null.
        roles[1].GetProperty("roleId").GetString().Should().Be("role-2");
        roles[1].TryGetProperty("tagId", out _).Should().BeFalse();
    }

    // ── GET /user-roles — the standalone role shape is unchanged ────────────
    [Fact]
    public void UserRole_RemainsTheFlatUserRolesShape()
    {
        var role = JsonSerializer.Deserialize<UserRole>(
            """{"id":"role-1","name":"Fleet Admin"}""",
            Samsara.Sdk.Serialization.SamsaraSerializerOptions.Default)!;

        role.Id.Should().Be("role-1");
        role.Name.Should().Be("Fleet Admin");
    }
}
