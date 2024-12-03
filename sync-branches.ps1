# -------------------------------------------------------------------------------------------------
# script .. : syrx project version sync. 
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : creates new local branches across the different Syrx projects. 
# usage ... : sync-branches.ps1 --new-branch 2.4.0 --remove-branch 2.3.0
# -------------------------------------------------------------------------------------------------
param ( [Parameter(Mandatory=$true)] [string]$branch )

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
	Write-Host "-----------------------------------------------------------------------------"
		
    # Change to the repository directory
    Set-Location -Path $repository_root$repo

	# switch to repo, check that this is what we want. 
    $response = Prompt-User "This script will create and switch to new branch '$new_branch' for $repo. Do you want to continue?"
    Handle-UserResponse -response $response 


    # Check the current status
    Write-Host "Checking current status..."
    $git_result = git status 2>&1 | Out-String 
	Write-Host $git_result
	Evaluate-Result -errorcode $LASTEXITCODE -message "Status check failed."
		
	# Check the current local branches
    Write-Host "Checking local branches..."
    $git_result = git branch -l 2>&1 | Out-String  
	Write-Host $git_result	
	Evaluate-Result -errorcode $LASTEXITCODE -message "Local branch check failed."
	
    # Fetch the latest changes
    Write-Host "Fetching latest changes..."
    $git_result = git fetch origin 2>&1 | Out-String
	Write-Host $git_result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to fetch latest changes."
	
	# Switch to main
    Write-Host "Switching to main..."
    $git_result = git switch main 2>&1 | Out-String
	Write-Host $git_result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to switch to main."
	
	# Fetch the latest changes
    Write-Host "Fetching latest changes..."
    $git_result = git fetch origin 2>&1 | Out-String
	Write-Host $git_result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to fetch the latest changes."

    # Create a new branch
    Write-Host "Creating new branch: $new_branch"
    $git_result = git checkout -b $new_branch 2>&1 | Out-String
	Write-Host $git_result
 	Evaluate-Result -errorcode $LASTEXITCODE -message "Creation and checkout of branch '$new_branch' failed."
	
	# Create a new branch
    Write-Host "Updating submodules for: $new_branch"
    $git_result = git submodule update --init --recursive --remote 2>&1 | Out-String
	Write-Host $git_result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to update submodules."
	
	# print status and branches again	
    Write-Host "Checking local branches..."
	$git_result = git status 2>&1 | Out-String 
	Write-Host $git_result
	Evaluate-Result -errorcode $LASTEXITCODE -message "Status check failed."
	
    $git_result = git branch -l 2>&1 | Out-String
	Write-Host $git_result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to check local branch list."

    # Push the new branch to the remote repository
    $response = Prompt-User "Push new branch to remote?"
	Handle-UserResponse -response $response 
	Write-Host "Pushing new branch to remote..."
    $git_result = git push -u origin $new_branch 2>&1 | Out-String
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to push to origin."

    # Return to the original directory
    Set-Location -Path $PSScriptRoot
	
    Write-Host "-----------------------------------------------------------------------------"
    Write-Host "Finished processing repository: $repo"
    Write-Host "-----------------------------------------------------------------------------"
}

Write-Host "Done!"
