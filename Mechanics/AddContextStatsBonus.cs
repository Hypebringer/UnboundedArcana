using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;

namespace UnboundedArcana.Mechanics
{
    /// <summary>
    /// Ignoring Powerful Change feature, as it is not relevant for what we're trying to do. (Scaling Guidance.)
    /// Applies same bonus to multiple stats.
    /// </summary>
    public class AddContextStatsBonus : UnitFactComponentDelegate
    {
        public ModifierDescriptor Descriptor;

        public StatType[] Stats;

        public int Multiplier = 1;

        public ContextValue Value;

        public bool HasMinimal;

        public int Minimal;

        public override void OnTurnOn()
        {
#if DEBUG
            Main.Logger.Log($"Entering OnTurnOn.");
#endif
            int num = CalculateBaseValue(base.Fact.MaybeContext);
            foreach (var statType in Stats)
            {

                ModifiableValue stat = base.Owner.Stats.GetStat(statType);
                if (HasMinimal)
                {
                    stat.AddModifier(Math.Max(num, Minimal), base.Runtime, Descriptor);
                }
                else
                {
                    stat.AddModifier(num, base.Runtime, Descriptor);
                }
            }
        }

        public override void OnTurnOff()
        {
            foreach (var stat in Stats)
                base.Owner.Stats.GetStat(stat)?.RemoveModifiersFrom(base.Runtime);
        }

        public int CalculateBaseValue(MechanicsContext context)
        {
#if DEBUG
            Main.Logger.Log($"Entering CalculateBaseValue.");
#endif
            if (context == null)
            {
                Main.Logger.Error("Context is missing");
                return 0;
            }
            var calculated = Value.Calculate(context);
#if DEBUG
            Main.Logger.Log($"Inside CalculateBaseValue. Value is {calculated}. Multiplier is {Multiplier}. Caster level is {context.Params?.CasterLevel}");
#endif
            return calculated * Multiplier;
        }
    }
}
