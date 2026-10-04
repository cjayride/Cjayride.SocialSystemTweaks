using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using SocialSystem.Client;
using SocialSystem.Core;
using SocialSystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Cjayride.SocialSystemTweaks
{
    [HarmonyPatch(typeof(HealthBar), nameof(HealthBar.Update))]
    [HarmonyPriority(Priority.Last)]
    internal static class HealthText
    {
        sealed class Label
        {
            public Text Text;
            public Outline Outline;
            public int Shown = int.MinValue;
            public int ShownMax = int.MinValue;
            public bool ShowMax;
        }

        static readonly ConditionalWeakTable<HealthBar, Label> Labels = new ConditionalWeakTable<HealthBar, Label>();
        static readonly List<Label> All = new List<Label>();

        static void Postfix(HealthBar __instance, long playerId, SocialClient client)
        {
            if (__instance?._root == null)
                return;

            Label label = Labels.GetValue(__instance, Create);
            if (label.Text == null)
                return;

            MemberVitals current = default;
            bool show = Plugin.HealthTextEnabled.Value && __instance._root.activeInHierarchy
                && client != null && client.TryGetVitals(playerId, out current) && current.MaxHealth > 0f;

            GameObject go = label.Text.gameObject;
            if (!show)
            {
                if (go.activeSelf)
                    go.SetActive(false);
                return;
            }

            Follow(label.Text.rectTransform, (RectTransform)__instance._root.transform);
            if (!go.activeSelf)
                go.SetActive(true);

            int hp = current.IsDead ? 0 : Mathf.CeilToInt(current.Health);
            int max = Mathf.CeilToInt(current.MaxHealth);
            if (hp != label.Shown || max != label.ShownMax || label.ShowMax != Plugin.HealthTextShowMax.Value)
            {
                label.Shown = hp;
                label.ShownMax = max;
                label.ShowMax = Plugin.HealthTextShowMax.Value;
                label.Text.text = label.ShowMax ? hp + " / " + max : hp.ToString();
            }
        }

        static Label Create(HealthBar bar)
        {
            var label = new Label();
            Transform parent = bar._root.transform.parent;
            if (parent == null)
                return label;

            var go = new GameObject("CjayrideHealthText", typeof(RectTransform));
            go.transform.SetParent(parent, false);
            go.transform.SetAsLastSibling();

            label.Text = go.AddComponent<Text>();
            label.Text.font = SocialUiFactory.Font;
            label.Text.alignment = TextAnchor.MiddleCenter;
            label.Text.horizontalOverflow = HorizontalWrapMode.Overflow;
            label.Text.verticalOverflow = VerticalWrapMode.Overflow;
            label.Text.raycastTarget = false;
            label.Outline = go.AddComponent<Outline>();

            Style(label);
            All.Add(label);
            return label;
        }

        static void Follow(RectTransform text, RectTransform bar)
        {
            text.anchorMin = bar.anchorMin;
            text.anchorMax = bar.anchorMax;
            text.pivot = bar.pivot;
            text.anchoredPosition = bar.anchoredPosition;
            Vector3 scale = bar.localScale;
            Rect rect = bar.rect;
            text.sizeDelta = new Vector2(rect.width * scale.x, rect.height * scale.y);
        }

        static void Style(Label label)
        {
            label.Text.fontSize = Plugin.HealthTextSize.Value;
            label.Text.color = Plugin.HealthTextColor.Value;
            float thickness = Plugin.HealthTextShadowThickness.Value;
            label.Outline.enabled = thickness > 0f;
            label.Outline.effectColor = Plugin.HealthTextShadowColor.Value;
            label.Outline.effectDistance = new Vector2(thickness, -thickness);
        }

        internal static void RestyleAll()
        {
            All.RemoveAll(l => l.Text == null);
            foreach (Label label in All)
                Style(label);
        }

        internal static void RefreshVisibility()
        {
            All.RemoveAll(l => l.Text == null);
            if (Plugin.HealthTextEnabled.Value)
                return;
            foreach (Label label in All)
                label.Text.gameObject.SetActive(false);
        }
    }
}
