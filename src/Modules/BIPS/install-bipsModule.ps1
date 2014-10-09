$packagePath = $PSScriptRoot | split-path;
$modulePath = $env:psmodulepath -split ';' | select -first 1;
mkdir $modulePath\BIPS -Force
ls $packagePath | copy-item -Destination $modulePath\BIPS -Recurse -verbose