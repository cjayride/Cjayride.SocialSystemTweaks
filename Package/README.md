# SocialSystem Tweaks

Patch for [M2Valheim SocialSystem](https://thunderstore.io/c/valheim/p/M2Valheim/SocialSystem/). Load it with SocialSystem on the **client**.

## Features

- **Invites during combat** — party and friend invite prompts show right away, even while fighting. SocialSystem normally holds them until combat ends.
- **Party HP text** — current / max health on each party member's HUD bar (for example `85 / 120`).

## Config (`cjayride.SocialSystemTweaks.cfg`)

| Section | Key | Default | What it does |
| --- | --- | --- | --- |
| Invites | ShowDuringCombat | true | Show invite prompts in combat. Off keeps SocialSystem's hold-until-safe behaviour. |
| PartyHealthText | Enabled | true | Draw the HP number on party bars. |
| PartyHealthText | ShowMaxHealth | true | `current / max` instead of current only. |
| PartyHealthText | FontSize | 12 | Size of the HP text (6–32). |
| PartyHealthText | Color | white | HP text color. |
| PartyHealthText | ShadowColor | black | Outline color. |
| PartyHealthText | ShadowThickness | 1.5 | Outline thickness. 0 turns the outline off. |

Configuration Manager can change these live.

## Solo test (no second player)

In a **local or hosted** world, with `devcommands`:

```
socialsystem_fake add TestFriend
socialsystem_fake invite TestFriend
```

Accept the invite. The party HUD appears on the left. Change dummy HP with `socialsystem_fake vitals TestFriend 42`.

## Changelog

### 1.0.0

- Show party and friend invite prompts during combat.
- Draw current / max HP on party member health bars.
