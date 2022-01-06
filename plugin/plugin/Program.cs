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

            Console.ReadLine();
        }
    }
}