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
    static class CantripScalingPatch
    {
        static bool initialized = false;

        static void Postfix()
        {
            if (!Main.Settings.InstallCantripScaling)
            {
                Main.Logger.Log("Skipping patch: Cantrip Scaling.");
                initialized = true;
            }
            if (initialized) return;
            initialized = true;
            Cantrips.EditRayOfFrost();
            Cantrips.EditAcidSplash();
            Cantrips.EditJolt();
            Cantrips.EditDivineZap();
            Cantrips.EditDaze();
            Cantrips.EditGuidance();
            Cantrips.EditVirtue();
            Main.Logger.Log("Installed patch: Cantrip Scaling!");
        }
    }
}
