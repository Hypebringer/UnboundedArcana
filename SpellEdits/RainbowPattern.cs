using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnboundedArcana.Extensions;
using static UnboundedArcana.Utilities.Blueprint;


namespace UnboundedArcana
{
    partial class SpellEdits
    {
        public static void EditRainbowPattern()
        {
            const string rainbowGuid = "4b8265132f9c8174f87ce7fa6d0fe47b";

            try
            {
                var rainbow = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(rainbowGuid);
                rainbow.m_Description = CreateLocalizedString("A glowing, rainbow-hued pattern or interweaving colors fascinates those within it. The rainbow pattern fascinates all creatures within area, dazing them.");
                var onRun = rainbow.GetComponent<AbilityEffectRunAction>();
                var conditional = onRun.Actions.Actions.FirstOfType<Conditional>();
                var oldConditions = conditional.ConditionsChecker.Conditions;
                var newConditions = oldConditions
                    .RemoveFirst(cond => cond is ContextConditionHitDice)
                    .ToArray(); // reversed conditon
                conditional.ConditionsChecker.Conditions = newConditions;
                Main.Logger.Log($"Successfully installed Rainbow Pattern edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Rainbow Pattern spell! {e.Message}");
            }
        }
    }
}
