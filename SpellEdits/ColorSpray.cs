using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
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
        public static void EditColorSpray()
        {
            const string colorSprayGuid = "91da41b9793a4624797921f221db653c";
            const string colorSprayOreadGuid = "b45ef568c664c6643982ba089339dbfc";
            const string colorSprayStunAndBlindBuffGuid = "e99c041297c835848b23ecbde8f50e63";
            const string colorSprayBlindBuffGuid = "32b1096634c459e40acc6baf7b3d86d2";

            try
            {
                var colorSpray = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(colorSprayGuid);
                var colorSprayOread = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>(colorSprayOreadGuid);
                var colorSprayStunAndBlindBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(colorSprayStunAndBlindBuffGuid);
                var colorSprayBlindBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(colorSprayBlindBuffGuid);

                colorSpray.m_Description = CreateLocalizedString("A vivid cone of clashing colors springs forth from your hand, causing creatures to become blinded for 1 + 1d4 rounds and stunned for 1 round.");
                colorSpray.LocalizedDuration = CreateLocalizedString("1 + 1d4 rounds blinded, 1 round stunned");

                foreach (var variant in new[] {colorSpray, colorSprayOread})
                {
                    variant.RemoveComponent(x => x is AbilityEffectRunAction);

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
                                        m_Buff = colorSprayStunAndBlindBuff.ToReference<BlueprintBuffReference>(),
                                        DurationValue = new ContextDurationValue
                                        {
                                            Rate = DurationRate.Rounds,
                                            BonusValue = 1,
                                            DiceCountValue = 0
                                        },
                                        IsFromSpell = true
                                    })
                                }
                            }
                        }
                    };
                    variant.AddComponent(runAction);
                }

                colorSprayStunAndBlindBuff.m_Description = CreateLocalizedString("Target is stunned and blinded.");
                colorSprayBlindBuff.m_Description = CreateLocalizedString("Target is blinded.");

                colorSprayStunAndBlindBuff.RemoveComponents(x =>
                    x is BuffStatusCondition status && status.Condition != UnitCondition.Stunned && status.Condition != UnitCondition.Blindness);

                var onEnd = colorSprayStunAndBlindBuff.GetComponent<AddFactContextActions>();
                var activateSecondBuff = new ContextActionApplyBuff
                {
                    m_Buff = colorSprayBlindBuff.ToReference<BlueprintBuffReference>(),
                    IsFromSpell = true,
                    DurationValue = new ContextDurationValue
                    {
                        DiceType = DiceType.D4,
                        DiceCountValue = 1,
                        Rate = DurationRate.Rounds,
                        BonusValue = 0
                    },
                };
                onEnd.Deactivated.Actions = new GameAction[] { activateSecondBuff };


                colorSprayBlindBuff.RemoveComponents(
                    x => (x is BuffStatusCondition status && status.Condition == UnitCondition.Stunned) || x is AddFactContextActions || x is BuffExtraEffects);
                colorSprayBlindBuff.ReplaceComponent(x => x is SpellDescriptorComponent, new SpellDescriptorComponent
                {
                    Descriptor = SpellDescriptor.Blindness
                });

                Main.Logger.Log($"Successfully installed Color Spray edit!");
            }
            catch (Exception e)
            {
                Main.Logger.Error($"Error when trying to edit Color Spray spell! {e.Message}");
            }
        }
    }
}
