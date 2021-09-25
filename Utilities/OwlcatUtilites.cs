using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using UnboundedArcana.Extensions;
using UnityEngine;

namespace UnboundedArcana.Utilities
{
    static class OwlcatUtilites
    {
        public static ActionList CreateActionList(params GameAction[] actions)
        {
            if (actions == null || actions.Length == 0) actions = Array.Empty<GameAction>();
            return new ActionList() { Actions = actions };
        }
        public static BlueprintGuid CreateBlueprintGuid(string raw) => new BlueprintGuid(new Guid(raw));
        public static LocalizedString CreateLocalizedString(string raw)
        {
            var taggedDescription = TagUtilities.TagEncyclopediaEntries(raw);
            var localizedDescription = SaveLocalizedString(taggedDescription);
            return localizedDescription;
        }

        private static LocalizedString SaveLocalizedString(string value)
        {
            var strings = LocalizationManager.CurrentPack.Strings;
            var key = Guid.NewGuid().ToString();
            strings[key] = value;

            var localized = new LocalizedString
            {
                Key = key
            };

            return localized;
        }
    }
}
