# SharpQuest: bring your repository up to date on a PC that only has .NET 9.
#
# Run it INSIDE your repository folder (the one with SharpQuest.slnx), in PowerShell:
#
#   irm https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.ps1 | iex
#
# It does what `sq update` does, without needing sq to run first: it downloads the course's
# official files and puts them in place. It never touches src/, tests/ or anything else you wrote.
# Plain ASCII on purpose, so it runs the same under Windows PowerShell 5.1 and PowerShell 7.

$ErrorActionPreference = 'Stop'

if (-not (Test-Path 'SharpQuest.slnx') -or -not (Test-Path 'sharpquest.json')) {
    Write-Host 'Run this inside your SharpQuest repository: the folder with SharpQuest.slnx in it.' -ForegroundColor Yellow
}
else {
    $config = Get-Content 'sharpquest.json' -Raw | ConvertFrom-Json
    $branch = 'main'
    if ($config.branch) { $branch = $config.branch }
    if ("$($config.upstream)" -like '*CHANGE-ME*') {
        Write-Host 'sharpquest.json still says CHANGE-ME. Ask your instructor for the course repository name.' -ForegroundColor Yellow
    }
    else {
        $url = "https://codeload.github.com/$($config.upstream)/zip/refs/heads/$branch"
        $temp = Join-Path ([IO.Path]::GetTempPath()) ('sq-fix-' + [guid]::NewGuid().ToString('N'))
        New-Item -ItemType Directory -Path $temp | Out-Null
        try {
            Write-Host "Downloading $url ..."
            [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
            Invoke-WebRequest -Uri $url -OutFile (Join-Path $temp 'official.zip') -UseBasicParsing
            Expand-Archive -Path (Join-Path $temp 'official.zip') -DestinationPath (Join-Path $temp 'x')
            # GitHub zips hold one top-level folder, e.g. SharpQuest-2026-main/
            $official = (Get-ChildItem (Join-Path $temp 'x') -Directory | Select-Object -First 1).FullName

            # The same list as Repo.Managed in sq.cs.
            $managed = @(
                'contracts', 'reference', 'catchup',
                'Directory.Build.props', 'Directory.Build.targets', 'Directory.Packages.props',
                'global.json', 'sq.cs', 'sq.cmd', 'sq.sh',
                '.github/workflows/ci.yml', 'tools/sq/sq.csproj', '.config/dotnet-tools.json'
            )
            $here = (Get-Location).Path
            foreach ($path in $managed) {
                $from = Join-Path $official $path
                if (-not (Test-Path $from)) { continue }
                $to = Join-Path $here $path
                if (Test-Path $from -PathType Container) {
                    if (Test-Path $to) { Remove-Item $to -Recurse -Force }
                    Copy-Item $from $to -Recurse
                }
                else {
                    New-Item -ItemType Directory -Force -Path (Split-Path $to) | Out-Null
                    Copy-Item $from $to -Force
                }
                Write-Host "  updated $path"
            }
            Write-Host ''
            Write-Host 'Done. Now run:  .\sq test' -ForegroundColor Green
            Write-Host 'Then commit these files along with your own work.'
        }
        finally {
            Remove-Item $temp -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
}
