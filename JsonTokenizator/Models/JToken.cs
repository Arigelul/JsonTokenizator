using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal abstract class JToken
    {
        public JTokenType Type { get; set; }
        public JToken? Next { get; set; }
        public JToken? Previous { get; set; }
        public JToken? Parent { get; set; }
        public IEnumerable<JToken> Children { get; set; }
    }
}
