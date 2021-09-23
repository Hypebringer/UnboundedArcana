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
using static UnboundedArcana.Utilities.OwlcatUtilites;


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

                // Remove SharedValues
                var components = hypnotism.Components
                    .Where(comp => !(comp is ContextCalculateSharedValue));

                // Add SharedValue for duration
                var newDurationValue = new ContextCalculateSharedValue
                {
                    ValueType = AbilitySharedValue.Duration,
                    Value = new ContextDiceValue
                    {
                        BonusValue = new ContextValue { ValueType = ContextValueType.Rank }
                    }
                };
                components = components.ConcatSingle(newDurationValue);
                hypnotism.Components = components.ToArray();

                // Decrease ability range, remove conditions
                var abilityTargetsAround = hypnotism.GetComponent<AbilityTargetsAround>();
                abilityTargetsAround.m_Radius.m_Value = 12;
                abilityTargetsAround.m_Condition.Conditions = new Condition[] { };

                // Change spell logic!
                hypnotism.m_Overrides.Remove("$AbilityCalculateSharedValue$8a3f517b-295c-4d88-a31b-91641c420fa9");
                var onRun = hypnotism.GetComponent<AbilityEffectRunAction>();

                // Remove hit dice initial condition
                var initialCondChecker = onRun.Actions.Actions.FirstOfType<Conditional>().ConditionsChecker;
                initialCondChecker.Conditions = initialCondChecker.Conditions
                    .Remove(x => x is ContextConditionHitDice)
                    .ToArray();

                // Different condition branches for undead and living
                var afterBasicChecks = onRun.Actions.Actions.FirstOfType<Conditional>().IfFalse;

                var undeadAction = afterBasicChecks
                    .Actions.FirstOfType<Conditional>().IfTrue
                    .Actions.FirstOfType<Conditional>().IfTrue;

                var aliveAction = afterBasicChecks
                    .Actions.FirstOfType<Conditional>().IfFalse;

                // Akcje zaczynając od tego miejsca:
                // ContextActionSavingThrow/ContextActionConditionalSaved/ContextActionApplyBuff - change duration
                // ContextActionChangeSharedValue - to remove

                foreach (var actionList in new[] { undeadAction, aliveAction })
                {
                    actionList.Actions = actionList.Actions
                        .Remove(x => x is ContextActionChangeSharedValue)
                        .ToArray();

                    var applyBuffAction = actionList.Actions.FirstOfType<ContextActionSavingThrow>()
                        .Actions.Actions.FirstOfType<ContextActionConditionalSaved>()
                        .Failed.Actions.FirstOfType<ContextActionApplyBuff>();
                    applyBuffAction.DurationValue = new ContextDurationValue
                    {
                        m_IsExtendable = true,
                        BonusValue = new ContextValue
                        {
                            ValueType = ContextValueType.Shared,
                            ValueShared = AbilitySharedValue.Duration
                        }
                    };
                }


                var hypnotismBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(hypnotismBuffGuid);
                var saveEachRoundCondition = new BuffStatusCondition
                {
                    SaveEachRound = true,
                    SaveType = SavingThrowType.Will,
                    Condition = UnitCondition.Dazed
                };

                hypnotismBuff.Components = hypnotismBuff.Components
                    .Remove(x => x is AddCondition)
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
