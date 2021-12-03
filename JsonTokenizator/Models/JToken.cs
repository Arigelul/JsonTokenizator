using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTokenizator.Models
{
    internal abstract class JToken
    {
        public abstract JTokenType Type { get; }
        public JToken? Next { get; set; }
        public JToken? Previous { get; set; }
        public JToken? Parent { get; set; }
        public abstract override string ToString();
    }
}
