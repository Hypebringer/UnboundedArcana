using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnboundedArcana.Edits;
using UnboundedArcana.Edits.Feats;

namespace UnboundedArcana.Patches
{
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class FeatProgressionChangesPatch
    {
        static bool initialized = false;

        static void Postfix()
        {
            if (!Main.Settings.InstallFeatProgressionChanges)
            {
                Main.Logger.Log("Skipping patch: Feat Progression Changes.");
                initialized = true;
            }
            if (initialized) return;
            initialized = true;
            Feats.EditCombatManeuverFeatsRequirements();
            Main.Logger.Log("Installed patch: Feat Progression Changes!");
        }
    }
}
