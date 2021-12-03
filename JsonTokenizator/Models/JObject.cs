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
    }
}
