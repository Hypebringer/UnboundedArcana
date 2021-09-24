using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace UnboundedArcana.Utilities.Builders
{
    internal class ContextRankConfigBuilder
    {
        public ContextRankBaseValueType BaseValueType { get; set; } = ContextRankBaseValueType.CasterLevel;
        public ContextRankProgression Progression { get; set; } = ContextRankProgression.AsIs;
        public AbilityRankType Type { get; set; } = AbilityRankType.Default;
        public int? Min { get; set; } = null;
        public int? Max { get; set; } = null;
        public int StartLevel { get; set; } = 0;
        public int StepLevel { get; set; } = 0;
        public bool ExceptClasses { get; set; } = false;
        public StatType Stat { get; set; } = StatType.Unknown;
        public BlueprintUnitProperty CustomProperty { get; set; } = null;
        public BlueprintCharacterClass[] Classes { get; set; } = null;
        public BlueprintArchetype[] Archetypes { get; set; } = null;
        public BlueprintArchetype Archetype { get; set; } = null;
        public BlueprintFeature Feature { get; set; } = null;
        public BlueprintFeature[] FeatureList { get; set; } = null;
        public (int, int)[] CustomProgression { get; set; } = null;

        public ContextRankConfig Build()
        {
            return new ContextRankConfig()
            {
                m_Type = Type,
                m_BaseValueType = BaseValueType,
                m_Progression = Progression,
                m_UseMin = Min.HasValue,
                m_Min = Min.GetValueOrDefault(),
                m_UseMax = Max.HasValue,
                m_Max = Max.GetValueOrDefault(),
                m_StartLevel = StartLevel,
                m_StepLevel = StepLevel,
                m_Feature = Feature.ToReference<BlueprintFeatureReference>(),
                m_ExceptClasses = ExceptClasses,
                m_CustomProperty = CustomProperty.ToReference<BlueprintUnitPropertyReference>(),
                m_Stat = Stat,
                m_Class = Classes == null ? Array.Empty<BlueprintCharacterClassReference>() : Classes.Select(c => c.ToReference<BlueprintCharacterClassReference>()).ToArray(),
                Archetype = Archetype.ToReference<BlueprintArchetypeReference>(),
                m_AdditionalArchetypes = Archetypes == null ? Array.Empty<BlueprintArchetypeReference>() : Archetypes.Select(c => c.ToReference<BlueprintArchetypeReference>()).ToArray(),
                m_FeatureList = FeatureList == null ? Array.Empty<BlueprintFeatureReference>() : FeatureList.Select(c => c.ToReference<BlueprintFeatureReference>()).ToArray()
            };
        }
    }
}
