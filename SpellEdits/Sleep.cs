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
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnboundedArcana.Extensions;
using static UnboundedArcana.Utilities.Blueprint;


namespace UnboundedArcana
{
    partial class SpellEdits
    {
        public static void EditSleep()
        {
            const string sleepGuid = "bb7ecad2d3d2c8247a38f44855c99061";

            try
            {
                var sleep = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(sleepGuid);
                sleep.m_Description = CreateLocalizedString("A sleep spell causes a magical slumber to come upon 4 + 1/2 of caster level (max 5) HD of creatures, and those who are closest to the spell's point of origin are affected first. HD that are not sufficient to affect a creature are wasted. Sleeping creatures are helpless. Wounding awakens an affected creature, but normal noise does not. Sleep does not target unconscious creatures, constructs, or undead creatures.");

                var hitDiceLimit = sleep.GetComponent<ContextCalculateSharedValue>();
                hitDiceLimit.Value.BonusValue = new ContextValue { ValueType = ContextValueType.Rank };


                var hitDiceConfig = sleep.GetComponent<ContextRankConfig>();
                hitDiceConfig.m_Max = 10;
                hitDiceConfig.m_Progression = ContextRankProgression.Div2PlusStep;
                hitDiceConfig.m_StepLevel = 4;
                hitDiceConfig.m_BaseValueType = ContextRankBaseValueType.MaxCasterLevel;

                Main.Logger.Log($"Successfully installed Sleep edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Sleep spell! {e.Message}");
            }
        }
    }
}
