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
    static class UnbindSpellsPatch
    {
        static bool initialized = false;

        static void Postfix()
        {
            if (initialized) return;
            initialized = true;
            //Main.Logger.Log("Patching spells");
            SpellEdits.EditCauseFear();
        }
    }
}
