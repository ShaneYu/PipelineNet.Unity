<#
$version = Get-Date -Format "mmddyyyy-HHmm"
Update-AppveyorBuild -Version "1.0-$version"

if ($env:APPVEYOR_REPO_TAG -eq "true")
{
    Update-AppveyorBuild -Version "$($env:APPVEYOR_REPO_TAG_NAME.TrimStart("v"))"
}
#>

$version = Get-Date -Format "mmddyyyy-HHmm"
Update-AppveyorBuild -Version "1.0-$version"