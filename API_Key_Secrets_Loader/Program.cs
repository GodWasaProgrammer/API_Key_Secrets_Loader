namespace API_Key_Secrets_Loader;

internal class Program
{
    static void Main(string[] args)
    {
        var Dir = Directory.GetCurrentDirectory();
        for (int i = 0; i < 3; i++)
        {
            Dir = Path.GetDirectoryName(Dir);
        }

        // Define paths
        string ApiKeys = $"{Dir}/API_Keys.txt";
        string ConsumerKeys = $"{Dir}/Consumer_Keys";
        string ConsumerSecrets = $"{Dir}/Consumer_Secrets";
        string AccessToken = $"{Dir}/Access_Token";
        string AccessSecret = $"{Dir}/Access_Secret";

        // create if it doesnt exist
        List<string> LoadList = new() { ApiKeys, ConsumerKeys, ConsumerSecrets, AccessToken, AccessSecret };
        foreach (string file in LoadList)
        {
            if (!File.Exists(file))
            {
                File.Create(file).Dispose(); // Stäng strömmen direkt
            }
        }
        // load your local keys, Do NOT push these to github
        Dictionary<string, string> Api_Keys = LoadApiKeys(ApiKeys);
        Dictionary<string, string> Consumer_Keys = LoadApiKeys(ConsumerKeys);
        Dictionary<string, string> Consumer_Secrets = LoadApiKeys(ConsumerSecrets);
        Dictionary<string, string> Access_Tokens = LoadApiKeys(AccessToken);
        Dictionary<string, string> Access_Secrets = LoadApiKeys(AccessSecret);
    }

    static Dictionary<string, string> LoadApiKeys(string filePath)
    {
        var apiKeys = new Dictionary<string, string>();

        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                // Ignorera tomma rader eller rader utan '='
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                    continue;

                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        apiKeys[key] = value;
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: File not found at {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading API keys: {ex.Message}");
        }

        return apiKeys;
    }
}