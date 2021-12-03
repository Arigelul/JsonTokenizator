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
        public JValue(JTokenType type = JTokenType.Null)
        {
            Type = type;
        }
        public override string ToString()
        {
            if (Value == null)
                return string.Empty;
            return Value.ToString();
        }
    }
}
