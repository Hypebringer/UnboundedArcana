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
    static class SpellsWithoutHDLimitsPatch
    {
        static bool initialized = false;

        static void Postfix()
        {
            if (!Main.Settings.InstallSpellsWithoutHDLimits)
            {
                Main.Logger.Log("Skipping patch: Spells without HD limits.");
                initialized = true;
            }
            if (initialized) return;
            initialized = true;
            SpellsAndHD.EditCauseFear();
            SpellsAndHD.EditScare();
            SpellsAndHD.EditSleep();
            SpellsAndHD.EditDeepSlumber();
            SpellsAndHD.EditHypnotism();
            SpellsAndHD.EditRainbowPattern();
            SpellsAndHD.EditColorSpray();
            Main.Logger.Log("Installed patch: Spells without HD limits!");
        }
    }
}
