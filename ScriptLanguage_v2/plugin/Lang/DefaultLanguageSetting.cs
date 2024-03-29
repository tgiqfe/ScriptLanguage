﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace plugin.Lang
{
    /// <summary>
    /// DefaultのLanguageの設定値を取得
    /// </summary>
    internal class DefaultLanguageSetting
    {
        public static List<Language> Create()
        {
            List<Language> list = new List<Language>();
            list.Add(new Language()
            {
                Name = "exe",
                Extensions = new string[] { ".exe" },
                Command = null,
            });
            list.Add(new Language()
            {
                Name = "cmd",
                Alias = new string[] { "dos", "bat" },
                Extensions = new string[] { ".bat", ".cmd" },
                Encoding = "Shift_JIS",
                Command = "cmd",
                ArgsPrefix = "/c \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            });
            list.Add(new Language()
            {
                Name = "PowerShell",
                Alias = new string[] { "pshell", "pwsh" },
                Extensions = new string[] { ".ps1" },
                Encoding = "Shift_JIS",
                Command = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
                ArgsPrefix = "-ExecutionPolicy Unrestricted -File \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            });
            list.Add(new Language()
            {
                Name = "Pwsh7",
                Alias = new string[] { "pshell", "pwsh" },
                Description = "PowerShell v7",
                Extensions = new string[] { ".ps1" },
                Encoding = "UTF-8",
                Command = WhereCommand("pwsh") ?? @"C:\Program Files\PowerShell\7\pwsh.exe",
                ArgsPrefix = "-ExecutionPolicy Unrestricted -File \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            });
            list.Add(new Language()
            {
                Name = "WScript",
                Alias = new string[] { "vbs", "vbscript", "js", "jscript" },
                Extensions = new string[] { ".vbs", ".vbe", ".js", ".jse", ".wsf", ".wsh" },
                Encoding = "Shift_JIS",
                Command = @"C:\Windows\System32\wscript.exe",
                ArgsPrefix = "//nologo \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            });
            list.Add(new Language()
            {
                Name = "Go",
                Alias = new string[] { "golang" },
                Extensions = new string[] { ".go" },
                Encoding = "UTF-8",
                //Command = @"C:\Program Files\Go\bin\go.exe",
                Command = WhereCommand("go") ?? @"C:\Program Files\Go\bin\go.exe",
                ArgsPrefix = "\"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            });
            list.Add(new Language()
            {
                Name = "Node.js",
                Alias = new string[] { "nodejs", "node" },
                Extensions = new string[] { ".js" },
                Encoding = "UTF-8",
                //Command = @"C:\Program Files\nodejs\node.exe",
                Command = WhereCommand("node") ?? @"C:\Program Files\nodejs\node.exe",
                ArgsPrefix = "\"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            });
            return list;
        }

        private static string WhereCommand(params string[] commands)
        {
            foreach (string command in commands)
            {
                using (var proc = new Process())
                {
                    proc.StartInfo.FileName = "where.exe";
                    proc.StartInfo.Arguments = command;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.Start();
                    string output_psCommandPath = proc.StandardOutput.ReadLine();     //最初の1行のみ取得
                    proc.WaitForExit();

                    return proc.ExitCode == 0 ? output_psCommandPath.Trim() : null;
                }
            }
            return null;
        }
    }
}
