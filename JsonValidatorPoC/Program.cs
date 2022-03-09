// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;
using Json.Schema;

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
// Check if is valid

// For debug show the validity status
var validity = result.IsValid ? "Valid" : "Invalid:";
Console.WriteLine($"Json is {validity} {result.Message}");