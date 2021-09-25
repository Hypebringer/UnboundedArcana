using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnboundedArcana.Extensions;
using static UnboundedArcana.Utilities.OwlcatUtilites;


namespace UnboundedArcana
{
    partial class SpellEdits
    {
        public static void EditDaze()
        {
            const string dazeGuid = "55f14bc84d7c85446b07a1b5dd6b2b4c";

            try
            {
                var daze = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(dazeGuid);
                daze.m_Description = CreateLocalizedString("This spell clouds the mind of a humanoid creature so that it takes no actions. A dazed subject is not stunned, so attackers get no special advantage against it. After a creature has been dazed by this spell, it is immune to the effects of this spell for 1 minute.");
                daze.RemoveComponent(comp => comp is AbilityTargetMaximumHitDice);
               
                Main.Logger.Log($"Successfully installed Daze edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Daze spell! {e.Message}");
            }
        }
    }
}
