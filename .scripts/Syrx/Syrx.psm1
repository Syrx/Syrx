# -------------------------------------------------------------------------------------------------
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : reusable functions that are used by different scripts.  
# usage ... : registered the .\scripts\ path to $profile first and then use
#				Import-Module Syrx
# -------------------------------------------------------------------------------------------------

function Confirm-Step {
    param (
        [string]$message,
		[string]$colour = "White",
		[string]$bgcolour = "Black"
		
    )
    do {
		Write-Host "$message" -ForegroundColor $colour -BackgroundColor $bgcolour
		Write-Host "[Y] " -ForegroundColor Green -NoNewLine
		Write-Host "Yes " -NoNewLine
		Write-Host "[N] " -ForegroundColor Yellow -NoNewLine
		Write-Host "No " -NoNewLine
		Write-Host "[Q] " -ForegroundColor Red -NoNewLine
		Write-Host "Quit" 		
        $response = Read-Host 
    } while ($response -notin @('Y', 'N', 'Q'))
    return $response
}

function Format-Path {
    param (
        [string]$path
    )
    if ($path[-1] -ne '\') {
        $path += '\'
    }
    return $path
}


function Confirm-GitResult {
	param (
		[int] $errorcode,
		[string]$message
	)
	if($errorcode -ne 0) {
		$response = Confirm-Step "$message Continue?" -colour "Red"
		#Handle-UserResponse -response $response 
	}
}


function Get-RepositoryNameFromPath{
	param (
		[Parameter(Mandatory=$true, HelpMessage="The path to the folder to check.")]
		[string]$path
	)
	
	# provide a return variable
	$result = ""	
	# Check if the path exists and is a Git repository
	if (Test-Path -Path "$path.git") {
		# Get the repository name from the path
		$result = Split-Path -Leaf $path.TrimEnd('\')
		Write-Debug "The repository name is: $repo"
	} else {
		Write-Host "The specified path is not a Git repository." -ForegroundColor "Red"
		break
	}	
	return $result
}

function Confirm-IsGitRepository{
	param (
		[Parameter(Mandatory=$true, HelpMessage="The path to the folder to check.")]
		[string]$path
	)
	
	$path = Format-Path -Path $path
	$result = Test-Path -Path "$path.git"
	
	if(-not $result){
		Write-Host "The '$path' is not a Git repository." -ForegroundColor "Red"
		#break
	}
	
	return $result
}