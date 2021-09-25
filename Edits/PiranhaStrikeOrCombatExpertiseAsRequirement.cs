using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using UnboundedArcana.Extensions;
using UnboundedArcana.Utilities;
using static UnboundedArcana.Utilities.OwlcatUtilites;

namespace UnboundedArcana.Edits
{
    static class PiranhaStrikeOrCombatExpertiseAsRequirement
    {
        public static void Edit()
        {
            try
            {
                var featuresGuids = new string[] { "ed699d64870044b43bb5a7fbe3f29494", "52c6b07a68940af41b270b3710682dc7", "0f15c6f70d8fb2b49aa6cc24239cc5fa", "4cc71ae82bdd85b40b3cfe6697bb7949", "25bc9c439ac44fd44ac3b1e58890916f", "63d8e3a9ab4d72e4081a7862d7246a79" };
                var combatExpertiseGuid = CreateBlueprintGuid("4c44724ffa8844f4d9bedb5bb27d144a");
                var combatExpertiseFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(combatExpertiseGuid);
                var piranhaStrikeGuid = CreateBlueprintGuid("6a556375036ac8b4ebd80e74d308d108");
                var piranhaStrikeFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(piranhaStrikeGuid);
                var kineticWarriorGuid = CreateBlueprintGuid("ff14cb2bfab1c0547be66d8aaa7e4ada");
                var kineticWarriorFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(kineticWarriorGuid);

                foreach (var guid in featuresGuids)
                {
                    var blueprint = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(guid);
                    BlueprintFeatureReference[] references;
                    if (blueprint.Components.FirstOrDefault(x => x is PrerequisiteFeature ftr && ftr.m_Feature.Guid == kineticWarriorGuid) != default)
                    {
                        references = new BlueprintFeatureReference[]
                        {
                            piranhaStrikeFeature.ToReference<BlueprintFeatureReference>(),
                            combatExpertiseFeature.ToReference<BlueprintFeatureReference>(),
                            kineticWarriorFeature.ToReference<BlueprintFeatureReference>()
                        };
                    }
                    else
                    {
                        references = new BlueprintFeatureReference[]
                        {
                            piranhaStrikeFeature.ToReference<BlueprintFeatureReference>(),
                            combatExpertiseFeature.ToReference<BlueprintFeatureReference>()
                        };
                    }

                    var newRequirement = new PrerequisiteFeaturesFromList
                    {
                        m_Features = references,
                        Amount = 1
                    };

                    blueprint.RemoveComponents(x => x is PrerequisiteFeature ftr 
                        && !featuresGuids.Contains(ftr.m_Feature.Guid.ToString()));
                    blueprint.AddComponent(newRequirement);
                }
                Main.Logger.Log($"Successfully installed combat maneuvers feats requirements edit!");
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Error when trying to edit combat maneuvers feats requirements! {ex.Message}");
            }
        }
    }
}
