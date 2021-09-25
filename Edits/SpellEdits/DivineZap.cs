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
        public static void EditDivineZap()
        {
            const string divineZapGuid = "8a1992f59e06dd64ab9ba52337bf8cb5";

            try
            {
                var divineZap = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(divineZapGuid);
                divineZap.m_Description = CreateLocalizedString("You unleash your divine powers against a single target. The target takes 1d3 + half of your caster level (max 10) points of divine damage. A successful save halves the damage.");
                var contextRankConfig = new ContextRankConfigBuilder
                {
                    BaseValueType = ContextRankBaseValueType.CasterLevel,
                    Type = AbilityRankType.DamageBonus,
                    Progression = ContextRankProgression.Div2,
                    Max = 10
                }.Build();
                divineZap.AddComponent(contextRankConfig);
                var runAction = divineZap.GetComponent<AbilityEffectRunAction>();
                var dealDamageAction = runAction.Actions.Actions.FirstOfType<ContextActionDealDamage>();
                dealDamageAction.Value.BonusValue = new ContextValue
                {
                    ValueType = ContextValueType.Rank,
                    ValueRank = AbilityRankType.DamageBonus
                };

                Main.Logger.Log($"Successfully installed Divine Zap edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Divine Zap spell! {e.Message}");
            }
        }
    }
}
