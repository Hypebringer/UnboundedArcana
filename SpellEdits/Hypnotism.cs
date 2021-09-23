using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using UnboundedArcana.Extensions;
using static UnboundedArcana.Utilities.Blueprint;
using static UnboundedArcana.Extensions.ReflectionExtensions;


namespace UnboundedArcana
{
    partial class SpellEdits
    {
        public static void EditHypnotism()
        {
            const string hypnotismGuid = "88367310478c10b47903463c5d0152b0";
            const string hypnotismBuffGuid = "daebec1cd104ad4448d900892149d7aa";

            try
            {
                var hypnotism = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(hypnotismGuid);
                hypnotism.m_Description = CreateLocalizedString("Your gestures and droning incantation fascinate nearby living creatures, causing them to stop and stare blankly at you in a dazed condition. At the end of their turn, the subjects may attempt a new saving throw to end the effect.");

                var contextCalculateSharedDuration = hypnotism
                    .GetComponents<ContextCalculateSharedValue>()
                    .First(x => x.ValueType == AbilitySharedValue.Duration);
                var newCalculateSharedValue = new ContextDiceValue
                {
                    BonusValue = new ContextValue { ValueType = ContextValueType.Rank }
                };
                contextCalculateSharedDuration.Value = newCalculateSharedValue;

                var newContextRunConfig = new ContextRankConfig
                {
                    m_BaseValueType = ContextRankBaseValueType.MaxCasterLevel,
                    m_Progression = ContextRankProgression.AsIs
                };
                hypnotism.ComponentsArray = hypnotism.ComponentsArray
                    .ConcatSingle(newContextRunConfig)
                    .ToArray();


                var abilityTargetsAround = hypnotism.GetComponent<AbilityTargetsAround>();
                abilityTargetsAround.m_Radius.m_Value = 15;
                abilityTargetsAround.m_Condition.Conditions = new Condition[] { };

                var onRun = hypnotism.GetComponent<AbilityEffectRunAction>();
                var firstConditionChecker = onRun.Actions.Actions.FirstOfType<Conditional>().ConditionsChecker;
                firstConditionChecker.Conditions = firstConditionChecker.Conditions
                    .Where(x => x.IsNotType<ContextConditionHitDice>())
                    .ToArray();


                var afterBasicChecks = onRun.Actions.Actions.FirstOfType<Conditional>().IfFalse;
                var undeadBranch = afterBasicChecks
                    .Actions.FirstOfType<Conditional>().IfTrue;

                var nonUndeadBranch = afterBasicChecks
                    .Actions.FirstOfType<Conditional>().IfFalse;

                var undeadBuff = undeadBranch.Actions.FirstOfType<Conditional>()
                    .IfTrue.Actions.FirstOfType<ContextActionSavingThrow>()
                    .Actions.Actions.FirstOfType<ContextActionConditionalSaved>()
                    .Failed.Actions.FirstOfType<ContextActionApplyBuff>();
                undeadBuff.DurationValue = new ContextDurationValue
                {
                    m_IsExtendable = true,
                    BonusValue = new ContextValue
                    {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Duration
                    }
                };


                var nonUndeadBuff = nonUndeadBranch
                    .Actions.FirstOfType<ContextActionSavingThrow>()
                    .Actions.Actions.FirstOfType<ContextActionConditionalSaved>()
                    .Failed.Actions.FirstOfType<ContextActionApplyBuff>();
                nonUndeadBuff.DurationValue = new ContextDurationValue
                {
                    m_IsExtendable = true,
                    BonusValue = new ContextValue
                    {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Duration
                    }
                };

                var hypnotismBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(hypnotismBuffGuid);
                var saveEachRoundCondition = new BuffStatusCondition
                {
                    SaveEachRound = true,
                    SaveType = SavingThrowType.Will,
                    Condition = UnitCondition.Dazed
                };

                hypnotismBuff.Components = hypnotismBuff.Components
                    .Where(x => x.IsNotType<AddCondition>())
                    .ConcatSingle(saveEachRoundCondition)
                    .ToArray();

                Main.Logger.Log($"Successfully installed Hypnotism edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Hypnotism spell! {e.Message}");
            }
        }
    }
}
