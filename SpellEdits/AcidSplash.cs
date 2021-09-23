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
using static UnboundedArcana.Utilities.OwlcatUtilites;


namespace UnboundedArcana
{
    partial class SpellEdits
    {
        public static void EditAcidSplash()
        {
            const string acidSplashGuid = "0c852a2405dd9f14a8bbcfaf245ff823";

            try
            {
                var acidSplash = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(acidSplashGuid);
                acidSplash.m_Description = CreateLocalizedString("You fire a small orb of acid at the target. You must succeed on a ranged touch attack to hit your target. The orb deals 1d3 + half of your caster level (max 10) points of acid damage.");
                var contextRankConfig = new ContextRankConfig
                {
                    m_BaseValueType = ContextRankBaseValueType.CasterLevel,
                    m_Type = AbilityRankType.DamageBonus,
                    m_Progression = ContextRankProgression.Div2,
                    m_Max = 20
                };
                acidSplash.AddComponent(contextRankConfig);
                var runAction = acidSplash.GetComponent<AbilityEffectRunAction>();
                var dealDamageAction = runAction.Actions.Actions.FirstOfType<ContextActionDealDamage>();
                dealDamageAction.Value.BonusValue = new ContextValue
                {
                    ValueType = ContextValueType.Rank,
                    ValueRank = AbilityRankType.DamageBonus
                };

                Main.Logger.Log($"Successfully installed Acid Splash edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Acid Splash spell! {e.Message}");
            }
        }
    }
}
