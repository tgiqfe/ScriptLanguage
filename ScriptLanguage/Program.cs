
using ScriptLanguage.Plugin;

var lang_json = LanguageCollection.Load("lang.json");
var lang_yaml = LanguageCollection.Load("lang.yml");

lang_json.Save("lang.json");
lang_yaml.Save("lang.yml");

using (var proc = lang_json.GetProcess("test.ps1"))
{
    proc.StartInfo.CreateNoWindow = true;
    proc.StartInfo.UseShellExecute = false;
    proc.Start();
    proc.WaitForExit();
}



Console.ReadLine();
