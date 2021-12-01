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
    }
}
