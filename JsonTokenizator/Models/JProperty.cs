using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal class JProperty : JToken
    {
        public string? Name { get; set; }
        public JToken? Value { get; set; }
        public override JTokenType Type => JTokenType.Property;
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('\"');
            sb.Append(Name);
            sb.Append("\": ");
            if (Value.Type == JTokenType.Array || Value.Type == JTokenType.Object)
                sb.Append('\n');
            sb.Append(Value.ToString());
            return sb.ToString();
        }
    }
}
