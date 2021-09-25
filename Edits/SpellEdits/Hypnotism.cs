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


namespace UnboundedArcana.Edits
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
                var hypnotismBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(hypnotismBuffGuid);
                hypnotism.m_Description = CreateLocalizedString("Your gestures and droning incantation fascinate nearby living creatures, causing them to stop and stare blankly at you in a dazed condition. At the end of their turn, the subjects may attempt a new saving throw to end the effect.");

                hypnotism.RemoveComponents(x => x is ContextCalculateSharedValue);

                // Add SharedValue for duration
                var newDurationValue = new ContextCalculateSharedValue
                {
                    ValueType = AbilitySharedValue.Duration,
                    Value = new ContextDiceValue
                    {
                        BonusValue = new ContextValue { ValueType = ContextValueType.Rank },
                        DiceType = Kingmaker.RuleSystem.DiceType.Zero,
                        DiceCountValue = 0
                    }
                };
                hypnotism.AddComponent(newDurationValue);

                // Decrease ability range, remove conditions
                var abilityTargetsAround = hypnotism.GetComponent<AbilityTargetsAround>();
                abilityTargetsAround.m_Radius.m_Value = 12;
                abilityTargetsAround.m_Condition.Conditions = new Condition[] { };

                // Change spell logic!
                hypnotism.RemoveComponent(x => x is AbilityEffectRunAction);
                var runAction = new AbilityEffectRunAction
                {
                    SavingThrowType = SavingThrowType.Will,
                    Actions = new ActionList
                    {
                        Actions = new[]
                            {
                            new ContextActionConditionalSaved
                            {
                                Succeed = CreateActionList(),
                                Failed = CreateActionList(new ContextActionApplyBuff
                                    {
                                        m_Buff = hypnotismBuff.ToReference<BlueprintBuffReference>(),
                                        DurationValue = new ContextDurationValue
                                        {
                                            Rate = DurationRate.Rounds,
                                            BonusValue = new ContextValue
                                            {
                                                ValueType = ContextValueType.Shared,
                                                ValueShared = AbilitySharedValue.Duration
                                            },
                                            DiceCountValue = 0
                                        },
                                        IsFromSpell = true
                                    })
                                }
                            }
                    }
                };
                hypnotism.AddComponent(runAction);


                
                var saveEachRoundCondition = new BuffStatusCondition
                {
                    SaveEachRound = true,
                    SaveType = SavingThrowType.Will,
                    Condition = UnitCondition.Dazed
                };

                Main.Logger.Log($"Successfully installed Hypnotism edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Hypnotism spell! {e.Message}");
            }
        }
    }
}
