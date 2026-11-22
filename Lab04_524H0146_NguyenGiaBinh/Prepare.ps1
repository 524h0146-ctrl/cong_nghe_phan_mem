$ErrorActionPreference='Stop'
Set-Location $PSScriptRoot
dotnet build .\BLL\BLL.csproj
if ($LASTEXITCODE) {exit $LASTEXITCODE}
New-Item Site/bin -ItemType Directory -Force | Out-Null
Copy-Item BLL/bin/Debug/net48/*.dll Site/bin -Force
