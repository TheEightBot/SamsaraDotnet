using System.Text.Json;
using Spectre.Console;

namespace Samsara.Cli.Helpers;

/// <summary>
/// Renders SDK results to the terminal using Spectre.Console.
/// </summary>
internal static class ResultRenderer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
    };

    public static void RenderObject(object? obj, string title)
    {
        var json = JsonSerializer.Serialize(obj, JsonOptions);
        AnsiConsole.Write(
            new Panel(new Text(json))
                .Header($"[blue]{title.EscapeMarkup()}[/]")
                .Border(BoxBorder.Rounded));
    }

    public static void RenderList<T>(
        IReadOnlyList<T> items,
        string title,
        Func<T, string[]> rowSelector,
        string[] headers)
    {
        if (items.Count == 0)
        {
            AnsiConsole.MarkupLine($"[yellow]No {title.EscapeMarkup()} found.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title($"[bold]{title.EscapeMarkup()}[/] ({items.Count} items)");

        foreach (var header in headers)
            table.AddColumn(new TableColumn(header).NoWrap());

        foreach (var item in items)
        {
            var row = rowSelector(item);
            table.AddRow(row.Select(c => c.EscapeMarkup()).ToArray());
        }

        AnsiConsole.Write(table);
    }

    public static void RenderError(Exception ex)
    {
        AnsiConsole.Write(
            new Panel($"[red]{ex.Message.EscapeMarkup()}[/]")
                .Header("[red]Error[/]")
                .Border(BoxBorder.Heavy));
    }

    public static void RenderSuccess(string message)
    {
        AnsiConsole.MarkupLine($"[green]✓ {message.EscapeMarkup()}[/]");
    }
}
