# -------------------------------------------------------------------------------------------------
# script .. : syrx project version sync. 
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : creates new local branches across the different Syrx projects. 
# usage ... : remove-branches.ps1 -branch 2.4.0 
# -------------------------------------------------------------------------------------------------
param ( )

$repository_name = "Syrx"
$repository_url = "https://github.com/Syrx/$repository_name"
$repository_root = "C:\Projects\"

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
        [string]$message
    )
    do {
        $response = Read-Host "$message (Y/N/A)"
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
		$response = Prompt-User "$message Continue?" 
		Handle-UserResponse -response $response 
	}
	
}

foreach ($repo in $repositories) {
    Write-Host "Processing repository: $repo"
		
    # Change to the repository directory
    Set-Location -Path $repository_root$repo

	# Capture the branch list 
	$branchList = git branch -l 2>&1 | Out-String 
	
	# Capture the git status 
	$status = git status 2>&1 | Out-String
	$alteredFilesCount = ($status -split "`n").Count
		
	$branchInfo += [PSCustomObject]@{ 
		Repository = $repo 
		Branches = $branchList -replace '\r?\n', ', '
		Status = $status -replace '\r?\n', ', '
		AlteredFiles = $alteredFilesCount
	}

    # Return to the original directory
    Set-Location -Path $PSScriptRoot
	
    Write-Host "-----------------------------------------------------------------------------"
    Write-Host "Finished processing repository: $repo"
	Write-Host "-----------------------------------------------------------------------------"
	
}

# Print branch information as a table 
$branchInfo | Format-Table -Property Repository, Branches, AlteredFiles -AutoSize

Write-Host ""
Write-Host "Done!"
