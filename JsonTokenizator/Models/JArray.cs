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
            int tabsNum = 0;
            GetNumParentsObject(ref tabsNum);
            if (HasParent())
            {
                sb.Append('\n');
                tabsNum--;
            }
            tabsNum *= 3;
            sb.Append(' ', tabsNum);
            sb.Append('[');
            if (Elements != null)
            {
                foreach (var element in Elements)
                {
                    sb.Append('\n');
                    sb.Append(' ', tabsNum + 3);
                    sb.Append($"{element},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append('\n');
            sb.Append(' ', tabsNum);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
