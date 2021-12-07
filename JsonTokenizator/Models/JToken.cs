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
        public bool HasParent() => Parent != null;
        public bool HasNext() => Next != null;
        public bool HasPrevious() => Previous != null;
        public void GetNumParents(ref int counter)
        {
            if (HasParent())
            {
                if (this.Type != JTokenType.Array)
                    counter++;
                Parent.GetNumParents(ref counter);
            }
        }
        public abstract override string ToString();
    }
}
