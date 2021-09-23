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
        public static void EditRayOfFrost()
        {
            const string rayOfFrostGuid = "9af2ab69df6538f4793b2f9c3cc85603";

            try
            {
                var rayOfFrost = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(rayOfFrostGuid);
                rayOfFrost.m_Description = CreateLocalizedString("A ray of freezing air and ice projects from your pointing finger. You must succeed on a ranged touch attack with the ray to deal damage to a target. The ray deals 1d3 + half of your caster level (max 10) points of cold damage.");
                var contextRankConfig = new ContextRankConfig
                {
                    m_BaseValueType = ContextRankBaseValueType.CasterLevel,
                    m_Type = AbilityRankType.DamageBonus,
                    m_Progression = ContextRankProgression.Div2,
                    m_Max = 20
                };
                rayOfFrost.AddComponent(contextRankConfig);
                var runAction = rayOfFrost.GetComponent<AbilityEffectRunAction>();
                var dealDamageAction = runAction.Actions.Actions.FirstOfType<ContextActionDealDamage>();
                dealDamageAction.Value.BonusValue = new ContextValue
                {
                    ValueType = ContextValueType.Rank,
                    ValueRank = AbilityRankType.DamageBonus
                };

                Main.Logger.Log($"Successfully installed Ray of Frost edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Ray of Frost spell! {e.Message}");
            }
        }
    }
}
