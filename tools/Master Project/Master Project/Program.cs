using System.Diagnostics;

Console.WriteLine("Inicio de la Aplicación de Consola");

// ===================================================================================================================
// Invocar a Unity desde el CMD

// COMANDO para CMD:
// "C:\Program Files\Unity\Hub\Editor\6000.3.11f1\Editor\unity.exe" -batchmode -projectPath "C:\Unity\MVU2526\MVU2526 - Fork" -buildWindows64Player "C:\Unity\MVU2526\MVU2526 - Fork\Build\Windows\Game.exe" -quit

string editorExePath = @"C:\Program Files\Unity\Hub\Editor\6000.3.11f1\Editor\unity.exe";
string projectPath = @"C:\Unity\MVU2526\MVU2526 - Fork";
string buildPath = @"C:\Unity\MVU2526\MVU2526 - Fork\Build\Windows\Game.exe";

string command = $"{editorExePath} -batchmode -projectPath {projectPath} -buildWindows64Player {buildPath} -quit";
string arguments = $"-batchmode -projectPath \"{projectPath}\" -buildWindows64Player \"{buildPath}\" -quit";

Console.WriteLine($"Ejecutando comando\n´{editorExePath} {arguments}");

Process.Start(editorExePath, arguments);

// ===================================================================================================================
