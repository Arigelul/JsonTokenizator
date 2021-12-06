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
            return $"\"{Name}\": {Value}";
        }
    }
}
