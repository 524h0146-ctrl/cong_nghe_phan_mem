Set-Location $PSScriptRoot
dotnet ef migrations add InitialSchool --project Mvc --startup-project Mvc
if ($LASTEXITCODE) { exit $LASTEXITCODE }
dotnet ef database update --project Mvc --startup-project Mvc
