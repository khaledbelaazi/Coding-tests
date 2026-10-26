[CmdletBinding(DefaultParameterSetName = 'Un')]
param(
    [Parameter(ParameterSetName = 'Un')]
    [ValidateRange(1, 16)]
    [int]$Tutoriel = 1,

    [Parameter(Mandatory, ParameterSetName = 'Tous')]
    [switch]$Tous
)

$ErrorActionPreference = 'Stop'
$documents = @(Get-ChildItem -Path $PSScriptRoot -Filter '*.md' |
    Where-Object { $_.Name -match '^\d{2}_' } | Sort-Object Name)
if (-not $Tous) {
    $prefixe = '{0:00}_' -f $Tutoriel
    $documents = @($documents | Where-Object { $_.Name.StartsWith($prefixe) })
}
if ($documents.Count -eq 0) { throw 'Tutoriel introuvable.' }

$atelier = Join-Path ([System.IO.Path]::GetTempPath()) ('TutorielCSharp-' + [guid]::NewGuid())
$projet = Join-Path $atelier 'Atelier.csproj'
$programme = Join-Path $atelier 'Program.cs'
$configuration = @'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
'@

try {
    $null = [System.IO.Directory]::CreateDirectory($atelier)
    [System.IO.File]::WriteAllText($projet, $configuration)
    foreach ($document in $documents) {
        $contenu = Get-Content -LiteralPath $document.FullName -Raw
        $exemple = [regex]::Match($contenu, '(?ms)^```csharp[ \t]*\r?\n(?<code>.*?)^```[ \t]*\r?$')
        if (-not $exemple.Success) { throw "Exemple C# absent : $($document.Name)" }
        [System.IO.File]::WriteAllText($programme, $exemple.Groups['code'].Value)
        Write-Host "`n=== $($document.BaseName) ==="
        dotnet run --project $projet --configuration Release --verbosity quiet
        if ($LASTEXITCODE -ne 0) { throw "Echec de l'exemple : $($document.Name)" }
    }
    Write-Host "`n$($documents.Count) exemple(s) execute(s) sans erreur."
}
finally {
    if (Test-Path -LiteralPath $atelier) {
        Remove-Item -LiteralPath $atelier -Recurse -Force
    }
}