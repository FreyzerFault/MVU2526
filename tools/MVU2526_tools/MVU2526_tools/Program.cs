using System.Diagnostics;

Console.WriteLine("Inicio de la Aplicación de Consola");

// ===================================================================================================================
// Invocar a Unity desde el CMD

// COMANDO para CMD:
// "C:\Program Files\Unity\Hub\Editor\6000.3.11f1\Editor\unity.exe" -batchmode -projectPath "C:\Unity\MVU2526\MVU2526 - Fork" -buildWindows64Player "C:\Unity\MVU2526\MVU2526 - Fork\Build\Windows\Game.exe" -quit

string unityEditorPath = Environment.GetEnvironmentVariable("MVU2526_UNITY") ?? @"C:\Program Files\Unity\Hub\Editor\6000.3.11f1\Editor\unity.exe";
string projectPath = Environment.GetEnvironmentVariable("MVU2526_PROJECT") ?? @"C:\Unity\MVU2526";
string unityProjectPath = Path.Combine(projectPath, @"\MVU2526 - Fork");
string buildPath = Path.Combine(projectPath, @"\MVU2526 - Fork\Build\Windows\Game.exe");

const bool batchMode = true;

string arguments = $"{(batchMode ? "-batchmode" : "")} -projectPath \"{unityProjectPath}\" -buildWindows64Player \"{buildPath}\" -quit";

Console.WriteLine($"Ejecutando comando\n´{unityEditorPath} {arguments}");

Process buildProcess = Process.Start(unityEditorPath, arguments);

buildProcess.WaitForExit();

// ===================================================================================================================
