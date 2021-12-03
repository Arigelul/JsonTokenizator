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
            if (String.IsNullOrEmpty(json))
                return new JObject();

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

        internal JObject GetJObject(string sourceString)
        {
            var jObject = new JObject();

            if (String.IsNullOrEmpty(sourceString))
                return jObject;

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
            return jObject;
        }

        internal JProperty GetJProperty(string sourceString, out int lastIndex)
        {
            lastIndex = 0;
            var jProperty = new JProperty();

            if (String.IsNullOrEmpty(sourceString))
                return jProperty;

            jProperty.Name = GetJPropertyName(sourceString, out int nameEndIndex);
            var valueStartIndex = nameEndIndex + 2;
            jProperty.Value = GetJPropertyValue(sourceString.Substring(valueStartIndex), out int valueEndIndex);
            lastIndex = nameEndIndex + valueEndIndex + 2;
            return jProperty;
        }

        internal string GetJPropertyName(string sourceString, out int nameEndIndex)
        {
            nameEndIndex = sourceString.IndexOf("\":", StringComparison.Ordinal);
            if (nameEndIndex == -1 /*&& nameEndIndex != 1*/)
                return String.Empty;
            return sourceString.Substring(1, nameEndIndex - 1);
        }

        internal JToken GetJPropertyValue(string sourceString, out int length)
        {
            length = 0;
            var valueBody = String.Empty;

            if (String.IsNullOrEmpty(sourceString))
                return new JValue(JTokenType.Null);

            if (char.IsDigit(sourceString[0])) //not done
            {
                length = GetLengthValue(sourceString);
                valueBody = sourceString.Substring(0, length);
                var jValue = new JValue(JTokenType.Integer);
                jValue.Value = Convert.ToInt64(valueBody);
                return jValue;
            }

            switch (sourceString[0])
            {
                case '{':
                    length = GetRawTokenLastIndex(sourceString, '{', '}');
                    valueBody = sourceString.Substring(0, length);
                    var jObject = GetJObject(valueBody);
                    return jObject;
                case '[':
                    length = GetRawTokenLastIndex(sourceString, '[', ']');
                    valueBody = sourceString.Substring(0, length);
                    var jArray = new JArray();
                    return jArray;
                case 't':
                    length = 4;
                    return new JValue(JTokenType.Boolean) { Value = true };
                case 'f':
                    length = 5;
                    return new JValue(JTokenType.Boolean) { Value = false };
                case '\"':
                    length = GetLengthValue(sourceString);
                    return new JValue(JTokenType.String) { Value = sourceString.Substring(1, length - 1) };
                default:
                    return new JValue(JTokenType.Null);
            }
        }

        internal int GetLengthValue(string sourceString)
        {
            if (String.IsNullOrEmpty(sourceString))
                return 0;

            for (int i = 1; i < sourceString.Length; i++)
            {
                switch (sourceString[i])
                {
                    case ',':
                    case ']':
                    case '}':
                    case '\"':
                        return i;
                }
            }
            return sourceString.Length;
        }

        internal int GetRawTokenLastIndex(string sourceString, char delimOpen, char delimClose)
        {
            var str = sourceString;
            var index = 0;
            while (str.Length > 0)
            {
                var localIndex = str.IndexOf(delimClose, StringComparison.Ordinal) + 1;
                if (localIndex != -1)
                {
                    index += localIndex;
                    if (sourceString.Substring(0, index).Count(c => c.Equals(delimOpen)) == sourceString.Substring(0, index).Count(c => c.Equals(delimClose)))
                        return index;
                    str = str.Substring(localIndex);
                }
                else break;
            }
            return -1;
        }
    }
}
