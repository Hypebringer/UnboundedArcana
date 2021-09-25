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
using static UnboundedArcana.Utilities.OwlcatUtilites;

namespace UnboundedArcana.Edits
{
    static partial class SpellsAndHD
    {
        public static void EditCauseFear()
        {
            const string causeFearGuid = "bd81a3931aa285a4f9844585b5d97e51";
            try
            {
                var causeFear = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(CreateBlueprintGuid(causeFearGuid));
                causeFear.m_Description = CreateLocalizedString("The affected creature becomes frightened for 1d4 rounds. If the subject succeeds at a Will save, it is shaken for 1 round instead. Cause fear dispels remove fear.");
                var onRun = causeFear.GetComponent<AbilityEffectRunAction>();
                var conditional = onRun.Actions.Actions.FirstOfType<Conditional>().IfFalse.Actions.FirstOfType<Conditional>();
                conditional.ConditionsChecker.Conditions = new Condition[] { };
                Main.Logger.Log($"Successfully installed Cause Fear edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Cause Fear spell! {e.Message}");
            }
        }
    }
}
