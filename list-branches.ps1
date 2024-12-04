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
$repoInfo = @()

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

	# Capture the branch list and altered files count for each branch 
	Write-Host "Capturing branch list and altered files count..." 
	$branches = (git branch -l 2>&1 | Out-String) -split "`n" 
	$branchDetails = @() 
	$currentBranch = (git rev-parse --abbrev-ref HEAD).Trim() 
	foreach ($branch in $branches) { 
		$branchName = $branch.Trim().Replace("* ", "") 
		if ($branchName) { 
			git switch $branchName 2>&1 | Out-String 
			$status = git status --porcelain 2>&1 | Out-String 
			$alteredFilesCount = ($status -split "`n").Count 
			$branchDetails += "$branchName ($alteredFilesCount)" 
			} 
		} 
	
	# Reapply the asterisk to the current branch 
	$branchDetails = $branchDetails -replace "^\s*$currentBranch\s*\(\d+\)", "* $currentBranch ($alteredFilesCount)" 
	
	# Store the information 
	$repoInfo += [PSCustomObject]@{ 
		Repository = $repo 
		Branches = $branchDetails -join ", " 
	}

    # Return to the original directory
    Set-Location -Path $PSScriptRoot
	
}

# Print branch information as a table 
$repoInfo | Format-Table -Property Repository, Branches -AutoSize

Write-Host ""
Write-Host "Done!"
