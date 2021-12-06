using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal class JArray : JToken
    {
        public IEnumerable<JToken>? Elements { get; set; }

        public override JTokenType Type => JTokenType.Array;

        public override string ToString()
        {
            var sb = new StringBuilder();
            int cnt = 0;
            var tabsNum = GetNumParents(ref cnt);
            //sb.Append('\n');
            //sb.Append('\t', tabsNum);
            sb.Append('[');
            if (Elements != null)
            {
                foreach (var element in Elements)
                {
                    sb.Append('\n');
                    sb.Append('\t', tabsNum + 1);
                    sb.Append($"{element},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append('\n');
            sb.Append('\t', tabsNum);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
