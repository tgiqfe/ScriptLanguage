using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Language> langs = DefaultLanguageSetting.Create();

            Console.WriteLine(string.Join(", ", langs));

            Console.ReadLine();
        }
    }
}
