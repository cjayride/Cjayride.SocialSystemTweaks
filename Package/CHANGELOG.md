# Changelog

## 1.0.4

- Hide party HP text when SocialSystem hides the health bar for a disconnected member (Update is not called during the reserved-slot timer).

## 1.0.3

- Hide party HP text when the member is offline/disconnected.
- Reparent HP labels when SocialSystem moves the health bar row (prevents orphaned text after disconnect).

## 1.0.2

- Verified against Valheim 1.0.21 (latest Steam patch as of 2026-11-05) and SocialSystem 1.0.5.
- Party HP text no longer rewrites UI layout every frame when the bar has not moved, and skips work when the feature is disabled.

## 1.0.1

- Verified against Valheim 1.0.21 (Release build).
- BepInEx dependency set to `denikson-BepInExPack_Valheim-5.4.2360`.

## 1.0.0

- Show party and friend invite prompts during combat (config: Invites / ShowDuringCombat).
- Draw current / max HP on party member health bars (config: PartyHealthText).
