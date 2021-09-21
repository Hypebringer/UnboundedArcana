using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;

namespace UnboundedArcana.Utilities
{
    static class BlueprintUtilities
    {
        public static BlueprintGuid CreateGuid(string raw) => new BlueprintGuid(new Guid(raw));
    }
}
