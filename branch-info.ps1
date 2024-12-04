# -------------------------------------------------------------------------------------------------
# script .. : gets the branch info for a single repo. 
# author .. : @sextondjc
# date .... : 2024.12.04
# purpose . : provides a quick look at the status of a local branch
# usage ... : branch-info.ps1 -repo "Syrx"
# -------------------------------------------------------------------------------------------------
param(
   [Parameter(Mandatory=$true, HelpMessage="The file path to the Git repository.")]
   [string]$path
)

Write-Debug "Evaluating repo on $path"

# Ensure the path ends with a backslash
function Ensure-TrailingBackslash {
    param (
        [string]$Path
    )
    if ($Path[-1] -ne '\') {
        $Path += '\'
    }
    return $Path
}

$path = Ensure-TrailingBackslash -Path $path

# Check if the path exists and is a Git repository
if (Test-Path -Path "$path.git") {
    # Get the repository name from the path
    $repo = Split-Path -Leaf $path.TrimEnd('\')
    Write-Host "The repository name is: $repo"
} else {
    Write-Host "The specified path is not a Git repository."
}

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
		$aheadBehind = git rev-list --left-right --count origin/$name...$name 2>&1 | Out-String 
		$aheadBehind = $aheadBehind.Trim().Split(" ") 
		$ahead = $aheadBehind[0] 
		$behind = $aheadBehind[1] 
			
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

#$details | Format-Table -Property Repo, Name, Altered, Ahead, Behind -AutoSize

return $details