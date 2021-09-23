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
        public static void ReplaceComponent(this BlueprintScriptableObject ability, Func<BlueprintComponent, bool> which, BlueprintComponent component)
        {
            ability.Components = ability.Components
                .Replace(which, component)
                .ToArray();
        }

        public static void RemoveComponent(this BlueprintScriptableObject ability, Func<BlueprintComponent, bool> which)
        {
            ability.Components = ability.Components
                .Remove(which)
                .ToArray();
        }
    }
}
