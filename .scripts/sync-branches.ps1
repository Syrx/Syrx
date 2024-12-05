# -------------------------------------------------------------------------------------------------
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : creates new local branches across the different Syrx projects. 
#			: can be called directly or aliased if you prefer. 
# usage ... : sync-branches.ps1 -branch 2.4.0 
# aliases . : syrx-cb -branch 2.4.0
# -------------------------------------------------------------------------------------------------
param ( 
	[Parameter(Mandatory=$true)] 
	[string]$branch,

	[Parameter(Mandatory=$false)] 
	[string]$pushToOrigin = "Y",

	[Parameter(Mandatory=$false)] 
	[string]$promptOnLoop = "N"
	
	)

$repository_name = "Syrx.Npgsql"
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

Write-Host "Checking status of branches first... " -ForegroundColor "Green"
# run a quick sanity check against all the repos before we start. 
& syrx-bl 

$response = Prompt-User "Status check complete. Continue?"
Handle-UserResponse -response $response

foreach ($repo in $repositories) {
    Write-Host "Processing repository: $repo"  -ForegroundColor "Green"
	Write-Host "-----------------------------------------------------------------------------"
		
    # Change to the repository directory
    Set-Location -Path $repository_root$repo
	
	if($promptOnLoop -eq "Y"){
		# switch to repo, check that this is what we want. 
		$response = Prompt-User "This script will create and switch to new branch '$branch' for $repo. Do you want to continue?"
		Handle-UserResponse -response $response 
	}
		
	# Switch to main
    Write-Host "Switching to main..."  -ForegroundColor "Blue"
    $result = git switch main 2>&1 | Out-String
	Write-Host $result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to switch to main."
	
	# Fetch the latest changes
    Write-Host "Fetching latest changes..."  -ForegroundColor "Blue"
    $result = git pull origin 2>&1 | Out-String
	Write-Host $result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to fetch the latest changes."

    # Create a new branch
    Write-Host "Creating new branch: $branch"  -ForegroundColor "Blue"
    $result = git checkout -b $branch 2>&1 | Out-String
	Write-Host $result
 	Evaluate-Result -errorcode $LASTEXITCODE -message "Creation and checkout of branch '$branch' failed."
	
	# update submodules
    Write-Host "Creating new branch: $branch"  -ForegroundColor "Blue"
    $result = git submodule update --init --recursive --remote 2>&1 | Out-String
	Write-Host $result
 	Evaluate-Result -errorcode $LASTEXITCODE -message "Creation and checkout of branch '$branch' failed."
		
	if($pushToOrigin -eq "Y") {
		# Push the new branch to the remote repository
		Write-Host "Pushing new branch to remote..."  -ForegroundColor "Blue"
		$result = git push -u origin $branch --set-upstream --progress 2>&1 | Out-String
		Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to push to origin."	
	}	
		
    # Return to the original directory
    Set-Location -Path $repository_root
	
    Write-Host "-----------------------------------------------------------------------------"
    Write-Host "Finished processing repository: $repo"  -ForegroundColor "Green"
    Write-Host "-----------------------------------------------------------------------------"
}

# Print branch information as a table 
#$branchInfo | Format-Table -Property Repository, Branches, AlteredFiles -AutoSize
Write-Host "-----------------------------------------------------------------------------"
Write-Host "Re-checking lists... "
    
# run a check against all the repos again. 
& syrx-bl 

Write-Host "-----------------------------------------------------------------------------"

Write-Host ""
Write-Host "Done!"
