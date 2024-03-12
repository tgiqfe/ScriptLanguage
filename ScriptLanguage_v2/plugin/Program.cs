using plugin.Lang;
using System;
using System.IO;
using System.Diagnostics;

namespace plugin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Sample02();

            Console.ReadLine();
        }

        private static void Sample01()
        {
            string path = Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "ScriptLanguage", "lang.json");
            string scriptPath = Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "ScriptLanguage", "sample.bat");

            var collection = new LanguageCollection();
            collection.Load(path);

            using (Process proc = collection.GetProcess(scriptPath))
            {
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.WaitForExit();
            }
        }

        private static void Sample02()
        {
            string path = Path.Combine(Environment.ExpandEnvironmentVariables("%TEMP%"), "ScriptLanguage", "lang.json");
            var collection = new LanguageCollection();
            collection.Load(path);

            string text1 = "bat";
            string text2 = "dos";
            string text3 = "VBS";
            string text4 = "pwsh";
            string text5 = "exe";

            var lang1 = collection.GetLanguage(text1);
            var lang2 = collection.GetLanguage(text2);
            var lang3 = collection.GetLanguage(text3);
            var lang4 = collection.GetLanguage(text4);
            var lang5 = collection.GetLanguage(text5);

            var options = new System.Text.Json.JsonSerializerOptions()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IgnoreReadOnlyProperties = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase) },
            };

            string json1 = System.Text.Json.JsonSerializer.Serialize(lang1, options);
            string json2 = System.Text.Json.JsonSerializer.Serialize(lang2, options);
            string json3 = System.Text.Json.JsonSerializer.Serialize(lang3, options);
            string json4 = System.Text.Json.JsonSerializer.Serialize(lang4, options);
            string json5 = System.Text.Json.JsonSerializer.Serialize(lang5, options);

            Console.WriteLine("-------------------");
            Console.WriteLine(json1);
            Console.WriteLine("-------------------");
            Console.WriteLine(json2);
            Console.WriteLine("-------------------");
            Console.WriteLine(json3);
            Console.WriteLine("-------------------");
            Console.WriteLine(json4);
            Console.WriteLine("-------------------");
            Console.WriteLine(json5);
        }
    }
}