using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;

namespace UnboundedArcana.Patches
{
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class UnchainSpellsPatch
    {
        static bool initialized = false;

        static void Postfix()
        {
            if (initialized) return;
            initialized = true;
            SpellEdits.EditCauseFear();
            SpellEdits.EditScare();
        }
    }
}
