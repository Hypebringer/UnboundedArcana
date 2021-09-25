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
            SpellEdits.EditRayOfFrost();
            SpellEdits.EditAcidSplash();
            SpellEdits.EditJolt();
            SpellEdits.EditDivineZap();
            SpellEdits.EditDaze();
            SpellEdits.EditGuidance();
            SpellEdits.EditVirtue();
            SpellEdits.EditCauseFear();
            SpellEdits.EditScare();
            SpellEdits.EditSleep();
            SpellEdits.EditDeepSlumber();
            SpellEdits.EditHypnotism();
            SpellEdits.EditRainbowPattern();
            SpellEdits.EditColorSpray();
        }
    }
}
