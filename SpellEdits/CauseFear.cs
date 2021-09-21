using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using UnboundedArcana.Extensions;
using static UnboundedArcana.Utilities.Blueprint;
using static UnboundedArcana.Utilities.Conditions;

namespace UnboundedArcana
{
    static partial class SpellEdits
    {
        public static void EditCauseFear()
        {
            var causeFear = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(CreateBlueprintGuid("bd81a3931aa285a4f9844585b5d97e51"));
            causeFear.m_Description = CreateLocalizedString("The affected creature becomes frightened for 1d4 rounds. If the subject succeeds at a Will save, it is shaken for 1 round instead. Cause fear dispels remove fear.");
            var onRun = causeFear.GetComponent<AbilityEffectRunAction>();
            var scareTargetAction = onRun.Actions.Actions.FirstOfType<Conditional>().IfFalse.Actions.FirstOfType<Conditional>();
            scareTargetAction.ConditionsChecker.Conditions = new Condition[] { new ContextConditionAlwaysTrue() };
        }
    }
}
