using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.ElementsSystem;

namespace UnboundedArcana.Utilities
{
    static class Conditions
    {
        public class ContextConditionAlwaysTrue : Condition
        {
            public override bool CheckCondition() => true;
            public override string GetConditionCaption() => null;
        }
    }
}
