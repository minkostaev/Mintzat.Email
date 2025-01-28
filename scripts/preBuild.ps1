$template = Get-Content -Path "..\scripts\README-template.md" -Raw

$gitTxt = "You can use it in your project from [![NuGet version (Mintzat.Email)](https://img.shields.io/nuget/v/Mintzat.Email.svg?style=flat-square)](https://www.nuget.org/packages/Mintzat.Email/)"
$nugetTxt = "You can see the repo here [![Badge Name](https://img.shields.io/badge/GitHub-Mintzat.Email-blue.svg)](https://github.com/minkostaev/Mintzat.Email)"

$git = $template -replace '---link---', $gitTxt -replace '---tutorial---', "[tutorial](/scripts/Resend.md)"
$nuget = $template -replace '---link---', $nugetTxt -replace '---tutorial---', ""


$git | Set-Content -Path "..\README.md"
$nuget | Set-Content -Path "..\scripts\README-nuget.md"
