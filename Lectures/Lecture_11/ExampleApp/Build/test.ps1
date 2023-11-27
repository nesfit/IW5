. .\Build\settings.ps1

$testResults = Join-Path $global:outputDir "\TestResults\dotnetTests.trx"
$resultsFullPath = [System.IO.Path]::GetFullPath($testResults)
$configuration = $env:BuildConfiguration
$testsDll = ".\Output\$configuration\Tests\net7.0\Calculator.Engine.Tests.dll"
dotnet vstest $testsDll /logger:"trx;LogFileName=$resultsFullPath" --nologo