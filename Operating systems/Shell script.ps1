$Dir = Read-Host "Set Current Directory"
$Ext = Read-Host "Set File Extension"
$Size = Read-Host "Set minimal File Size"
Get-ChildItem $Dir -Filter *.$Ext | Where-Object {$_.Length -gt $Size} | % { $_.Delete() }
$SysPause = Read-Host "Press any key"
