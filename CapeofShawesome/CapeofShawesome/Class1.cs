using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Configs;
using Jotunn.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace shawcape
{
    [BepInPlugin("IDshawesome4u", "shawcape", "0.0.1")]
    public class CapeofShawesome : BaseUnityPlugin
    {


        public Harmony harmony;
        public string version = "0.0.1";

        public static BepInEx.Logging.ManualLogSource harmonyLog;
        private static AssetBundle Shawcassets;

        public void Awake()
        {
            harmony = new Harmony("IDshawesome4u");
            harmony.PatchAll();
            harmonyLog = Logger;

            harmonyLog.LogWarning("Cape of Shawesome Decends from the Heavens...");
            Shawcassets = AssetUtils.LoadAssetBundleFromResources("Shawcassets", Assembly.GetExecutingAssembly());

            PrefabManager.OnVanillaPrefabsAvailable += Capeofshaw;



        }

        // Add custom recipes
        private void AddRecipes()
        {
            // Create a custom recipe with a RecipeConfig
            ItemConfig scapeConfig = new ItemConfig();

            scapeConfig.AddRequirement(new RequirementConfig("CapeLinen", 2));
            scapeConfig.AddRequirement(new RequirementConfig("TrophyWolf", 1));
            scapeConfig.AddRequirement(new RequirementConfig("Coins", 404));
            scapeConfig.AddRequirement(new RequirementConfig("Chain", 1));
            CustomItem ShawesomeCape = new CustomItem("ShawesomeCape", "CapeWolf", scapeConfig);
            ItemManager.Instance.AddItem(ShawesomeCape);


            StatusEffect dripeffect = AddStatusEffects().StatusEffect;


            var id = ShawesomeCape.ItemDrop;
            var id2 = id.m_itemData;
            id2.m_shared.m_name = "Cape of Shawesome";
            id2.m_shared.m_description = "Looks Great! Feels Great! Worth Every Coin!";
            id2.m_shared.m_equipStatusEffect = dripeffect;

        }



        public void Capeofshaw()
        {
            AddRecipes();


            Transform CapeTransform = PrefabManager.Instance.GetPrefab("ShawesomeCape").transform.Find("attach_skin").transform;
            Transform CapeTransform1 = CapeTransform.Find("WolfCape_Cloth").transform;
            Transform CapeTransform2 = CapeTransform1.Find("WolfCape_cloth").transform;
            SkinnedMeshRenderer scskinmesh = CapeTransform2.GetComponent<SkinnedMeshRenderer>();
            Material scmat = scskinmesh.material;
            scmat.color = Color.red;



        }

        // Add new status effects
        private CustomStatusEffect AddStatusEffects()
        {
            StatusEffect effect = ScriptableObject.CreateInstance<StatusEffect>();
            effect.name = "MaximumDrip";
            effect.m_name = "It is said the Mighty Drip Gods Themselves soaked the linen in the Blood of the World Serpent... What potent Magical properties must lie within its threads...";
            effect.m_icon = AssetUtils.LoadSpriteFromFile("CapeofShawesome/Properties/Shawcassets");
            effect.m_startMessageType = MessageHud.MessageType.Center;
            effect.m_startMessage = "The Gods Remark at your Impeccable Drip...";
            effect.m_stopMessageType = MessageHud.MessageType.Center;
            effect.m_stopMessage = "Your Drip Fades...";




            CustomStatusEffect MaximumDrip = new CustomStatusEffect(effect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            ItemManager.Instance.AddStatusEffect(MaximumDrip);
            return MaximumDrip;



        }












        #region Utils

        #endregion

        #region Transpilers

        #endregion

        #region Patches 

        #endregion
    }
}