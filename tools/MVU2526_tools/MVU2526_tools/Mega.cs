using CG.Web.MegaApiClient;

namespace Master_Project;

public static class Mega
{
    public static void UploadToMega(string filePath)
    {
        MegaApiClient client = MegaLoginClient();
        string downloadUrl = UploadFileToMega(client, filePath);
        
        Console.WriteLine();
        Console.WriteLine($"Game Uploaded to {downloadUrl}");
    }
    
    private static MegaApiClient MegaLoginClient()
    {
        MegaApiClient megaApiClient = new();
        megaApiClient.Login(Secrets.megaUser, Secrets.megaPassword);
        return megaApiClient;
    }
    
    // Subir un Archivo a Mega
    private static string UploadFileToMega(MegaApiClient megaApiClient, string filePath)
    {
        IEnumerable<INode> nodes = megaApiClient.GetNodes();
        INode root = nodes.Single(x => x.Type == NodeType.Root);
        INode myFile = megaApiClient.UploadFile(filePath, root);

        Uri downloadLink = megaApiClient.GetDownloadLink(myFile);

        return downloadLink.ToString();
    }
}
