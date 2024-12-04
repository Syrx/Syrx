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


foreach ($repo in $repositories) {
    Write-Host "Processing repository: $repo"
	
	$path = "$repository_root$repo"
    # Change to the repository directory
    Set-Location -Path $path
	
	# sbi is the alias for branch-info.ps1
    $info += & sbi -path $path

    # Return to the original directory
    Set-Location -Path $PSScriptRoot
	
}

# Print branch information as a table 
$info | Format-Table -Property Repo, Branch, Altered, Ahead, Behind -AutoSize


Write-Host ""
Write-Host "Done!"
