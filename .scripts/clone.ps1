# -------------------------------------------------------------------------------------------------
# script .. : syrx project version sync. 
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : creates new local branches across the different Syrx projects. 
# usage ... : remove-branches.ps1 -branch 2.4.0 
# -------------------------------------------------------------------------------------------------
param ( 
	[Parameter(Mandatory=$true)] [string]$path,
	[Parameter(Mandatory=$false)][bool]$remove = $false,
	[Parameter(Mandatory=$false)][bool]$force = $false
	)

#$repository_name = "Syrx"
$repository_url = "https://github.com/Syrx/$repository_name"
$repository_root = $path ??  "C:\Projects\"

# Array of repositories
$repositories = @(
"Syrx",
"Syrx.Commanders.Databases",
"Syrx.MySql",
"Syrx.Npgsql",
"Syrx.Oracle",
"Syrx.SqlServer"
)

# Array to store branch information 
$branchInfo = @()

function Prompt-User {
    param (
        [string]$message,
		[string]$colour = "White",
		[string]$bgcolour = "Black"
		
    )
    do {
		Write-Host "$message (Y/N/A)"-ForegroundColor $colour -BackgroundColor $bgcolour -NoNewline
        $response = Read-Host 
    } while ($response -notin @('Y', 'N', 'A'))
    return $response
}


function Handle-UserResponse { 
	param ( 
		[string]$response ) 
	if ($response -eq 'A') { break } 
	if ($response -eq 'N') { continue } 
}

function Evaluate-Result {
	param (
		[int] $errorcode,
		[string]$message
	)
	
	if($errorcode -ne 0) {
		$response = Prompt-User "$message Continue?" -colour "Red"
		Handle-UserResponse -response $response 
	}
}

function Check-Path {
		param ([Parameter(Mandatory=$true)][string]$DestinationPath)
		
		# Check if the destination path exists
		if (Test-Path -Path $DestinationPath) {
			# Check if the directory is not empty
			if ((Get-ChildItem -Path $DestinationPath).Count -gt 0) {
				$response = Prompt-User "The folder '$DestinationPath' already exists and is not empty. Do you want to continue with the cloning? " -colour "Yellow"
				Handle-UserResponse -response $response
		
		# list files so that user can see which files are affected
		ls $DestinationPath
		
		#Set-Location $PSScriptRoot
		
		# if the next bit executes, it means we said Y
		Write-Host ""
		Write-Host "Removing directory $DestinationPath will delete ALL these files and folders! This is not reversible!" -ForegroundColor "Red"
		
		$response = Prompt-User "Are you SURE you want to continue? " -colour "Red" 
		Handle-UserResponse -response $response
		rmdir -force $DestinationPath
		}
	} else {
    # Create the directory if it does not exist
    New-Item -ItemType Directory -Path $DestinationPath
	}
}


foreach ($repository_name in $repositories) {
	
    Write-Host "Cloning repository: " -NoNewline
	Write-host "$repository_name" -ForegroundColor "Blue"
		
    # Change to the repository directory
    Set-Location -Path $repository_root
	
	$path = "$repository_root$repository_name"
	Check-Path -DestinationPath $path
	
	Write-Host "Repository URL:  $repository_url$repository_name"	
		
	$source = "$repository_url$repository_name"	
	$result = git clone $source $repository_name  2>&1 | Out-String 
	Write-Host $result
	Evaluate-Result -errorcode $LASTEXITCODE -message "Clone failed."
	
    # Return to the original directory
    Set-Location -Path $PSScriptRoot
	
    Write-Host "Finished cloning repository: $repository_name"
}

#& ".\list-branches.ps1"

# Return to the original directory
Set-Location -Path $repository_root

Write-Host ""
Write-Host "Done!"
