# -------------------------------------------------------------------------------------------------
# author .. : @sextondjc
# date .... : 2024.12.03
# purpose . : gathers branch info for each of the different repos. 
# usage ... : list-branches.ps1 
# aliases . : syrx-bl 
#			: sbl
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

# initalize array. 
$info = @()
 
foreach ($repo in $repositories) {
    Write-Host "Processing repository: $repo"
	
	$path = "$repository_root$repo"
    # Change to the repository directory
    Set-Location -Path $path
	
	# sbi is the alias for branch-info.ps1
    $info += & sbi -path $path
}

Set-Location -Path $repository_root

# Print branch information as a table 
$info | Format-Table -Property Repo, Branch, Altered, Ahead, Behind -AutoSize


Write-Host ""
