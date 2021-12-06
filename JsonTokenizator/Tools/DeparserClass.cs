using JsonTokenizator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Tools
{
    internal class DeparserClass
    {
        internal string Deparse (JToken jToken)
        {
            var sb = new StringBuilder();
            sb.Append(jToken.ToString());
            return sb.ToString ();
        }
    }
}
