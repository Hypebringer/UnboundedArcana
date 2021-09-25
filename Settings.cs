using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace UnboundedArcana
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool InstallCantripScaling { get; set; }
        public bool InstallSpellsWithoutHDLimits { get; set; }
        public bool InstallFeatProgressionChanges { get; set; }
        public override string ToString() => $"Unbounded Arcana Settings: {{ InstallCantripScaling: {InstallCantripScaling}, InstallSpellsWithoutHDLimits: {InstallSpellsWithoutHDLimits}, InstallFeatProgressionChanges: {InstallFeatProgressionChanges} }}";
    }
}
