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
#if DEBUG
                .Tap(xs => 
                {
                    if (xs.FirstOrDefault(which) == default)
                        Main.Logger.Warning("No element was removed?");
                })
#endif
                .Replace(which, component)
                .ToArray();
        }

        public static void RemoveComponent(this BlueprintScriptableObject ability, Func<BlueprintComponent, bool> which)
        {
            ability.Components = ability.Components
#if DEBUG
                .Tap(xs =>
                {
                    if (xs.FirstOrDefault(which) == default)
                        Main.Logger.Warning("No element was removed?");
                })
#endif
                .Remove(which)
                .ToArray();
        }

        public static void RemoveComponents(this BlueprintScriptableObject ability, Func<BlueprintComponent, bool> which)
        {
            ability.Components = ability.Components
#if DEBUG
                .Tap(xs => Main.Logger.Log
                    ($"Removing {xs.Where(which).Count()} elements"))
#endif
                .RemoveAll(which)
                .ToArray();
        }
    }
}
