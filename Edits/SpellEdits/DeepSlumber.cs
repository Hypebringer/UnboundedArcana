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
        public static void EditDeepSlumber()
        {
            const string deepSlumberGuid = "7658b74f626c56a49939d9c20580885e";
            const string deepSlumberKitsuneGuid = "2bc8d4bb8baa23a4b84ef34945d13733";

            try
            {
                var deepSlumber = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(deepSlumberGuid);
                var deepSlumberKitsune = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(deepSlumberKitsuneGuid);

                foreach (var variant in new[] { deepSlumber, deepSlumberKitsune })
                {
                    var hitDiceLimit = variant.GetComponent<ContextCalculateSharedValue>();
                    hitDiceLimit.Value.BonusValue = new ContextValue { ValueType = ContextValueType.Rank, ValueRank = AbilityRankType.Default };
                    variant.m_Description = CreateLocalizedString("This spell functions like sleep, except that it affects 10 + caster level (max 10) HD of targets.");
                    variant.RemoveComponent(x => x is ContextRankConfig);
                    var contextRankConfig = new ContextRankConfigBuilder
                    {
                        Max = 10,
                        Progression = ContextRankProgression.BonusValue,
                        StepLevel = 10,
                        BaseValueType = ContextRankBaseValueType.CasterLevel
                    }.Build();
                    variant.AddComponent(contextRankConfig);
                }

                Main.Logger.Log($"Successfully installed Deep Slumber edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Deep Slumber spell! {e.Message}");
            }
        }
    }
}
