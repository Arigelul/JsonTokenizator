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
            int parentsNum = 0;
            int delimeterCounter = 3;
            GetNumParents(ref parentsNum);
            parentsNum *= delimeterCounter;
            sb.Append(' ', parentsNum);
            sb.Append('[');
            if (Elements != null)
            {
                foreach (var element in Elements)
                {
                    sb.Append('\n');
                    if (element.Type != JTokenType.Object)
                        sb.Append(' ', parentsNum + delimeterCounter);
                    sb.Append($"{element},");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append('\n');
            sb.Append(' ', parentsNum);
            sb.Append(']');
            return sb.ToString();
        }
    }
}
