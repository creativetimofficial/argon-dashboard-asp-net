using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace CreativeTim.Argon.DotNetCore.Free.Infrastructure
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value == null) { return null; }

            // Slugify value
            return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
