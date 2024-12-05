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
    Write-Host "Processing repository: " -NoNewLine
	Write-Host "$repo" -ForegroundColor "Green"
		
    # Change to the repository directory
    Set-Location -Path $repository_root$repo
	
	if($promptOnLoop -eq "Y"){
		# switch to repo, check that this is what we want. 
		$response = Prompt-User "This script will create and switch to new branch '$branch' for $repo. Do you want to continue?"
		Handle-UserResponse -response $response 
	}
		
	# Switch to main
    Write-Host "Switching to main..."  -ForegroundColor "Green"
    $result = git switch main 2>&1 | Out-String
	Write-Host $result
    Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to switch to main."
	
    # remove the local branch
    Write-Host "Removing local branch: $branch" -ForegroundColor "Green"
    $result = git branch -d $branch 2>&1 | Out-String
	Write-Host $result
 	Evaluate-Result -errorcode $LASTEXITCODE -message "Removal of branch '$branch' failed."

	$response = Prompt-User "Local branch removed. Delete remote branch as well?"
	Handle-UserResponse -response $response
 
		
	if($pushToOrigin -eq "Y") {
		# Push the new branch to the remote repository
		Write-Host "Removing remote branch $branch ..."
		$result = git push origin :$branch 2>&1 | Out-String
		Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to push to origin."	
		
		# Push the new branch to the remote repository
		Write-Host "Pruning references..."
		$result = git fetch --all --prune 2>&1 | Out-String
		Evaluate-Result -errorcode $LASTEXITCODE -message "Failed to prune."	
	}	
		
    # Return to the original directory
    Set-Location -Path $repository_root
	
    Write-Host "Finished processing repository: $repo"
}

# Print branch information as a table 
#$branchInfo | Format-Table -Property Repository, Branches, AlteredFiles -AutoSize
Write-Host "Re-checking lists... "
    
# run a check against all the repos again. 
& syrx-bl 
