using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal class JValue : JToken
    {
        public object Value { get; set; }
        public override JTokenType Type { get; }
        public JValue(JTokenType type)
        {
            Type = type;
        }
        public override string ToString()
        {
            switch (Type)
            {
                case JTokenType.String:
                    return $"\"{Value}\"";
                case JTokenType.Null:
                    return "null";
            }
            return Value.ToString();
        }
    }
}
