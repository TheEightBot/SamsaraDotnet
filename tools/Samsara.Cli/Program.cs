using Microsoft.Extensions.DependencyInjection;
using Samsara.Cli;
using Samsara.Sdk.DependencyInjection;
using Spectre.Console;

// Parse arguments
string? token = null;
string? baseUrl = null;

for (int i = 0; i < args.Length; i++)
{
    if ((args[i] == "--token" || args[i] == "-t") && i + 1 < args.Length)
        token = args[++i];
    else if ((args[i] == "--base-url" || args[i] == "-u") && i + 1 < args.Length)
        baseUrl = args[++i];
}

// Fallback to environment variable
token ??= Environment.GetEnvironmentVariable("SAMSARA_TOKEN");

// Prompt if still not provided
if (string.IsNullOrWhiteSpace(token))
{
    token = AnsiConsole.Prompt(
        new TextPrompt<string>("[yellow]Enter your Samsara API token:[/]")
            .Secret());
}

// Display banner
AnsiConsole.Write(new FigletText("Samsara CLI").Color(Color.Cyan1));
AnsiConsole.Write(new Rule("[bold cyan]Samsara .NET SDK Test Harness[/]").RuleStyle("cyan"));
AnsiConsole.WriteLine();

// Set up DI
var services = new ServiceCollection();
services.AddSamsaraClient(options =>
{
    options.ApiToken = token;
    if (!string.IsNullOrWhiteSpace(baseUrl))
        options.BaseUrl = baseUrl;
});
services.AddScoped<TuiApp>();

await using var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();
var app = scope.ServiceProvider.GetRequiredService<TuiApp>();
await app.RunAsync();
