using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace ScriptLanguage.ScriptLanguage
{
    internal class Languages
    {
        const string CONFIG_FILE_NAME = "languages.json";

        private static Language[] _languages = null;

        #region Load/Save

        /// <summary>
        /// Load from languages conf file.
        /// </summary>
        /// <returns></returns>
        public static Language[] Load()
        {
            try
            {
                var parent = Path.GetDirectoryName(Environment.ProcessPath);
                var configFilePath = Path.Combine(parent, CONFIG_FILE_NAME);
                var content = File.ReadAllText(configFilePath, Encoding.UTF8);
                _languages = JsonSerializer.Deserialize<Language[]>(content);
            }
            catch
            {
                _languages = DefaultLanguageSetting.Generate2();
                Save();
            }
            return _languages;
        }

        /// <summary>
        /// Save to languages conf file.
        /// </summary>
        /// <param name="languages"></param>
        public static void Save(Language[] languages = null)
        {
            try
            {
                if (languages == null && _languages == null) return;

                var parent = Path.GetDirectoryName(Environment.ProcessPath);
                var configFilePath = Path.Combine(parent, CONFIG_FILE_NAME);
                var content = JsonSerializer.Serialize(languages ?? _languages,
                    new JsonSerializerOptions
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        IgnoreReadOnlyProperties = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                        WriteIndented = true,
                        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase) },
                    });
                File.WriteAllText(configFilePath, content, Encoding.UTF8);
            }
            catch { }
        }

        #endregion

        /// <summary>
        /// Get Language instance from filepath or language name/alias.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static Language GetLanguage(string keyword)
        {
            if (_languages == null) Load();
            if (File.Exists(keyword))
            {
                string extension = Path.GetExtension(keyword);
                return _languages.FirstOrDefault(x =>
                    x.Extensions.Any(y =>
                        y.Equals(extension, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                return _languages.FirstOrDefault(x =>
                    x.Name.Equals(keyword, StringComparison.OrdinalIgnoreCase) ||
                    (x.Alias != null && x.Alias.Any(y => y.Equals(keyword, StringComparison.OrdinalIgnoreCase))));
            }
        }

        /// <summary>
        /// Get process instance from script file path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Process GetProcess(string path)
        {
            if (_languages == null) Load();
            if (File.Exists(path))
            {
                string extension = Path.GetExtension(path);
                var lang = _languages.FirstOrDefault(x =>
                    x.Extensions.Any(y =>
                        y.Equals(extension, StringComparison.OrdinalIgnoreCase)));
                return lang?.GetProcess(path, "");
            }
            return null;
        }
    }
}
