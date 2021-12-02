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
        internal JToken Parse(string json)
        {
            if (json == null)
            {
                return new JObject();
            }

            switch (json[0])
            {
                case '[':
                    return new JArray();
                default:
                    return GetJObject(json);
            }
        }

        internal string ReadJson(string path = @"C:\Users\Igor\Desktop\Example2min.txt")
        {
            return File.ReadAllText(path);
        }

        internal int GetRawTokenIndex(string sourceString, char delim_open, char delim_close)
        {
            var str = sourceString;
            int index = 0;
            while (str.Length > 0)
            {
                int localIndex = str.IndexOf(delim_close) + 1;
                if (localIndex != -1)
                {
                    index += localIndex;
                    if (sourceString.Substring(0, index).Count(c => c.Equals(delim_open)) == sourceString.Substring(0, index).Count(c => c.Equals(delim_close)))
                        return index;
                    str = str.Substring(localIndex);
                }
                else break;
            }
            return -1;
        }

        internal JObject GetJObject(string sourceString)
        {
            var jObject = new JObject() { Type = JTokenType.Object };
            if (sourceString.Length > 0)
            {
                var jPropertiesList = new List<JProperty>();
                for (int i = 0; i < sourceString.Length; i++)
                {
                    switch (sourceString[i])
                    {
                        case '\"':
                            var jProperty = GetJProperty(sourceString.Substring(i), out int endPropertyIndex);
                            jPropertiesList.Add(jProperty);
                            i += endPropertyIndex;
                            break;
                    }
                }
                jObject.Properties = jPropertiesList.ToArray();
            }
            return jObject;
        }

        internal JProperty GetJProperty(string sourceString, out int lastIndex)
        {
            lastIndex = 0;
            var jProperty = new JProperty() { Type = JTokenType.Property };
            if (sourceString.Length > 0)
            {
                int nameEndIndex = sourceString.IndexOf("\":");
                if (nameEndIndex != -1)
                {
                    jProperty.Name = sourceString.Substring(0, nameEndIndex + 1);
                    int valueStartIndex = nameEndIndex + 2;
                    int valueEndIndex = GetPropertyLastIndex(sourceString.Substring(valueStartIndex), sourceString[valueStartIndex]);
                    jProperty.Value = new JValue { Value = sourceString.Substring(valueStartIndex, valueEndIndex) };
                    lastIndex = nameEndIndex + valueEndIndex + 2;
                }
            }
            return jProperty;
        }

        internal int GetPropertyLastIndex(string sourceString, char param)
        {
            if (char.IsDigit(param))
                return GetLengthValue(sourceString);

            switch (param)
            {
                case '{':
                    return GetRawTokenIndex(sourceString, '{', '}');
                case '[':
                    return GetRawTokenIndex(sourceString, '[', ']');
                case 't':
                case 'f':
                case '\"':
                    return GetLengthValue(sourceString);
                default:
                    return 0;
            }
        }

        internal int GetLengthValue(string sourceString)
        {
            for (int i = 1; i < sourceString.Length; i++)
            {
                switch (sourceString[i])
                {
                    case ',':
                    case ']':
                    case '}':
                        return i;
                    case '\"':
                        return i + 1;
                }
            }
            return sourceString.Length;
        }
    }
}
