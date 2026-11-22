Set-Location $PSScriptRoot
dotnet ef dbcontext scaffold 'Name=ConnectionStrings:School' Microsoft.EntityFrameworkCore.SqlServer --output-dir Generated --context ReverseEngineeredSchool --namespace School.Generated --context-namespace School.Generated --no-onconfiguring --force
