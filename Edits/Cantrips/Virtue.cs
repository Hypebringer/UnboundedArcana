using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnboundedArcana.Extensions;
using UnboundedArcana.Utilities.Builders;
using static UnboundedArcana.Utilities.OwlcatUtilites;


namespace UnboundedArcana.Edits
{
    partial class Cantrips
    {
        public static void EditVirtue()
        {
            const string virtueGuid = "d3a852385ba4cd740992d1970170301a";
            const string virtueBuffGuid = "a13ad2502d9e4904082868eb71efb0c5";

            try
            {
                var virtue = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(virtueGuid);
                virtue.m_Description = CreateLocalizedString("With a touch, you infuse a creature with a tiny surge of life, granting the subject 1 + half of your caster level (max 10) temporary hit point.");

                var virtueBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(virtueBuffGuid);
                virtueBuff.m_Description = CreateLocalizedString("With a touch, you infuse a creature with a tiny surge of life, granting the subject 1 + half of your caster level (max 10) temporary hit point.");

                var contextRankConfig = new ContextRankConfigBuilder
                {
                    BaseValueType = ContextRankBaseValueType.CasterLevel,
                    Type = AbilityRankType.Default,
                    Progression = ContextRankProgression.OnePlusDiv2,
                    Max = 10
                }.Build();
                virtueBuff.AddComponent(contextRankConfig);

                var grantHitPoints = virtueBuff.GetComponent<TemporaryHitPointsFromAbilityValue>();
                grantHitPoints.Value = new ContextValue
                {
                    ValueType = ContextValueType.Rank,
                    ValueRank = AbilityRankType.Default
                };

                Main.Logger.Log($"Successfully installed Virtue edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Virtue spell! {e.Message}");
            }
        }
    }
}
