# -------------------------------------------------------------------------------------------------
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : creates new local branches across the different Syrx projects. 
#			: can be called directly or aliased if you prefer. 
# usage ... : syrx-branch-settings.ps1  
# aliases . : syrx-settings -path $path 
# -------------------------------------------------------------------------------------------------

param(
	[Parameter(Mandatory=$true, HelpMessage="The path to the settings file.")]
    [string]$path,
   
    [Parameter(Mandatory=$true, HelpMessage="Keys of the settings to retrieve.")]
    [string[]]$keys
)

# Read the JSON file content
$jsonContent = Get-Content -Path $path -Raw

# Convert JSON content to a PowerShell object
$jsonObject = $jsonContent | ConvertFrom-Json

# Function to get nested settings
function Get-NestedSetting {
    param (
        [object]$Object,
        [string[]]$KeyPath
    )
    $current = $Object
    foreach ($key in $KeyPath) {
        if ($current.PSObject.Properties[$key]) {
            $current = $current.$key
        } else {
            return $null
        }
    }
    return $current
}

# Retrieve and display the specified settings
foreach ($key in $Keys) {
    $keyPath = $key -split '\.'
    $value = Get-NestedSetting -Object $jsonObject -KeyPath $keyPath
    if ($value) {
        Write-Host "$key $value"
    } else {
        Write-Host "$key Not found"
    }
}


