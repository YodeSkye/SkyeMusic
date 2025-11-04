[Setup]
AppName=Skye Music
AppVersion=1.0
AppVerName=Skye Music v1.0
DefaultDirName={commonpf64}\Skye\Skye Music
ArchitecturesInstallIn64BitMode=x64compatible
DisableProgramGroupPage=yes
OutputDir=.
OutputBaseFilename=SkyeMusicSetup

[Files]
Source: "bin\Release\net9.0-windows7.0\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
; Start Menu shortcut (root, no subfolder)
Name: "{commonprograms}\Skye Music"; Filename: "{app}\SkyeMusic.exe"

; Optional desktop shortcut
Name: "{commondesktop}\Skye Music"; Filename: "{app}\SkyeMusic.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"