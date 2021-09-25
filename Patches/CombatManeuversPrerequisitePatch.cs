using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnboundedArcana.Edits;

namespace UnboundedArcana.Patches
{
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class CombatManeuversPrerequisitePatch
    {
        static bool initialized = false;

        static void Postfix()
        {
            if (initialized) return;
            initialized = true;
            PiranhaStrikeOrCombatExpertiseAsRequirement.Edit();
        }
    }
}
