param(
    [string]$Root = (Split-Path -Parent $PSScriptRoot)
)

$source = Join-Path $Root 'BlazorKinetix.Components'
$target = Join-Path $Root 'BlazorKinetix.Demo/Components/Pages/DocsParameters.g.cs'
$entries = [ordered]@{}

function Escape-CSharp([string]$value) {
    return $value.Replace('"', '""')
}

Get-ChildItem -LiteralPath $source -Recurse -Filter '*.razor' |
    Where-Object { $_.Name -ne '_Imports.razor' } |
    ForEach-Object {
        $lines = Get-Content -LiteralPath $_.FullName
        $rows = [System.Collections.Generic.List[string]]::new()
        $summary = ''
        foreach ($line in $lines) {
            if ($line -match '///\s*<summary>(.*?)</summary>') {
                $summary = $Matches[1].Trim()
                continue
            }
            if ($line -match '\[Parameter(?:,\s*EditorRequired)?\]\s+public\s+(.+?)\s+(\w+)\s*\{\s*get;\s*set;\s*\}') {
                $type = $Matches[1].Trim()
                $name = $Matches[2]
                $description = if ($summary) { $summary } else { "Configures $name." }
                $rows.Add('            new("' + (Escape-CSharp $name) + '", "' + (Escape-CSharp $type) + '", "' + (Escape-CSharp $description) + '"),')
                $summary = ''
            }
        }
        if ($rows.Count -gt 0) { $entries[$_.BaseName] = $rows }
    }

$output = [System.Collections.Generic.List[string]]::new()
$output.Add('// Generated from component [Parameter] declarations and XML summaries.')
$output.Add('namespace BlazorKinetix.Demo.Components.Pages;')
$output.Add('')
$output.Add('internal static class DocsParameters')
$output.Add('{')
$output.Add('    internal sealed record Entry(string Name, string Type, string Description);')
$output.Add('    internal static readonly IReadOnlyDictionary<string, Entry[]> ByComponent = new Dictionary<string, Entry[]>')
$output.Add('    {')
foreach ($key in ($entries.Keys | Sort-Object)) {
    $output.Add('        ["' + (Escape-CSharp $key) + '"] = [')
    foreach ($row in $entries[$key]) { $output.Add($row) }
    $output.Add('        ],')
}
$output.Add('    };')
$output.Add('}')
[System.IO.File]::WriteAllLines($target, $output, [System.Text.UTF8Encoding]::new($false))
Write-Output "Generated $target with $($entries.Count) components."
