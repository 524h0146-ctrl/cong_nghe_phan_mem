$ErrorActionPreference='Stop'
Set-Location $PSScriptRoot
dotnet test Tests/LabTests.csproj --results-directory TestResults --logger "trx;LogFileName=Lab09.trx"
$exitCode=$LASTEXITCODE
if (!(Test-Path TestResults/Lab09.trx)) {throw "Missing TRX output"}
[xml]$trx=Get-Content TestResults/Lab09.trx -Raw
$dest=Join-Path "TestResults" ("allure-"+[guid]::NewGuid().ToString())
New-Item -ItemType Directory $dest | Out-Null
foreach($r in $trx.SelectNodes("//*[local-name()='UnitTestResult']")){
 $id=[guid]::NewGuid().ToString()
 $status=switch($r.outcome){'Passed'{'passed'} 'Failed'{'failed'} default{'skipped'}}
 $data=@{uuid=$id;name=$r.testName;fullName=$r.testName;status=$status;stage='finished';statusDetails=@{message=[string]$r.Output.ErrorInfo.Message}}
 [IO.File]::WriteAllText((Join-Path (Resolve-Path $dest) "$id-result.json"),($data|ConvertTo-Json -Depth 4))
}
allure generate $dest -o "$dest-report"
exit $exitCode
