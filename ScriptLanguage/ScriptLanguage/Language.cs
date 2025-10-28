using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ScriptLanguage.ScriptLanguage
{
    internal class Language
    {
        /// <summary>
        /// スクリプトを実行させる言語名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 別名
        /// </summary>
        public string[] Alias { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 関連する拡張子
        /// </summary>
        public string[] Extensions { get; set; }

        /// <summary>
        /// スクリプト実行する場合の文字コード
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// x64用コマンド
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// x86用コマンド
        /// </summary>
        public string Command_x86 { get; set; }

        /// <summary>
        /// Arm64用コマンド
        /// </summary>
        public string Command_arm { get; set; }

        /// <summary>
        /// [実行ファイル].exe AAAA[スクリプトファイル]BBBB[引数]CCCC
        ///                    ~~~~
        ///                    ↑この部分
        /// </summary>
        public string ArgsPrefix { get; set; }

        /// <summary>
        /// [実行ファイル].exe AAAA[スクリプトファイル]BBBB
        ///                                            ~~~~
        ///                                            ↑この部分
        /// </summary>
        public string ArgsMidWithoutArgs { get; set; }

        /// <summary>
        /// [実行ファイル].exe AAAA[スクリプトファイル]BBBB[引数]CCCC
        ///                                            ~~~~
        ///                                            ↑この部分
        /// </summary>
        public string ArgsMidWithArgs { get; set; }

        /// <summary>
        /// [実行ファイル].exe AAAA[スクリプトファイル]BBBB[引数]CCCC
        ///                                                      ~~~~
        ///                                                      ↑この部分
        /// </summary>
        public string ArgsSuffix { get; set; }

        /// <summary>
        /// 文字情報として表示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string extensionText = string.Join(" ", this.Extensions);
            return $"{this.Name}[{extensionText}]";
        }

        /// <summary>
        /// スクリプトを実行させるためのProcessインスタンスを取得
        /// </summary>
        /// <param name="scriptFile"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public Process GetProcess(string scriptFile, string arguments)
        {
            if (this.Command == null)
            {
                return new()
                {
                    StartInfo = new()
                    {
                        FileName = scriptFile,
                        Arguments = arguments,
                    }
                };
            }
            else
            {
                return new()
                {
                    StartInfo = new()
                    {
                        FileName = RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X64 => this.Command,
                            Architecture.Arm64 => this.Command_arm ?? this.Command,
                            Architecture.X86 => this.Command_x86 ?? this.Command,
                            _ => this.Command,
                        },
                        Arguments = string.IsNullOrEmpty(arguments) ?
                            $"{this.ArgsPrefix}{scriptFile}{this.ArgsMidWithoutArgs}" :
                            $"{this.ArgsPrefix}{scriptFile}{this.ArgsMidWithArgs}{arguments}{this.ArgsSuffix}",
                    }
                };
            }
        }
    }
}
