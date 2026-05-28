using System.Diagnostics;

namespace Master_Project;

public static class Commands
{
    public static CommandExecutionResult RunCommand(string unityPath, string arguments)
    {
        ProcessStartInfo processInfo = new()
        {
            FileName = unityPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        Process buildProcess = new() { StartInfo = processInfo };

        buildProcess.Start();
        buildProcess.WaitForExit();

        return new CommandExecutionResult
        {
            exitCode =  buildProcess.ExitCode,
            stdout = buildProcess.StandardOutput.ReadToEnd(),
            stderr = buildProcess.StandardError.ReadToEnd()
        };
    }

    public class CommandExecutionResult
    {
        public int exitCode;
        public string stdout;
        public string stderr;
    }

}
