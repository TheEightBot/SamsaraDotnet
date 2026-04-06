using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Samsara.Cli.Helpers;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Addresses;
using Samsara.Sdk.Models.Assignments;
using Samsara.Sdk.Models.Communication;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Models.Documents;
using Samsara.Sdk.Models.Drivers;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Models.Fuel;
using Samsara.Sdk.Models.Industrial;
using Samsara.Sdk.Models.Issues;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Models.Media;
using Samsara.Sdk.Models.Organization;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Models.Safety;
using Samsara.Sdk.Models.Tags;
using Samsara.Sdk.Models.Training;
using Samsara.Sdk.Models.Webhooks;
using Spectre.Console;

namespace Samsara.Cli;

/// <summary>
/// Main interactive TUI loop for the Samsara CLI test harness.
/// </summary>
[SuppressMessage("Design", "CA1031", Justification = "General catch for CLI robustness")]
internal sealed class TuiApp
{
    private readonly ISamsaraClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public TuiApp(ISamsaraClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            AnsiConsole.WriteLine();
            var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Select a category:[/]")
                    .PageSize(20)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(
                        "Fleet Assets",
                        "People & Organization",
                        "Tags & Attributes",
                        "Routing",
                        "Compliance",
                        "Safety",
                        "Maintenance",
                        "Documents & Forms",
                        "Communication & Alerts",
                        "Fuel & Energy",
                        "Industrial & Sensors",
                        "Addresses",
                        "Webhooks",
                        "Assignments",
                        "Training",
                        "Issues",
                        "Media",
                        "Quit"));

            if (category == "Quit") break;

