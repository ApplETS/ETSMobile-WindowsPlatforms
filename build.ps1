Param(
    [string]$Script = "build.cake",
    [string]$Target = "Default",
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [ValidateSet("Quiet", "Minimal", "Normal", "Verbose", "Diagnostic")]
    [string]$Verbosity = "Normal",
    [switch]$WhatIf
)

$TOOLS_DIR = Join-Path $PSScriptRoot "tools"

$NUGET_EXE = Join-Path $TOOLS_DIR "nuget.exe"
$CAKE_EXE = Join-Path $TOOLS_DIR "Cake/Cake.exe"

# Should we use experimental build of Roslyn?
#$UseExperimental = "";
#if($Experimental.IsPresent) {
#    $UseExperimental = "-experimental"
#}

# Is this a dry run?
$UseDryRun = "";
if($WhatIf.IsPresent) {
    $UseDryRun = "-dryrun"
}

# Does the tool folder exist?
if(!(Test-Path $TOOLS_DIR)) {
   New-Item -ItemType directory -Path ($PSScriptRoot + "/tools")
}

# Try download NuGet.exe if do not exist.
if (!(Test-Path $NUGET_EXE)) {
    Invoke-WebRequest -Uri http://nuget.org/nuget.exe -OutFile $NUGET_EXE
}

# Make sure NuGet exists where we expect it.
if (!(Test-Path $NUGET_EXE)) {
    Throw "Could not find NuGet.exe"
}


Push-Location
Set-Location $TOOLS_DIR
$nugetExecutable = $NUGET_EXE
$nugetExecutableArgs = @('install', '-ExcludeVersion')
& $nugetExecutable $nugetExecutableArgs
Pop-Location
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

# Make sure that Cake has been installed.
if (!(Test-Path $CAKE_EXE)) {
    Throw "Could not find Cake.exe"
}

# Start Cake
$startCakeCommand = $CAKE_EXE
$startCakeScriptFile = "$Script"
$startCakeCommandArgs = @("-target=$Target", "-configuration=$Configuration", "-verbosity=$Verbosity", $UseDryRun)
& $startCakeCommand $startCakeScriptFile $startCakeCommandArgs
Write-Host
exit $LASTEXITCODE