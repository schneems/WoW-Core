# Copyright (c) Arctium Emulation.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.

param
(
    [parameter(Mandatory = $true)]
    [string]$src,
    [parameter(Mandatory = $true)]
    [string]$dst
)

function IsValidCommand($cmdName)
{
    return [bool](Get-Command -Name $cmdName -ErrorAction SilentlyContinue)
}

# Check if we are on the windows integrated powershell.
if (IsValidCommand('Get-WmiObject') -and ($IsWindows -eq $NULL))
{
    echo "Detected windows integrated powershell."
    echo "xcopy will be used."
	
    xcopy /Y /S /I /D `"$src`" `"$dst`"
}
elseif ($IsLinux)
{
    echo "Detected powershell core on linux."
    echo "mkdir & cp will be used."
    
    mkdir -p `"$dst`"
    cp -r -u `"$src/`" `"$dst`"
}
elseif ($IsOsX)
{
    echo "Detected powershell core on macOS."
    echo "rsync will be used."

    rsync -r -u `"$src/`" `"$dst`"
}
