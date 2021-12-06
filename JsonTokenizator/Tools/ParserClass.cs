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
                    return GetJArray(json);
                default:
                    return GetJObject(json);
            }
        }

        internal string ReadJson(string path = @"C:\Users\Igor\Desktop\Example1min.txt")
        {
            return File.ReadAllText(path);
        }

        internal JObject GetJObject(string sourceString, JToken? parent = null)
        {
            var jObject = new JObject() { Parent = parent };

            if (String.IsNullOrEmpty(sourceString))
                return jObject;

            var jPropertiesList = new List<JProperty>();
            for (int i = 0; i < sourceString.Length; i++)
            {
                switch (sourceString[i])
                {
                    case '\"':
                        var jProperty = GetJProperty(sourceString.Substring(i), out int endPropertyIndex, jObject);
                        jPropertiesList.Add(jProperty);
                        i += endPropertyIndex;
                        break;
                }
            }
            jObject.Properties = GetNextPreviousTokens(jPropertiesList);
            return jObject;
        }

        internal JArray GetJArray(string sourceString, JToken? parent = null)
        {
            JArray jArray = new JArray() { Parent = parent };

            if (String.IsNullOrEmpty(sourceString))
                return jArray;

            var elements = new List<JToken>();

            for (int i = 1; i < sourceString.Length; i++)
            {
                var element = GetJPropertyValue(sourceString.Substring(i), out int length, jArray);
                elements.Add(element);
                i += length;
            }
            jArray.Elements = GetNextPreviousTokens(elements);
            return jArray;
        }

        internal JProperty GetJProperty(string sourceString, out int lastIndex, JToken? parent = null)
        {
            lastIndex = 0;
            var jProperty = new JProperty() { Parent = parent };

            if (String.IsNullOrEmpty(sourceString))
                return jProperty;

            jProperty.Name = GetJPropertyName(sourceString, out int nameEndIndex);
            var valueStartIndex = nameEndIndex + 2;
            jProperty.Value = GetJPropertyValue(sourceString.Substring(valueStartIndex), out int valueEndIndex, jProperty);
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

        internal JToken GetJPropertyValue(string sourceString, out int length, JToken? parent = null)
        {
            length = 0;
            var valueBody = String.Empty;

            if (String.IsNullOrEmpty(sourceString))
                return new JValue(JTokenType.Null);

            if (char.IsDigit(sourceString[0])) //not done
            {
                length = GetLengthValue(sourceString);
                valueBody = sourceString.Substring(0, length).Replace('.', ',');
                if (int.TryParse(valueBody, out var intNumber))
                    return new JValue(JTokenType.Integer) { Value = intNumber, Parent = parent };
                if (long.TryParse(valueBody, out var longNumber))
                    return new JValue(JTokenType.Long) { Value = longNumber };
                if (length < 10 && float.TryParse(valueBody, out var floatNumber))
                    return new JValue(JTokenType.Float) { Value = floatNumber, Parent = parent };
                if (double.TryParse(valueBody, out var doubleNumber))
                    return new JValue(JTokenType.Double) { Value = doubleNumber, Parent = parent };
            }

            switch (sourceString[0])
            {
                case '{':
                    length = GetContainerLastIndex(sourceString, '{', '}');
                    valueBody = sourceString.Substring(0, length);
                    var jObject = GetJObject(valueBody, parent);
                    return jObject;
                case '[':
                    length = GetContainerLastIndex(sourceString, '[', ']');
                    valueBody = sourceString.Substring(0, length);
                    var jArray = GetJArray(valueBody, parent);
                    return jArray;
                case 't':
                    length = 4;
                    return new JValue(JTokenType.Boolean) { Value = true, Parent = parent };
                case 'f':
                    length = 5;
                    return new JValue(JTokenType.Boolean) { Value = false, Parent = parent };
                case 'n':
                    length = 4;
                    return new JValue(JTokenType.Null);
                case '\"':
                    length = GetLengthValue(sourceString, true);
                    if (length < 2)
                        return new JValue(JTokenType.String) { Value = String.Empty, Parent = parent };
                    return new JValue(JTokenType.String) { Value = sourceString.Substring(1, length - 2), Parent = parent };
                default:
                    return new JValue(JTokenType.Null);
            }
        }

        internal List<T> GetNextPreviousTokens<T>(List<T> tokens) where T : JToken
        {
            for (int i = 1; i < tokens.Count(); i++)
            {
                tokens[i].Previous = tokens[i - 1];
                tokens[i - 1].Next = tokens[i];
            }
            if (tokens.Count > 1)
            {
                tokens[tokens.Count - 1].Previous = tokens[tokens.Count - 2];
                tokens[0].Next = tokens[1];
            }
            return tokens;
        }

        internal int GetLengthValue(string sourceString, bool isString = false)
        {
            if (String.IsNullOrEmpty(sourceString))
                return 0;

            for (int i = 1; i < sourceString.Length; i++)
            {
                switch (sourceString[i])
                {
                    case ']':
                    case '}':
                        return i;
                    case ',':
                        if (!isString)
                            return i;
                        break;
                    case '\"':
                        return i + 1;
                }
            }
            return sourceString.Length;
        }

        internal int GetContainerLastIndex(string sourceString, char delimOpen, char delimClose)
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
