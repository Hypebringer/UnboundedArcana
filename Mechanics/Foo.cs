using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
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
            int num = CalculateBaseValue(base.Fact.MaybeContext);
            foreach (var stat in Stats)
            {

                ModifiableValue stat_ = base.Owner.Stats.GetStat(stat);
                if (HasMinimal)
                {
                    stat_.AddModifier(Math.Max(num, Minimal), base.Runtime, Descriptor);
                }
                else
                {
                    stat_.AddModifier(num, base.Runtime, Descriptor);
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
            if (context == null)
            {
                Main.Logger.Error("Context is missing");
                return 0;
            }

            return Value.Calculate(context) * Multiplier;
        }
    }
}
