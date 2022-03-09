// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Json.Schema;

IClientRequestMessage? ParseMessageByType(JsonElement root)
{
    var type = root.GetProperty("type").GetString();
    if (string.Equals(type, "LoginRequest", StringComparison.CurrentCultureIgnoreCase))
    {
        var login = root.GetProperty("payload").Deserialize<LoginRequest>();
        return login;
    }

    return null;
}

// Read the schema
var schema = JsonSchema.FromFile("schema.json");
// Get a stream (in this case a file stream)
var file = File.OpenRead("data.json");
// Parse the stream of bytes
var json = await JsonDocument.ParseAsync(file); // uses System.Text.Json 
// Validate against schema
var result = schema.Validate(json.RootElement, new ValidationOptions {
    ValidateAs = Draft.Draft7,
    OutputFormat = OutputFormat.Verbose
});

// For debug show the validity status
var validity = result.IsValid ? "Valid" : "Invalid:";
Console.WriteLine($"Json is {validity} {result.Message}");

// Check if is valid
if (result.IsValid)
{
    var m = ParseMessageByType(json.RootElement);
    if (m is LoginRequest request)
    {
        Console.WriteLine($"Bearer Token: {request?.bearer}");
    }
}

public interface IClientRequestMessage
{
}

public class LoginRequest : IClientRequestMessage
{
    public string bearer { get; set; }
}