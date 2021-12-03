using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal class JArray : JToken
    {
        public IEnumerable<JToken> Elements { get; set; }

        public override JTokenType Type => JTokenType.Array;

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var element in Elements)
                sb.Append(element.ToString() + '\n');
            return sb.ToString();
        }
    }
}
