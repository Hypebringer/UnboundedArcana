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
using UnboundedArcana.Utilities.Builders;
using static UnboundedArcana.Utilities.OwlcatUtilites;


namespace UnboundedArcana.Edits
{
    partial class SpellEdits
    {
        public static void EditSleep()
        {
            const string sleepGuid = "bb7ecad2d3d2c8247a38f44855c99061";
            const string sleepKitsuneGuid = "f8a32c60ae1f878408b525bb967ef48c";

            try
            {
                var sleep = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(sleepGuid);
                var sleepKitsune = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(sleepKitsuneGuid);

                foreach (var variant in new[] { sleep, sleepKitsune })
                {
                    var hitDiceLimit = variant.GetComponent<ContextCalculateSharedValue>();
                    hitDiceLimit.Value.BonusValue = new ContextValue { ValueType = ContextValueType.Rank };
                    variant.m_Description = CreateLocalizedString("A variant spell causes a magical slumber to come upon 4 + 1/2 of caster level (max 5) HD of creatures, and those who are closest to the spell's point of origin are affected first. HD that are not sufficient to affect a creature are wasted. Sleeping creatures are helpless. Wounding awakens an affected creature, but normal noise does not. Sleep does not target unconscious creatures, constructs, or undead creatures.");
                    variant.RemoveComponent(x => x is ContextRankConfig);
                    var contextRankConfig = new ContextRankConfigBuilder
                    {
                        Max = 5,
                        Progression = ContextRankProgression.Div2PlusStep,
                        StepLevel = 4,
                        BaseValueType = ContextRankBaseValueType.CasterLevel
                    }.Build();
                    variant.AddComponent(contextRankConfig);
                }

                Main.Logger.Log($"Successfully installed Sleep edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Sleep spell! {e.Message}");
            }
        }
    }
}
