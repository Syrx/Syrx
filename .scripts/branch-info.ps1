# -------------------------------------------------------------------------------------------------
# script .. : gets the branch info for a single repo. 
# author .. : @sextondjc
# date .... : 2024.12.04
# purpose . : provides a quick look at the status of a local branch. returns a PSCustomObject
# usage ... : branch-info.ps1 -repo "Syrx"
# aliases . : sbi -path "C:\Projects\Syrx" -output "T"
# -------------------------------------------------------------------------------------------------
param(
   [Parameter(Mandatory=$true, HelpMessage="The file path to the Git repository.")]
   [string]$path,
   
   [Parameter(Mandatory=$false, HelpMessage="The output type. T for Table. Anything else for object")]
   [string]$output = "O"
)
# -------------------------------------------------------------------------------------------------
# import our common functions. 
# -------------------------------------------------------------------------------------------------
Import-Module Syrx
# -------------------------------------------------------------------------------------------------
# local variables 
# -------------------------------------------------------------------------------------------------
# store the current location so that we can set back to it later. 
$execute_location = Get-Location


function Process{
}

# Ensure the path ends with a backslash
$path = Format-Path -path $path

# Check if the path exists and is a Git repository
$result = Confirm-IsGitRepository $path
if(-not $result){
	break
}


set-location $path

# get branch information
$branches = (git branch -l 2>&1 | Out-String) -split "`n" 
$details = @() 
$current = (git rev-parse --abbrev-ref HEAD).Trim() 

foreach ($branch in $branches) { 
	$name = $branch.Trim().Replace("* ", "") 
	
	if ($name) {
		#Write-Host "Switching $repo to $branch"
		$switch = git switch -q $name 2>&1 | Out-String 
		$status = git status --porcelain 2>&1 | Out-String 
		$altered = ($status -split "`n").Count 
		
		# Get the number of commits ahead/behind 
		$commits = git rev-list --left-right --count origin/$name...$name 2>&1 | Out-String 
		$commits = $commits.Trim() -split "\s+" 	
		$ahead = $commits[0] 
		$behind = $commits[1] 
			
		if($name -eq $current){
			$name = "* $name"
		}
			
		$details += [PSCustomObject]@{
			Repo = $repo
			Branch = $name
			Altered = $altered
			Ahead = $ahead 
			Behind = $behind
		}				
	} 
}
# set the current branch back
$switch = git switch -q $current

# Return to the original directory
Set-Location -Path $execute_location

if($output -eq "T"){
	$details | Format-Table -Property Repo, Branch, Altered, Ahead, Behind -AutoSize
}
else {
	return $details
}

