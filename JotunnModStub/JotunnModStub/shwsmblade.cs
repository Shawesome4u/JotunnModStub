using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;

namespace ShwsmBlade
{
    [BepInPlugin(Shawesome4u.ShwsmBlade, ShwsmBlade, "0.0.1")]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class JotunnModStub : BaseUnityPlugin
    {
        public const string PluginGUID = "Shawesome4u.ShwsmBlade";
        public const string PluginName = "ShwsmBlade";
        public const string PluginVersion = "0.0.1";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("The Skies Twist and Thunders Roar - The Blade of Shawesome Decends from the Heavens!");
            
            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
        }
    }
}