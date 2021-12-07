using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal class JObject : JToken
    {
        public override JTokenType Type => JTokenType.Object;
        public IEnumerable<JProperty>? Properties { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            int parentsNum = 0;
            int delimeterCounter = 3;
            GetNumParents(ref parentsNum);
            parentsNum *= delimeterCounter;
            sb.Append(' ', parentsNum);
            sb.Append('{');
            if (Properties != null)
            {
                foreach (var property in Properties)
                {
                    sb.Append('\n');
                    sb.Append(' ', parentsNum + delimeterCounter);
                    sb.Append($"{property},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append('\n');
            sb.Append(' ', parentsNum);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
