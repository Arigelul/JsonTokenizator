using JsonTokenizator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JsonTokenizator.Tools
{
    internal class ParserClass
    {
        internal JObject Parse(string json)
        {
            for (int i = 0; i < json.Length; i++)
            {
                switch (json[i])
                {
                    case '{':
                        
                        break;
                    case '[':
                        break;
                    case ',':
                        break;
                }
            }
            return new JObject();
        }

        static string ReadJson(string path = @"C:\Users\Igor\Desktop\Example2min.txt")
        {
            return File.ReadAllText(path);
        }

        //public static int RegexIndexOf(string str, string pattern)
        //{
        //    var m = Regex.Match(str, pattern);
        //    return m.Success ? m.Index : -1;
        //}

        public static int GetRawToken (string str, char delim_open, char delim_close)
        {
            int index = str.IndexOf(delim_close);
            if (str.Substring(0, index).Count(c => c.Equals(delim_open)) == str.Substring(0, index).Count(c => c.Equals(delim_close)))
                return index;
            else if (index != str.Length - 1) str = str.Substring(index + 1);
            return 0;
        }
    }
}
