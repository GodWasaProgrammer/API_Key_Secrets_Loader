namespace API_Key_Secrets_Loader;

public static class KeyLoader
{
    public static void Main()
    {

    }

    public static Dictionary<string, string> API_Keys = [];
    public static Dictionary<string, string> CONSUMER_Keys = [];
    public static Dictionary<string, string> CONSUMER_Secrets = [];
    public static Dictionary<string, string> ACCESS_Tokens = [];
    public static Dictionary<string,string> ACCESS_Secrets = [];
    public static Dictionary<string,string> CLIENT_Ids = [];
    public static Dictionary<string,string> ACCOUNT_Names = [];

    public static void BuildKeys()
    {
        //var Dir = Directory.GetCurrentDirectory();
        //for (int i = 0; i < 3; i++)
        //{
        //    Dir = Path.GetDirectoryName(Dir);
        //}

        var Dir = "C:/Users/vemha/source/repos/API_Key_Secrets_Loader/API_Key_Secrets_Loader";

        // Define paths
        string ApiKeys = $"{Dir}/API_Keys.txt";
        string ConsumerKeys = $"{Dir}/Consumer_Keys.txt";
        string ConsumerSecrets = $"{Dir}/Consumer_Secrets.txt";
        string AccessToken = $"{Dir}/Access_Token.txt";
        string AccessSecret = $"{Dir}/Access_Secret.txt";
        string ClientIDs = $"{Dir}/Client_Ids.txt";
        string Accounts = $"{Dir}/Accounts.txt";

        // create if it doesnt exist
        //List<string> LoadList = new() { ApiKeys, ConsumerKeys, ConsumerSecrets, AccessToken, AccessSecret, ClientIDs, Accounts };
        //foreach (string file in LoadList)
        //{
        //    if (!File.Exists(file))
        //    {
        //        File.Create(file).Dispose(); // Stäng strömmen direkt
        //    }
        //}
        // load your local keys, Do NOT push these to github
        Dictionary<string, string> Api_Keys = LoadApiKeys(ApiKeys);
        API_Keys = Api_Keys;
        Dictionary<string, string> Consumer_Keys = LoadApiKeys(ConsumerKeys);
        CONSUMER_Keys = Consumer_Keys;
        Dictionary<string, string> Consumer_Secrets = LoadApiKeys(ConsumerSecrets);
        CONSUMER_Secrets = Consumer_Secrets;
        Dictionary<string, string> Access_Tokens = LoadApiKeys(AccessToken);
        ACCESS_Tokens = Access_Tokens;
        Dictionary<string, string> Access_Secrets = LoadApiKeys(AccessSecret);
        ACCESS_Secrets = Access_Secrets;
        Dictionary<string, string> Client_Ids = LoadApiKeys(ClientIDs);
        CLIENT_Ids = Client_Ids;
        Dictionary<string, string> Account_Names = LoadApiKeys(Accounts);
        ACCOUNT_Names = Account_Names;
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