using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;

namespace UnboundedArcana.Extensions
{
    static class BlueprintScriptableObjectExtensions
    {
        public static void AddComponent(this BlueprintScriptableObject ability, BlueprintComponent component)
        {
            ability.Components = ability.Components
                .ConcatSingle(component)
                .ToArray();
        }
        public static void ReplaceFirstComponent(this BlueprintScriptableObject ability, Func<BlueprintComponent, bool> which, BlueprintComponent component)
        {
            ability.Components = ability.Components
                .ReplaceFirst(which, component)
                .ToArray();
        }
    }
}
