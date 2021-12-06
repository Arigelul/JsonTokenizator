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
            int cnt = 0;
            var tabsNum = GetNumParents(ref cnt);
            sb.Append('{');
            if (Properties != null)
            {
                foreach (var propoerty in Properties)
                {
                    sb.Append('\n');
                    sb.Append('\t', tabsNum + 1);
                    sb.Append($"{propoerty},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append('\n');
            sb.Append('\t', tabsNum);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
