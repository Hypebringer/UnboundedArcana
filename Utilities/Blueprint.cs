using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Localization;

namespace UnboundedArcana.Utilities
{
    static class Blueprint
    {
        public static BlueprintGuid CreateBlueprintGuid(string raw) => new BlueprintGuid(new Guid(raw));
        public static LocalizedString CreateLocalizedString(string raw)
        {
            var taggedDescription = Tag.TagEncyclopediaEntries(raw);
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
