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

if (IsValidCommand('Get-WmiObject') -or $IsWindows)
{
    echo "Detected integrated powershell or powershell core on windows."
    echo "xcopy will be used."

    xcopy /Y /S /I /D `"$src`" `"$dst`"
}
elseif ($IsLinux)
{
    echo "Detected powershell core on linux."
    echo "mkdir & cp will be used."

    mkdir -p `"$dst`"
    cp -aru `"$src/.`" `"$dst`"
}
elseif ($IsOsX)
{
    echo "Detected powershell core on macOS."
    echo "rsync will be used."

    mkdir -p `"$dst`"
    rsync -r -u `"$src/`" `"$dst`"
}
