$content = Get-Content -Path "..\README-template.md" -Raw
$git = "You can use it in your project from [![NuGet version (Mintzat.Email)](https://img.shields.io/nuget/v/Mintzat.Email.svg?style=flat-square)](https://www.nuget.org/packages/Mintzat.Email/)"
$nuget = "You can see the repo here [![Badge Name](https://img.shields.io/badge/GitHub-Mintzat.Email-blue.svg)](https://github.com/minkostaev/Mintzat.Email)"
$content -replace '---link---', $git | Set-Content -Path "..\README.md"
$content -replace '---link---', $nuget | Set-Content -Path "..\README-nuget.md"