            await HandleCategoryAsync(category);
        }

        AnsiConsole.MarkupLine("[grey]Goodbye![/]");
    }

    private async Task HandleCategoryAsync(string category)
    {
        switch (category)
        {
            case "Fleet Assets": await FleetAssetsMenuAsync(); break;
            case "People & Organization": await PeopleMenuAsync(); break;
            case "Tags & Attributes": await TagsAttributesMenuAsync(); break;
            case "Routing": await RoutingMenuAsync(); break;
            case "Compliance": await ComplianceMenuAsync(); break;
            case "Safety": await SafetyMenuAsync(); break;
            case "Maintenance": await MaintenanceMenuAsync(); break;
            case "Documents & Forms": await DocumentsFormsMenuAsync(); break;
            case "Communication & Alerts": await CommunicationMenuAsync(); break;
            case "Fuel & Energy": await FuelMenuAsync(); break;
            case "Industrial & Sensors": await IndustrialMenuAsync(); break;
            case "Addresses": await AddressesMenuAsync(); break;
            case "Webhooks": await WebhooksMenuAsync(); break;
            case "Assignments": await AssignmentsMenuAsync(); break;
            case "Training": await TrainingMenuAsync(); break;
            case "Issues": await IssuesMenuAsync(); break;
            case "Media": await MediaMenuAsync(); break;
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static string SubMenu(string title, params string[] operations)
    {
        var choices = new List<string> { "← Back" };
        choices.AddRange(operations);
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold cyan]{title.EscapeMarkup()}[/]")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down)[/]")
                .AddChoices(choices));
    }

    private static CancellationToken Timeout60s()
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
        return cts.Token;
    }

    private static async Task<List<T>> CollectAsync<T>(IAsyncEnumerable<T> source, int max = 50)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
            if (list.Count >= max) break;
        }
        return list;
    }

    private static T? DeserializePrompt<T>(string typeName) where T : class
    {
        AnsiConsole.MarkupLine($"[grey]Enter JSON for {typeName.EscapeMarkup()} (single line):[/]");
        var raw = Console.ReadLine() ?? "{}";
        try
        {
            return JsonSerializer.Deserialize<T>(raw);
        }
        catch (Exception ex)
        {
            ResultRenderer.RenderError(ex);
            return null;
        }
    }

    // ── Fleet Assets ─────────────────────────────────────────────────────────

    private async Task FleetAssetsMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Fleet Assets", "Vehicles", "Trailers", "Equipment", "Gateways");
            if (sub == "← Back") return;
            switch (sub)
            {
                case "Vehicles": await VehiclesMenuAsync(); break;
                case "Trailers": await TrailersMenuAsync(); break;
                case "Equipment": await EquipmentMenuAsync(); break;
                case "Gateways": await GatewaysMenuAsync(); break;
            }
        }
    }

    private async Task VehiclesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Vehicles", "List All", "Get by ID", "Update", "List Locations", "List Stats");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching vehicles...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Vehicles.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Vehicles", v => [v.Id, v.Name ?? "", v.Vin ?? "", v.LicensePlate ?? ""], ["ID", "Name", "VIN", "License Plate"]);
                        });
                        break;
                    case "Get by ID":
                        var vid = InputHelper.AskId("Vehicle ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching vehicle...[/]", async _ =>
                        {
                            var v = await _client.Vehicles.GetAsync(vid, Timeout60s());
                            ResultRenderer.RenderObject(v, $"Vehicle {vid}");
                        });
                        break;
                    case "Update":
                        var uvid = InputHelper.AskId("Vehicle ID to update");
                        var uvReq = DeserializePrompt<Samsara.Sdk.Models.Fleet.UpdateVehicleRequest>("UpdateVehicleRequest");
                        if (uvReq != null && InputHelper.Confirm("update vehicle"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var v = await _client.Vehicles.UpdateAsync(uvid, uvReq, Timeout60s());
                                ResultRenderer.RenderObject(v, "Updated Vehicle");
                            });
                        }
                        break;
                    case "List Locations":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching locations...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Vehicles.ListLocationsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Vehicle Locations", l => [l.Id, l.Name ?? "", l.Latitude?.ToString() ?? "", l.Longitude?.ToString() ?? ""], ["ID", "Name", "Lat", "Lon"]);
                        });
                        break;
                    case "List Stats":
                        var statTypes = InputHelper.AskOptionalString("Stat types (e.g. engineStates,fuelPercents)");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching stats...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Vehicles.ListStatsAsync(statTypes, Timeout60s()));
                            ResultRenderer.RenderList(items, "Vehicle Stats", s => [s.Id, s.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task TrailersMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Trailers", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching trailers...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Trailers.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Trailers", t => [t.Id, t.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Trailer ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching trailer...[/]", async _ =>
                        {
                            var t = await _client.Trailers.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(t, $"Trailer {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateTrailerRequest>("CreateTrailerRequest");
                        if (cReq != null && InputHelper.Confirm("create trailer"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var t = await _client.Trailers.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(t, "Created Trailer");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Trailer ID to update");
                        var uReq = DeserializePrompt<UpdateTrailerRequest>("UpdateTrailerRequest");
                        if (uReq != null && InputHelper.Confirm("update trailer"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var t = await _client.Trailers.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(t, "Updated Trailer");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Trailer ID to delete");
                        if (InputHelper.Confirm($"delete trailer {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Trailers.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Trailer deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task EquipmentMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Equipment", "List All", "Get by ID", "List Locations");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching equipment...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Equipment.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Equipment", e => [e.Id, e.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Equipment ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching equipment...[/]", async _ =>
                        {
                            var e = await _client.Equipment.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(e, $"Equipment {id}");
                        });
                        break;
                    case "List Locations":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching locations...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Equipment.ListLocationsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Equipment Locations", l => [l.Id, l.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task GatewaysMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Gateways", "List All", "Get by ID");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching gateways...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Gateways.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Gateways", g => [g.Id, g.Serial ?? "", g.Model ?? ""], ["ID", "Serial", "Model"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Gateway ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching gateway...[/]", async _ =>
                        {
                            var g = await _client.Gateways.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(g, $"Gateway {id}");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── People & Organization ─────────────────────────────────────────────────

    private async Task PeopleMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("People & Organization", "Drivers", "Users", "Contacts", "User Roles", "Organization Info");
            if (sub == "← Back") return;
            switch (sub)
            {
                case "Drivers": await DriversMenuAsync(); break;
                case "Users": await UsersMenuAsync(); break;
                case "Contacts": await ContactsMenuAsync(); break;
                case "User Roles": await UserRolesMenuAsync(); break;
                case "Organization Info": await OrgInfoMenuAsync(); break;
            }
        }
    }

    private async Task DriversMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Drivers", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching drivers...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Drivers.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Drivers", d => [d.Id, d.Name, d.Username ?? "", d.Status ?? ""], ["ID", "Name", "Username", "Status"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Driver ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching driver...[/]", async _ =>
                        {
                            var d = await _client.Drivers.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(d, $"Driver {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateDriverRequest>("CreateDriverRequest");
                        if (cReq != null && InputHelper.Confirm("create driver"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var d = await _client.Drivers.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(d, "Created Driver");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Driver ID to update");
                        var uReq = DeserializePrompt<UpdateDriverRequest>("UpdateDriverRequest");
                        if (uReq != null && InputHelper.Confirm("update driver"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var d = await _client.Drivers.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(d, "Updated Driver");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Driver ID to delete");
                        if (InputHelper.Confirm($"delete driver {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Drivers.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Driver deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task UsersMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Users", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching users...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Users.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Users", u => [u.Id, u.Name ?? "", u.Email ?? ""], ["ID", "Name", "Email"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("User ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching user...[/]", async _ =>
                        {
                            var u = await _client.Users.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(u, $"User {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateUserRequest>("CreateUserRequest");
                        if (cReq != null && InputHelper.Confirm("create user"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var u = await _client.Users.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(u, "Created User");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("User ID to update");
                        var uReq = DeserializePrompt<UpdateUserRequest>("UpdateUserRequest");
                        if (uReq != null && InputHelper.Confirm("update user"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var u = await _client.Users.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(u, "Updated User");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("User ID to delete");
                        if (InputHelper.Confirm($"delete user {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Users.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("User deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task ContactsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Contacts", "List All", "Get by ID");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching contacts...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Contacts.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Contacts", c => [c.Id, c.FirstName ?? "", c.LastName ?? "", c.Email ?? ""], ["ID", "First Name", "Last Name", "Email"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Contact ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching contact...[/]", async _ =>
                        {
                            var c = await _client.Contacts.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(c, $"Contact {id}");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task UserRolesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("User Roles", "List All");
            if (op == "← Back") return;
            try
            {
                if (op == "List All")
                {
                    await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching user roles...[/]", async _ =>
                    {
                        var items = await CollectAsync(_client.UserRoles.ListAsync(Timeout60s()));
                        ResultRenderer.RenderList(items, "User Roles", r => [r.Id, r.Name ?? ""], ["ID", "Name"]);
                    });
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task OrgInfoMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Organization Info", "Get");
            if (op == "← Back") return;
            try
            {
                if (op == "Get")
                {
                    await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching org info...[/]", async _ =>
                    {
                        var info = await _client.Organization.GetAsync(Timeout60s());
                        ResultRenderer.RenderObject(info, "Organization Info");
                    });
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Tags & Attributes ─────────────────────────────────────────────────────

    private async Task TagsAttributesMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Tags & Attributes", "Tags", "Attributes");
            if (sub == "← Back") return;
            switch (sub)
            {
                case "Tags": await TagsMenuAsync(); break;
                case "Attributes": await AttributesMenuAsync(); break;
            }
        }
    }

    private async Task TagsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Tags", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching tags...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Tags.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Tags", t => [t.Id, t.Name, t.ParentTagId ?? ""], ["ID", "Name", "Parent Tag ID"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Tag ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching tag...[/]", async _ =>
                        {
                            var t = await _client.Tags.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(t, $"Tag {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateTagRequest>("CreateTagRequest");
                        if (cReq != null && InputHelper.Confirm("create tag"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var t = await _client.Tags.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(t, "Created Tag");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Tag ID to update");
                        var uReq = DeserializePrompt<UpdateTagRequest>("UpdateTagRequest");
                        if (uReq != null && InputHelper.Confirm("update tag"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var t = await _client.Tags.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(t, "Updated Tag");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Tag ID to delete");
                        if (InputHelper.Confirm($"delete tag {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Tags.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Tag deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task AttributesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Attributes", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching attributes...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Attributes.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Attributes", a => [a.Id, a.Name ?? "", a.EntityType ?? ""], ["ID", "Name", "Entity Type"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Attribute ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching attribute...[/]", async _ =>
                        {
                            var a = await _client.Attributes.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(a, $"Attribute {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateAttributeRequest>("CreateAttributeRequest");
                        if (cReq != null && InputHelper.Confirm("create attribute"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var a = await _client.Attributes.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Created Attribute");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Attribute ID to update");
                        var uReq = DeserializePrompt<UpdateAttributeRequest>("UpdateAttributeRequest");
                        if (uReq != null && InputHelper.Confirm("update attribute"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var a = await _client.Attributes.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Updated Attribute");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Attribute ID to delete");
                        if (InputHelper.Confirm($"delete attribute {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Attributes.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Attribute deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Routing ───────────────────────────────────────────────────────────────

    private async Task RoutingMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Routing", "Routes", "Hubs", "Trips");
            if (sub == "← Back") return;
            switch (sub)
            {
                case "Routes": await RoutesMenuAsync(); break;
                case "Hubs": await HubsMenuAsync(); break;
                case "Trips": await TripsMenuAsync(); break;
            }
        }
    }

    private async Task RoutesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Routes", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        var (rStart, rEnd) = InputHelper.AskTimeRange("Routes");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching routes...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Routes.ListAsync(rStart, rEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Routes", r => [r.Id, r.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Route ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching route...[/]", async _ =>
                        {
                            var r = await _client.Routes.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(r, $"Route {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateRouteRequest>("CreateRouteRequest");
                        if (cReq != null && InputHelper.Confirm("create route"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var r = await _client.Routes.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(r, "Created Route");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Route ID to update");
                        var uReq = DeserializePrompt<UpdateRouteRequest>("UpdateRouteRequest");
                        if (uReq != null && InputHelper.Confirm("update route"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var r = await _client.Routes.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(r, "Updated Route");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Route ID to delete");
                        if (InputHelper.Confirm($"delete route {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Routes.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Route deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task HubsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Hubs", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching hubs...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Hubs.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Hubs", h => [h.Id, h.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Hub ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching hub...[/]", async _ =>
                        {
                            var h = await _client.Hubs.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(h, $"Hub {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateHubRequest>("CreateHubRequest");
                        if (cReq != null && InputHelper.Confirm("create hub"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var h = await _client.Hubs.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(h, "Created Hub");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Hub ID to update");
                        var uReq = DeserializePrompt<UpdateHubRequest>("UpdateHubRequest");
                        if (uReq != null && InputHelper.Confirm("update hub"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var h = await _client.Hubs.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(h, "Updated Hub");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Hub ID to delete");
                        if (InputHelper.Confirm($"delete hub {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Hubs.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Hub deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task TripsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Trips", "List All");
            if (op == "← Back") return;
            try
            {
                if (op == "List All")
                {
                    var (tStart, tEnd) = InputHelper.AskTimeRange("Trips");
                    var tVehicleId = InputHelper.AskOptionalId("Vehicle ID");
                    var tDriverId = InputHelper.AskOptionalId("Driver ID");
                    await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching trips...[/]", async _ =>
                    {
                        var items = await CollectAsync(_client.Trips.ListAsync(tStart, tEnd, tVehicleId, tDriverId, Timeout60s()));
                        ResultRenderer.RenderList(items, "Trips", t => [t.Id, t.VehicleId ?? "", t.DriverId ?? ""], ["ID", "Vehicle ID", "Driver ID"]);
                    });
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Compliance ────────────────────────────────────────────────────────────

    private async Task ComplianceMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Compliance",
                "HOS Logs", "HOS Violations", "HOS Clocks",
                "DVIRs (Compliance)", "Get DVIR",
                "Tachograph Activities", "Tachograph Files",
                "IFTA Details", "IFTA Summary");
            if (sub == "← Back") return;
            try
            {
                switch (sub)
                {
                    case "HOS Logs":
                        var (hosLogStart, hosLogEnd) = InputHelper.AskTimeRange("HOS Logs");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching HOS logs...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Compliance.ListHosLogsAsync(hosLogStart, hosLogEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "HOS Logs", l => [l.Id, l.DriverId ?? "", l.HosStatusType ?? ""], ["ID", "Driver ID", "Status"]);
                        });
                        break;
                    case "HOS Violations":
                        var (hosViolStart, hosViolEnd) = InputHelper.AskTimeRange("HOS Violations");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching HOS violations...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Compliance.ListHosViolationsAsync(hosViolStart, hosViolEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "HOS Violations", v => [v.DriverId ?? "", v.ViolationType ?? ""], ["Driver ID", "Type"]);
                        });
                        break;
                    case "HOS Clocks":
                        var driverId = InputHelper.AskId("Driver ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching HOS clocks...[/]", async _ =>
                        {
                            var clocks = await _client.Compliance.GetHosClocksAsync(driverId, Timeout60s());
                            ResultRenderer.RenderObject(clocks, $"HOS Clocks for {driverId}");
                        });
                        break;
                    case "DVIRs (Compliance)":
                        var (dvirStart, dvirEnd) = InputHelper.AskTimeRange("DVIRs");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching DVIRs...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Compliance.ListDvirsAsync(dvirStart, dvirEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "DVIRs", d => [d.Id, d.Vehicle?.Id ?? "", d.AuthorSignature?.DriverId ?? ""], ["ID", "Vehicle ID", "Driver ID"]);
                        });
                        break;
                    case "Get DVIR":
                        var dvirId = InputHelper.AskId("DVIR ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching DVIR...[/]", async _ =>
                        {
                            var d = await _client.Compliance.GetDvirAsync(dvirId, Timeout60s());
                            ResultRenderer.RenderObject(d, $"DVIR {dvirId}");
                        });
                        break;
                    case "Tachograph Activities":
                        var (taStart, taEnd) = InputHelper.AskTimeRange("Tachograph Activities");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching tachograph activities...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Tachograph.ListActivitiesAsync(taStart, taEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Tachograph Activities", a => [a.DriverId ?? "", a.ActivityType ?? ""], ["Driver ID", "Activity Type"]);
                        });
                        break;
                    case "Tachograph Files":
                        var (tfStart, tfEnd) = InputHelper.AskTimeRange("Tachograph Files");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching tachograph files...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Tachograph.ListFilesAsync(tfStart, tfEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Tachograph Files", f => [f.DriverId ?? "", f.FileType ?? ""], ["Driver ID", "File Type"]);
                        });
                        break;
                    case "IFTA Details":
                        var (iftaStart, iftaEnd) = InputHelper.AskTimeRange("IFTA Details");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching IFTA details...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Ifta.ListDetailsAsync(iftaStart, iftaEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "IFTA Details", d => [d.VehicleId ?? "", d.Jurisdiction ?? ""], ["Vehicle ID", "Jurisdiction"]);
                        });
                        break;
                    case "IFTA Summary":
                        var (iftaSumStart, iftaSumEnd) = InputHelper.AskTimeRange("IFTA Summary");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching IFTA summary...[/]", async _ =>
                        {
                            var items = await _client.Ifta.GetSummaryAsync(iftaSumStart, iftaSumEnd, Timeout60s());
                            ResultRenderer.RenderObject(items, "IFTA Summary");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Safety ────────────────────────────────────────────────────────────────

    private async Task SafetyMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Safety", "List Events", "Get Event by ID", "Vehicle Safety Scores", "Driver Safety Scores");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List Events":
                        var (seStart, seEnd) = InputHelper.AskTimeRange("Safety Events");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching safety events...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Safety.ListEventsAsync(seStart, seEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Safety Events", e => [e.Id, e.BehaviorLabel ?? "", e.Vehicle?.Id ?? ""], ["ID", "Behavior", "Vehicle ID"]);
                        });
                        break;
                    case "Get Event by ID":
                        var id = InputHelper.AskId("Safety Event ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching event...[/]", async _ =>
                        {
                            var e = await _client.Safety.GetEventAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(e, $"Safety Event {id}");
                        });
                        break;
                    case "Vehicle Safety Scores":
                        var (vssStart, vssEnd) = InputHelper.AskTimeRange("Vehicle Safety Scores");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching vehicle safety scores...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Safety.ListVehicleSafetyScoresAsync(vssStart, vssEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Vehicle Safety Scores", s => [s.VehicleId ?? "", s.SafetyScore?.ToString() ?? ""], ["Vehicle ID", "Score"]);
                        });
                        break;
                    case "Driver Safety Scores":
                        var (dssStart, dssEnd) = InputHelper.AskTimeRange("Driver Safety Scores");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching driver safety scores...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Safety.ListDriverSafetyScoresAsync(dssStart, dssEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Driver Safety Scores", s => [s.DriverId ?? "", s.SafetyScore?.ToString() ?? ""], ["Driver ID", "Score"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Maintenance ───────────────────────────────────────────────────────────

    private async Task MaintenanceMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Maintenance", "List DVIRs", "Get DVIR by ID", "List DTCs");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List DVIRs":
                        var (mDvirStart, mDvirEnd) = InputHelper.AskTimeRange("Maintenance DVIRs");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching maintenance DVIRs...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Maintenance.ListDvirsAsync(mDvirStart, mDvirEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Maintenance DVIRs", d => [d.Id, d.VehicleId ?? "", d.Defects?.Count.ToString() ?? "0"], ["ID", "Vehicle ID", "Defects"]);
                        });
                        break;
                    case "Get DVIR by ID":
                        var id = InputHelper.AskId("DVIR ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching DVIR...[/]", async _ =>
                        {
                            var d = await _client.Maintenance.GetDvirAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(d, $"Maintenance DVIR {id}");
                        });
                        break;
                    case "List DTCs":
                        var (dtcStart, dtcEnd) = InputHelper.AskTimeRange("DTCs");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching DTCs...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Maintenance.ListDtcsAsync(dtcStart, dtcEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "DTCs", d => [d.DtcId ?? "", d.VehicleId ?? "", d.DtcShortCode ?? ""], ["DTC ID", "Vehicle ID", "Code"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Documents & Forms ─────────────────────────────────────────────────────

    private async Task DocumentsFormsMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Documents & Forms", "Documents", "Forms");
            if (sub == "← Back") return;
            switch (sub)
            {
                case "Documents": await DocumentsMenuAsync(); break;
                case "Forms": await FormsMenuAsync(); break;
            }
        }
    }

    private async Task DocumentsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Documents", "List All", "Get by ID", "Create", "Delete", "List Types");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching documents...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Documents.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Documents", d => [d.Id, d.DocumentTypeId ?? "", d.DriverId ?? ""], ["ID", "Type ID", "Driver ID"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Document ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching document...[/]", async _ =>
                        {
                            var d = await _client.Documents.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(d, $"Document {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateDocumentRequest>("CreateDocumentRequest");
                        if (cReq != null && InputHelper.Confirm("create document"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var d = await _client.Documents.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(d, "Created Document");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Document ID to delete");
                        if (InputHelper.Confirm($"delete document {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Documents.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Document deleted.");
                        }
                        break;
                    case "List Types":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching document types...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Documents.ListTypesAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Document Types", t => [t.Id, t.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task FormsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Forms", "List Templates", "List Submissions", "Get Submission by ID");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List Templates":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching form templates...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Forms.ListTemplatesAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Form Templates", t => [t.Id, t.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "List Submissions":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching form submissions...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Forms.ListSubmissionsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Form Submissions", s => [s.Id, s.FormTemplateId ?? "", s.DriverId ?? ""], ["ID", "Template ID", "Driver ID"]);
                        });
                        break;
                    case "Get Submission by ID":
                        var id = InputHelper.AskId("Submission ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching submission...[/]", async _ =>
                        {
                            var s = await _client.Forms.GetSubmissionAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(s, $"Form Submission {id}");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Communication & Alerts ────────────────────────────────────────────────

    private async Task CommunicationMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Communication & Alerts", "Alerts", "Alert Configurations", "Messages");
            if (sub == "← Back") return;
            try
            {
                switch (sub)
                {
                    case "Alerts":
                        await AlertsMenuAsync();
                        break;
                    case "Alert Configurations":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching alert configurations...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Alerts.ListConfigurationsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Alert Configurations", c => [c.Id, c.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Messages":
                        await MessagesMenuAsync();
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task AlertsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Alerts", "List All", "Get by ID");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching alerts...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Alerts.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Alerts", a => [a.Id, a.ConfigurationId ?? ""], ["ID", "Configuration ID"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Alert ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching alert...[/]", async _ =>
                        {
                            var a = await _client.Alerts.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(a, $"Alert {id}");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task MessagesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Messages", "List All", "Send Message");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching messages...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Messages.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Driver Messages", m => [m.Id, m.DriverId ?? "", m.Body ?? ""], ["ID", "Driver ID", "Text"]);
                        });
                        break;
                    case "Send Message":
                        var req = DeserializePrompt<SendDriverMessageRequest>("SendDriverMessageRequest");
                        if (req != null && InputHelper.Confirm("send message"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Sending...[/]", async _ =>
                            {
                                await _client.Messages.SendAsync(req, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Message sent.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Fuel & Energy ─────────────────────────────────────────────────────────

    private async Task FuelMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Fuel & Energy", "List Fuel Purchases", "List Energy Levels");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List Fuel Purchases":
                        var (fpStart, fpEnd) = InputHelper.AskTimeRange("Fuel Purchases");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching fuel purchases...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Fuel.ListPurchasesAsync(fpStart, fpEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Fuel Purchases", p => [p.Id, p.VehicleId ?? "", p.VolumeGallons?.ToString() ?? ""], ["ID", "Vehicle ID", "Gallons"]);
                        });
                        break;
                    case "List Energy Levels":
                        var (elStart, elEnd) = InputHelper.AskTimeRange("Energy Levels");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching energy levels...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Fuel.ListEnergyLevelsAsync(elStart, elEnd, Timeout60s()));
                            ResultRenderer.RenderList(items, "Energy Levels", e => [e.VehicleId ?? "", e.Percent?.ToString() ?? ""], ["Vehicle ID", "Charge %"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Industrial & Sensors ──────────────────────────────────────────────────

    private async Task IndustrialMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Industrial & Sensors", "Industrial Assets", "Data Inputs", "Get Data Input", "Sensors", "Get Sensor", "Sensor History");
            if (sub == "← Back") return;
            try
            {
                switch (sub)
                {
                    case "Industrial Assets":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching industrial assets...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Industrial.ListAssetsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Industrial Assets", a => [a.Id, a.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Data Inputs":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching data inputs...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Industrial.ListDataInputsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Data Inputs", d => [d.Id, d.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Get Data Input":
                        var diId = InputHelper.AskId("Data Input ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching data input...[/]", async _ =>
                        {
                            var d = await _client.Industrial.GetDataInputAsync(diId, Timeout60s());
                            ResultRenderer.RenderObject(d, $"Data Input {diId}");
                        });
                        break;
                    case "Sensors":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching sensors...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Sensors.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Sensors", s => [s.Id, s.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                    case "Get Sensor":
                        var sId = InputHelper.AskId("Sensor ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching sensor...[/]", async _ =>
                        {
                            var s = await _client.Sensors.GetAsync(sId, Timeout60s());
                            ResultRenderer.RenderObject(s, $"Sensor {sId}");
                        });
                        break;
                    case "Sensor History":
                        var idsRaw = InputHelper.AskOptionalString("Sensor IDs (comma-separated)");
                        var sensorIds = idsRaw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching sensor history...[/]", async _ =>
                        {
                            var result = await _client.Sensors.GetHistoryAsync(sensorIds, Timeout60s());
                            ResultRenderer.RenderObject(result, "Sensor History");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Addresses ─────────────────────────────────────────────────────────────

    private async Task AddressesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Addresses", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching addresses...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Addresses.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Addresses", a => [a.Id, a.Name ?? "", a.FormattedAddress ?? ""], ["ID", "Name", "Address"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Address ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching address...[/]", async _ =>
                        {
                            var a = await _client.Addresses.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(a, $"Address {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateAddressRequest>("CreateAddressRequest");
                        if (cReq != null && InputHelper.Confirm("create address"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var a = await _client.Addresses.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Created Address");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Address ID to update");
                        var uReq = DeserializePrompt<UpdateAddressRequest>("UpdateAddressRequest");
                        if (uReq != null && InputHelper.Confirm("update address"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var a = await _client.Addresses.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Updated Address");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Address ID to delete");
                        if (InputHelper.Confirm($"delete address {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Addresses.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Address deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Webhooks ──────────────────────────────────────────────────────────────

    private async Task WebhooksMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Webhooks", "List All", "Get by ID", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching webhooks...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Webhooks.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Webhooks", w => [w.Id, w.Name ?? "", w.Url ?? ""], ["ID", "Name", "URL"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Webhook ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching webhook...[/]", async _ =>
                        {
                            var w = await _client.Webhooks.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(w, $"Webhook {id}");
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateWebhookRequest>("CreateWebhookRequest");
                        if (cReq != null && InputHelper.Confirm("create webhook"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var w = await _client.Webhooks.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(w, "Created Webhook");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Webhook ID to update");
                        var uReq = DeserializePrompt<UpdateWebhookRequest>("UpdateWebhookRequest");
                        if (uReq != null && InputHelper.Confirm("update webhook"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var w = await _client.Webhooks.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(w, "Updated Webhook");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Webhook ID to delete");
                        if (InputHelper.Confirm($"delete webhook {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.Webhooks.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Webhook deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Assignments ───────────────────────────────────────────────────────────

    private async Task AssignmentsMenuAsync()
    {
        while (true)
        {
            var sub = SubMenu("Assignments", "Driver-Vehicle Assignments", "Trailer Assignments", "Carrier Proposed Assignments");
            if (sub == "← Back") return;
            switch (sub)
            {
                case "Driver-Vehicle Assignments": await DriverVehicleAssignmentsMenuAsync(); break;
                case "Trailer Assignments": await TrailerAssignmentsMenuAsync(); break;
                case "Carrier Proposed Assignments": await CarrierProposedAssignmentsMenuAsync(); break;
            }
        }
    }

    private async Task DriverVehicleAssignmentsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Driver-Vehicle Assignments", "List All", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching assignments...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.DriverVehicleAssignments.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Driver-Vehicle Assignments", a => [a.Id, a.DriverId ?? "", a.VehicleId ?? ""], ["ID", "Driver ID", "Vehicle ID"]);
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateDriverVehicleAssignmentRequest>("CreateDriverVehicleAssignmentRequest");
                        if (cReq != null && InputHelper.Confirm("create assignment"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var a = await _client.DriverVehicleAssignments.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Created Assignment");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Assignment ID to update");
                        var uReq = DeserializePrompt<UpdateDriverVehicleAssignmentRequest>("UpdateDriverVehicleAssignmentRequest");
                        if (uReq != null && InputHelper.Confirm("update assignment"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var a = await _client.DriverVehicleAssignments.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Updated Assignment");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Assignment ID to delete");
                        if (InputHelper.Confirm($"delete assignment {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.DriverVehicleAssignments.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Assignment deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task TrailerAssignmentsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Trailer Assignments", "List All");
            if (op == "← Back") return;
            try
            {
                if (op == "List All")
                {
                    await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching trailer assignments...[/]", async _ =>
                    {
                        var items = await CollectAsync(_client.TrailerAssignments.ListAsync(Timeout60s()));
                        ResultRenderer.RenderList(items, "Trailer Assignments", a => [a.Id, a.TrailerId ?? "", a.VehicleId ?? ""], ["ID", "Trailer ID", "Vehicle ID"]);
                    });
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    private async Task CarrierProposedAssignmentsMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Carrier Proposed Assignments", "List All", "Create", "Update", "Delete");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching carrier proposed assignments...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.CarrierProposedAssignments.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Carrier Proposed Assignments", a => [a.Id, a.DriverId ?? ""], ["ID", "Driver ID"]);
                        });
                        break;
                    case "Create":
                        var cReq = DeserializePrompt<CreateCarrierProposedAssignmentRequest>("CreateCarrierProposedAssignmentRequest");
                        if (cReq != null && InputHelper.Confirm("create carrier proposed assignment"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Creating...[/]", async _ =>
                            {
                                var a = await _client.CarrierProposedAssignments.CreateAsync(cReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Created Carrier Proposed Assignment");
                            });
                        }
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Assignment ID to update");
                        var uReq = DeserializePrompt<UpdateCarrierProposedAssignmentRequest>("UpdateCarrierProposedAssignmentRequest");
                        if (uReq != null && InputHelper.Confirm("update carrier proposed assignment"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var a = await _client.CarrierProposedAssignments.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(a, "Updated Carrier Proposed Assignment");
                            });
                        }
                        break;
                    case "Delete":
                        var did = InputHelper.AskId("Assignment ID to delete");
                        if (InputHelper.Confirm($"delete carrier proposed assignment {did}"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Deleting...[/]", async _ =>
                            {
                                await _client.CarrierProposedAssignments.DeleteAsync(did, Timeout60s());
                            });
                            ResultRenderer.RenderSuccess("Carrier proposed assignment deleted.");
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Training ──────────────────────────────────────────────────────────────

    private async Task TrainingMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Training", "List Assignments", "List Courses");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List Assignments":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching training assignments...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Training.ListAssignmentsAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Training Assignments", a => [a.Id, a.DriverId ?? "", a.CourseId ?? "", a.Status ?? ""], ["ID", "Driver ID", "Course ID", "Status"]);
                        });
                        break;
                    case "List Courses":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching training courses...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Training.ListCoursesAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Training Courses", c => [c.Id, c.Name ?? ""], ["ID", "Name"]);
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Issues ────────────────────────────────────────────────────────────────

    private async Task IssuesMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Issues", "List All", "Get by ID", "Update");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching issues...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Issues.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Issues", i => [i.Id, i.Title ?? "", i.Status ?? ""], ["ID", "Title", "Status"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Issue ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching issue...[/]", async _ =>
                        {
                            var i = await _client.Issues.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(i, $"Issue {id}");
                        });
                        break;
                    case "Update":
                        var uid = InputHelper.AskId("Issue ID to update");
                        var uReq = DeserializePrompt<UpdateIssueRequest>("UpdateIssueRequest");
                        if (uReq != null && InputHelper.Confirm("update issue"))
                        {
                            await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Updating...[/]", async _ =>
                            {
                                var i = await _client.Issues.UpdateAsync(uid, uReq, Timeout60s());
                                ResultRenderer.RenderObject(i, "Updated Issue");
                            });
                        }
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }

    // ── Media ─────────────────────────────────────────────────────────────────

    private async Task MediaMenuAsync()
    {
        while (true)
        {
            var op = SubMenu("Media", "List All", "Get by ID");
            if (op == "← Back") return;
            try
            {
                switch (op)
                {
                    case "List All":
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching media files...[/]", async _ =>
                        {
                            var items = await CollectAsync(_client.Media.ListAsync(Timeout60s()));
                            ResultRenderer.RenderList(items, "Media Files", m => [m.Id, m.MediaType ?? "", m.VehicleId ?? ""], ["ID", "Type", "Vehicle ID"]);
                        });
                        break;
                    case "Get by ID":
                        var id = InputHelper.AskId("Media File ID");
                        await AnsiConsole.Status().Spinner(Spinner.Known.Dots).StartAsync("[yellow]Fetching media file...[/]", async _ =>
                        {
                            var m = await _client.Media.GetAsync(id, Timeout60s());
                            ResultRenderer.RenderObject(m, $"Media File {id}");
                        });
                        break;
                }
            }
            catch (Exception ex) { ResultRenderer.RenderError(ex); }
        }
    }
}
