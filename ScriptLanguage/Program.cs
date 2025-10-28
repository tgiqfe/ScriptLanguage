
using ScriptLanguage.ScriptLanguage;
using System.Text.Json;

var languages = Languages.Load();
var json = JsonSerializer.Serialize(
    languages,
    new JsonSerializerOptions
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        IgnoreReadOnlyProperties = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase) }
    });
Console.WriteLine(json);

using (var proc = Languages.GetProcess("test.ps1"))
{
    proc.StartInfo.UseShellExecute = false;
    proc.StartInfo.CreateNoWindow = true;
    proc.Start();
    proc.WaitForExit();
}

Console.ReadLine();
