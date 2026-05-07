using System.Diagnostics;
using System.IO.Compression;
using CG.Web.MegaApiClient;
using Discord.Webhook;
using Master_Project;

Console.WriteLine("Inicio de la Aplicación de Consola");


// ===================================================================================================================
// Invocar a Unity desde el CMD

// COMANDO para CMD:
// "C:\Program Files\Unity\Hub\Editor\6000.3.11f1\Editor\unity.exe" -batchmode -projectPath "C:\Unity\MVU2526\MVU2526 - Fork" -buildWindows64Player "C:\Unity\MVU2526\MVU2526 - Fork\Build\Windows\Game.exe" -quit

string unityEditorPath = Environment.GetEnvironmentVariable("MVU2526_UNITY") ?? @"C:\Program Files\Unity\Hub\Editor\6000.3.11f1\Editor\unity.exe";
string projectPath = Environment.GetEnvironmentVariable("MVU2526_PROJECT") ?? @"C:\Unity\MVU2526";

string unityProjectPath = Path.Combine(projectPath, @"MVU2526 - Fork");
string buildFolder = Path.Combine(projectPath, @"MVU2526 - Fork\Build\Windows\Game");
string buildPath = Path.Combine(buildFolder, @"Game.exe");
string zipFilePath = Path.Combine(buildFolder, "../Game.zip");

const bool batchMode = true;

string arguments = $"{(batchMode ? "-batchmode" : "")} -projectPath \"{unityProjectPath}\" -buildWindows64Player \"{buildPath}\" -quit";


Console.WriteLine($"Building Game!!\nComando: {unityEditorPath} {arguments}");

Commands.CommandExecutionResult result = Commands.RunCommand(unityEditorPath, arguments);

// ===================================================================================
// DISCORD Webhook

// ESTE WEBHOOK NO FUNCIONA. Es un Placeholder

try
{
    DiscordWebhookClient discordClient = new(Secrets.discordWebHook);

    if (result.exitCode != 0)
        await discordClient.SendMessageAsync(
            $"No se compiló correctamente.\n```stdout:\n {result.stdout}\nstderr:\n{result.stderr}\n```");
    else
        await discordClient.SendMessageAsync("Sa compilao jijijijijiji");
}
catch (Exception e)
{
    Console.WriteLine("Can't Connect to Discord Webhook");
    if (result.exitCode != 0)
    {
        Console.WriteLine($"No se compiló correctamente.\n```stdout:\n {result.stdout}\nstderr:\n{result.stderr}\n```");
        return 1;
    }

    Console.WriteLine($"Sa compilao jijijijijiji");
}

// ===================================================================================================================
// ZIP File

if (File.Exists(zipFilePath))
    File.Delete(zipFilePath);

ZipFile.CreateFromDirectory(buildFolder, zipFilePath);

Console.WriteLine($"Compressed Game to ZIP: {zipFilePath}");

// ===================================================================================================================
// MEGA
// NO FUNCIONA porque pide el código 2FA

Mega.UploadToMega(zipFilePath);

return 0;

// ===================================================================================================================
