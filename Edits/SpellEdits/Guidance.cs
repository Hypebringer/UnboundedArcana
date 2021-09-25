using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.QA;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnboundedArcana.Extensions;
using UnboundedArcana.Mechanics;
using UnboundedArcana.Utilities.Builders;
using static UnboundedArcana.Utilities.OwlcatUtilites;


namespace UnboundedArcana.Edits
{
    partial class SpellEdits
    {
        public static void EditGuidance()
        {
            const string guidanceGuid = "c3a8f31778c3980498d8f00c980be5f5";
            const string guidanceBuffGuid = "ec931b882e806ce42906597e5585c13f";

            try
            {
                var guidance = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(guidanceGuid);
                guidance.m_Description = CreateLocalizedString("This spell imbues the subject with a touch of divine guidance. The creature gets a +1 competence bonus on a single attack roll, saving throw, or skill check. This bonus increases to +2 at 8th level and +3 at 16th level");

                var guidanceBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(guidanceBuffGuid);
                guidanceBuff.m_Description = CreateLocalizedString("This spell imbues the subject with a touch of divine guidance. The creature gets a +1 competence bonus on a single attack roll, saving throw, or skill check. This bonus increases to +2 at 8th level and +3 at 16th level");

                var contextRankConfig = new ContextRankConfigBuilder
                {
                    BaseValueType = ContextRankBaseValueType.CasterLevel,
                    Type = AbilityRankType.Default,
                    StepLevel = 8,
                    Progression = ContextRankProgression.OnePlusDivStep
                }.Build();
                guidanceBuff.AddComponent(contextRankConfig);

                guidanceBuff.RemoveComponents(x => x is BuffAllSavesBonus || x is AddStatBonus || x is BuffAllSkillsBonus);

                var addContextStatsBonus = new AddContextStatsBonus
                {
                    Descriptor = ModifierDescriptor.Competence,
                    Stats = StatTypeHelper.Skills
                        .Concat(StatTypeHelper.Saves)
                        .ConcatSingle(StatType.AdditionalAttackBonus)
                        .ToArray(),
                    Value = new ContextValue
                    {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    }
                };
                guidanceBuff.AddComponent(addContextStatsBonus);

                Main.Logger.Log($"Successfully installed Guidance edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Guidance spell! {e.Message}");
            }
        }
    }
}
