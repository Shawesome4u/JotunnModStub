using BepInEx;
using HarmonyLib;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System.Reflection;
using UnityEngine;

namespace shawcape
{
    [BepInPlugin("IDshawesome4u", "Shawcapes", "0.0.1")]
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

        private void AddRecipes()
        {
            // Create a custom recipe with a RecipeConfig
            ItemConfig scapeConfig = new ItemConfig();

            scapeConfig.CraftingStation = CraftingStations.Forge;
            scapeConfig.AddRequirement(new RequirementConfig("Flametal", 4, 4));
            scapeConfig.AddRequirement(new RequirementConfig("MushroomYellow", 20));
            scapeConfig.AddRequirement(new RequirementConfig("Thunderstone", 10, 10));
            scapeConfig.AddRequirement(new RequirementConfig("CapeLinen", 2));
            scapeConfig.AddRequirement(new RequirementConfig("Silver", 4, 4));
            scapeConfig.AddRequirement(new RequirementConfig("TrophyWolf", 1));
            scapeConfig.AddRequirement(new RequirementConfig("Coins", 404, 400));
            scapeConfig.AddRequirement(new RequirementConfig("Chain", 1));
            CustomItem ShawesomeCape = new CustomItem("ShawesomeCape", "CapeWolf", scapeConfig);
            ItemManager.Instance.AddItem(ShawesomeCape);

            
            

            StatusEffect dripeffect = AddStatusEffects().StatusEffect;

            //Assigning item ID
            var id = ShawesomeCape.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Cape of Shawesome";
            id2.m_shared.m_description = "A Divine Armament, Crafted by Odin, Blessed by Thor, A Gift from the gods to the Mighty Shawesome for his Ascension to the godly Realm... ";
            id2.m_shared.m_equipStatusEffect = dripeffect;
            id2.m_shared.m_armor = 4;
            id2.m_shared.m_armorPerLevel = 1;
            id2.m_shared.m_maxDurability = 4000;
            id2.m_shared.m_maxQuality = 7;
            id2.m_shared.m_eitrRegenModifier = 0.44f;
            id2.m_shared.m_weight = 4;
            id2.m_shared.m_setStatusEffect = dripeffect;
            id2.m_shared.m_durabilityPerLevel = 400;
            id2.m_shared.m_movementModifier = 0.44f;

            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Resistant;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Immune;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] scicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/Scapeimage.png");
            id2.m_shared.m_icons[0] = scicon[1] as Sprite;








        }

        // making copy of sparcs and set the parent of the copy to wherever you want it to be
        public static T CopyIntoParent<T>(T go, T parent) where T : Component
        {
            var CompCopy = InstantiatePrefab.Instantiate(go);
            CompCopy.name = go.name;
            CompCopy.transform.parent = parent.transform;
            CompCopy.transform.localPosition = new Vector3(0, 0, 0);
            return CompCopy;
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
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_d.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_e.png");
            UnityEngine.Object[] sparcs = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/Sparcs.prefab");
            
            //Adding textures and emissions 
            scmat.SetTexture("_MainTex", capetexture[0] as Texture2D);
            scmat.EnableKeyword("_EMISSION");
            scmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            scmat.SetColor("_EmissionColor", new Color(1, 1, 0, 1));

            //Making sparcs visable *testing 
            GameObject sparcsobject = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/Sparcs.prefab");
            sparcsobject.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            CopyIntoParent(sparcsobject.transform, CapeTransform1);
            harmonyLog.LogWarning(sparcsobject.transform.parent);
            sparcsobject.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            sparcsobject.transform.localPosition = new Vector3(0.1f, 1, 0.3f);
            





        }

        // Add new status effects
        private CustomStatusEffect AddStatusEffects()
        {
            StatusEffect dripeffect = ScriptableObject.CreateInstance<StatusEffect>();
            dripeffect.name = "MaximumDrip";
            dripeffect.m_name = "It is said the Mighty Drip Gods Themselves soaked the linen in the Blood of the World Serpent... What potent Magical properties must lie within its threads...";
            
            UnityEngine.Object[] loadedsprite = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/Sentry.png");
            dripeffect.m_icon = loadedsprite[0] as Sprite;
            dripeffect.m_icon = AssetUtils.LoadSpriteFromFile("Assets/ShawesomeCapeimage/Sentry.png");
            dripeffect.m_startMessageType = MessageHud.MessageType.Center;
            dripeffect.m_startMessage = "The Gods Remark at your Impeccable Drip...";
            dripeffect.m_stopMessageType = MessageHud.MessageType.Center;
            dripeffect.m_stopMessage = "Your Drip Fades...";
            
            



            CustomStatusEffect MaximumDrip = new CustomStatusEffect(dripeffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
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