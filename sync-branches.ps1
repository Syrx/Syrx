# -------------------------------------------------------------------------------------------------
# script .. : syrx project version sync. 
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : creates new local branches across the different Syrx projects. 
# usage ... : sync-branches.ps1 -branch 2.4.0 
# -------------------------------------------------------------------------------------------------
param ( 
	[Parameter(Mandatory=$true)] 
	[string]$branch,

	[Parameter(Mandatory=$false)] 
	[string]$pushToOrigin = "N",

	[Parameter(Mandatory=$false)] 
	[string]$promptOnLoop = "N"
	
	#[Parameter(Mandatory=$false, Help-Message="Use 'fetch' or 'pull' for updates")] 
	#[string]$mode = "fetch", # fetch or pull
	
	#[Parameter(Mandatory=$false, Help-Message="Switch to main ahead of creating branch.")] 
	#[string]$switchToMain = "Y" # fetch or pull

	)

$repository_name = "Syrx.Npgsql"
$repository_url = "https://github.com/Syrx/$repository_name"
$new_branch = $branch
$remove_branch = "2.3.0"
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

Write-Host "Checking status of branches first... " -ForegroundColor "Green"
& ".\list-branches.ps1"

$response = Prompt-User "Status check complete. Continue?"
Handle-UserResponse -response $response

foreach ($repo in $repositories) {
    Write-Host "Processing repository: $repo"
	Write-Host "-----------------------------------------------------------------------------"
		
    # Change to the repository directory
    Set-Location -Path $repository_root$repo
	
	if($promptOnLoop -eq "Y"){
		# switch to repo, check that this is what we want. 
		$response = Prompt-User "This script will create and switch to new branch '$new_branch' for $repo. Do you want to continue?"
		Handle-UserResponse -response $response 
	}
		
	# Switch to main
    Write-Host "Switching to main..."
    $result = git switch main 2>&1 | Out-String
	Write-Host $result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to switch to main."
	
	# Fetch the latest changes
    Write-Host "Fetching latest changes..."
    $result = git fetch origin 2>&1 | Out-String
	Write-Host $result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to fetch the latest changes."

    # Create a new branch
    Write-Host "Creating new branch: $new_branch"
    $result = git checkout $new_branch 2>&1 | Out-String
	Write-Host $result
 	Evaluate-Result -errorcode $LASTEXITCODE -message "Creation and checkout of branch '$new_branch' failed."
		
	if($pushToOrigin -eq "Y") {
		# Push the new branch to the remote repository
		Write-Host "Pushing new branch to remote..."
		$result = git push -u origin $new_branch --set-upstream --progress 2>&1 | Out-String
		Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to push to origin."
		
		Write-Host "Set head on origin..."	
		$result = git remote set-head -a origin
		Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to push to origin."
		
	}	
		
    # Return to the original directory
    Set-Location -Path $repository_root
	
    Write-Host "-----------------------------------------------------------------------------"
    Write-Host "Finished processing repository: $repo"
    Write-Host "-----------------------------------------------------------------------------"
}

# Print branch information as a table 
#$branchInfo | Format-Table -Property Repository, Branches, AlteredFiles -AutoSize
Write-Host "-----------------------------------------------------------------------------"
Write-Host "Re-checking lists... "
    
& ".\list-branches.ps1"

Write-Host "-----------------------------------------------------------------------------"

Write-Host ""
Write-Host "Done!"
