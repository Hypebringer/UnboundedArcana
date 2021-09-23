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
        public static void EditDeepSlumber()
        {
            const string deepSlumberGuid = "7658b74f626c56a49939d9c20580885e";

            try
            {
                var deepSlumber = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(deepSlumberGuid);
                deepSlumber.m_Description = CreateLocalizedString("This spell functions like sleep, except that it affects 10 + caster level (max 10) HD of targets.");


                var contextCalculateSharedValue = deepSlumber.GetComponent<ContextCalculateSharedValue>();
                contextCalculateSharedValue.Value.DiceCountValue = null;

                var hitDiceLimit = deepSlumber.GetComponent<ContextCalculateSharedValue>();
                hitDiceLimit.Value.BonusValue = new ContextValue { ValueType = ContextValueType.Rank };


                var hitDiceConfig = deepSlumber.GetComponent<ContextRankConfig>();
                hitDiceConfig.m_Max = 10;
                hitDiceConfig.m_Progression = ContextRankProgression.BonusValue;
                hitDiceConfig.m_StepLevel = 10;
                hitDiceConfig.m_BaseValueType = ContextRankBaseValueType.MaxCasterLevel;

                Main.Logger.Log($"Successfully installed Deep Slumber edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Deep Slumber spell! {e.Message}");
            }
        }
    }
}
