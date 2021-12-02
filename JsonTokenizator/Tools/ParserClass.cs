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
                        var jObject = new JObject() { Type = JTokenType.Object };
                        break;
                    case '[':
                        var jArray = new JArray() { Type = JTokenType.Array };
                        break;
                    case ',':
                        break;
                }
            }
            return new JObject();
        }

        internal string ReadJson(string path = @"C:\Users\Igor\Desktop\Example2min.txt")
        {
            return File.ReadAllText(path);
        }

        //public static int RegexIndexOf(string str, string pattern)
        //{
        //    var m = Regex.Match(str, pattern);
        //    return m.Success ? m.Index : -1;
        //}

        internal int GetRawTokenIndex(string source_str, char delim_open, char delim_close)
        {
            var str = source_str;
            int index = 0;
            while (str.Length > 0)
            {
                int localIndex = str.IndexOf(delim_close) + 1;
                if (localIndex != -1)
                {
                    index += localIndex;
                    if (source_str.Substring(0, index).Count(c => c.Equals(delim_open)) == source_str.Substring(0, index).Count(c => c.Equals(delim_close)))
                        return index;
                    str = str.Substring(localIndex);
                }
                else break;
            }
            return -1;
        }
    }
}
