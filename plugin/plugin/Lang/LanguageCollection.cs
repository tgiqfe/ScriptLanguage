
namespace plugin.Lang
{
    internal class LanguageCollection : List<Language>
    {
        #region Load/Save

        public void Load(string path)
        {
            List<Language> list = null;
            try
            {
                using (var sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    list = System.Text.Json.JsonSerializer.Deserialize<List<Language>>(sr.ReadToEnd());
                }
            }
            catch { }
            if (list == null)
            {
                list = DefaultLanguageSetting.Create();
            }

            this.Clear();
            this.AddRange(list);
            this.Save(path);
        }

        public void Save(string path)
        {
            string parent = Path.GetDirectoryName(path);
            if (parent != "" && !Directory.Exists(parent))
            {
                Directory.CreateDirectory(parent);
            }
            try
            {
                using (var sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    string json = System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions()
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        IgnoreReadOnlyProperties = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                        WriteIndented = true,

                    });
                    sw.WriteLine(json);
                }
            }
            catch { }
        }

        #endregion

        public Language GetLanguage(string filePath)
        {
            if (File.Exists(filePath))
            {
                string extension = Path.GetExtension(filePath);
                return this.FirstOrDefault(x =>
                    x.Extensions.Any(y =>
                        y.Equals(extension, StringComparison.OrdinalIgnoreCase)));
            }
            return null;
        }

        public System.Diagnostics.Process GetProcess(string filePath)
        {
            if (File.Exists(filePath))
            {
                string extension = Path.GetExtension(filePath);
                Language lang = this.FirstOrDefault(x =>
                    x.Extensions.Any(y =>
                        y.Equals(extension, StringComparison.OrdinalIgnoreCase)));
                return lang == null ?
                    null :
                    lang.GetProcess(filePath, "");
            }
            return null;
        }
    }
}
