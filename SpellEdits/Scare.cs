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
using static UnboundedArcana.Utilities.OwlcatUtilites;


namespace UnboundedArcana
{
    partial class SpellEdits
    {
        public static void EditScare()
        {
            const string scareGuid = "08cb5f4c3b2695e44971bf5c45205df0";

            try
            {
                var scare = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(scareGuid);
                scare.m_Description = CreateLocalizedString("All living targets are frightened for 1 round per level (shaken for 1 round on will save). Scare dispels remove fear.");
                var onRun = scare.GetComponent<AbilityEffectRunAction>();
                var conditional = onRun.Actions.Actions.FirstOfType<Conditional>();
                var oldConditions = conditional.ConditionsChecker.Conditions;
                var newConditions = oldConditions
                    .RemoveFirst(cond => cond is ContextConditionHitDice)
                    .ToArray(); // reversed conditon
                conditional.ConditionsChecker.Conditions = newConditions;
                Main.Logger.Log($"Successfully installed Scare edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Scare spell! {e.Message}");
            }
        }
    }
}
