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

        // TODO: fetch data from new patch, refactor Sleep and DeepSlumber, fix Hypnotism with this data, check stuff that I've already done if it can be cleaned.

        static void Postfix()
        {
            if (initialized) return;
            initialized = true;
            SpellEdits.EditCauseFear();
            SpellEdits.EditScare();
            SpellEdits.EditSleep();
            SpellEdits.EditDeepSlumber();
            SpellEdits.EditRainbowPattern();
            //SpellEdits.EditHypnotism();
            SpellEdits.EditRayOfFrost();
            SpellEdits.EditAcidSplash();
            SpellEdits.EditJolt();
            SpellEdits.EditDaze();
            SpellEdits.EditGuidance();
        }
    }
}
