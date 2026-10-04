using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Cjayride.SocialSystemTweaks
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("M2Valheim.SocialSystem")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "cjayride.SocialSystemTweaks";
        public const string PluginName = "SocialSystem Tweaks";
        public const string PluginVersion = "1.0.0";

        internal static ConfigEntry<bool> InvitesDuringCombat;
        internal static ConfigEntry<bool> HealthTextEnabled;
        internal static ConfigEntry<bool> HealthTextShowMax;
        internal static ConfigEntry<int> HealthTextSize;
        internal static ConfigEntry<Color> HealthTextColor;
        internal static ConfigEntry<Color> HealthTextShadowColor;
        internal static ConfigEntry<float> HealthTextShadowThickness;

        private void Awake()
        {
            InvitesDuringCombat = Config.Bind("Invites", "ShowDuringCombat", true,
                "Show party and friend invite prompts right away, even while in combat. Off keeps SocialSystem's behaviour of holding them until combat ends.");

            HealthTextEnabled = Config.Bind("PartyHealthText", "Enabled", true,
                "Draw the HP number on party member health bars.");
            HealthTextShowMax = Config.Bind("PartyHealthText", "ShowMaxHealth", true,
                "Show current / max (for example 85 / 120) instead of only the current HP.");
            HealthTextSize = Config.Bind("PartyHealthText", "FontSize", 12,
                new ConfigDescription("HP text size.", new AcceptableValueRange<int>(6, 32)));
            HealthTextColor = Config.Bind("PartyHealthText", "Color", Color.white,
                "HP text color.");
            HealthTextShadowColor = Config.Bind("PartyHealthText", "ShadowColor", Color.black,
                "Border/shadow color around the HP text.");
            HealthTextShadowThickness = Config.Bind("PartyHealthText", "ShadowThickness", 1.5f,
                new ConfigDescription("Border/shadow thickness around the HP text. 0 removes it.", new AcceptableValueRange<float>(0f, 5f)));

            HealthTextSize.SettingChanged += (_, __) => HealthText.RestyleAll();
            HealthTextColor.SettingChanged += (_, __) => HealthText.RestyleAll();
            HealthTextShadowColor.SettingChanged += (_, __) => HealthText.RestyleAll();
            HealthTextShadowThickness.SettingChanged += (_, __) => HealthText.RestyleAll();
            HealthTextEnabled.SettingChanged += (_, __) => HealthText.RefreshVisibility();

            new Harmony(PluginGuid).PatchAll();
            Logger.LogInfo(PluginName + " " + PluginVersion + " loaded.");
        }
    }
}
