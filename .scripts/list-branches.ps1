# -------------------------------------------------------------------------------------------------
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : gathers branch info for each of the different repos. 
# usage ... : list-branches.ps1 
# aliases . : syrx-bl 
#			: sbl
# -------------------------------------------------------------------------------------------------
param ( 
	[Parameter(Mandatory=$false, HelpMessage="Interrogates the path for branch info. Defaults to C:\Projects\")]
	[string]$root = "C:\Projects\", 
	[Parameter(Mandatory=$false, HelpMessage="Determines the output type of the result. 1 = Table output (default). Any other value will return PSCustomObject.")]
	[int]$output = 1
)
# Array of repositories
$repositories = @(
"Syrx",
"Syrx.Commanders.Databases",
"Syrx.MySql",
"Syrx.Npgsql",
"Syrx.Oracle",
"Syrx.SqlServer"
)

# initalize array. 
$info = @()
 
foreach ($repo in $repositories) {
    Write-Host "Processing repository: " -NoNewLine
	Write-Host 	"$repo" -foregroundcolor green
	
	$path = "$root$repo"
    
	# Change to the repository directory
    Set-Location -Path $path
	
	# sbi is the alias for branch-info.ps1
    $info += & sbi -path $path
}

Set-Location -Path $repository_root

if($output -eq 1) {
	# Print branch information as a table 
	$info | Format-Table -Property Repo, Branch, Altered, Ahead, Behind -AutoSize
} else {
	return $info
}

Write-Host ""
