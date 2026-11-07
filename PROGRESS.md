# PROGRESS — SocialSystem Tweaks 1.0.4

## Goal
HP numbers must disappear from the party plate when a member disconnects, for the whole reserved-slot timer.

## Status
Completed — Release build succeeded; dist zip updated.

## Root cause
`PartyMemberView.Bind` calls `_health.SetVisible(false)` when `member.IsConnected` becomes false, then `Tick` **does not call** `HealthBar.Update` while showing the disconnected timer. Our HP overlay is a sibling of the bar, so hiding `_root` does not hide the number, and the Update postfix never runs.

## Fix
Postfix `HealthBar.SetVisible` and deactivate the HP label when the bar is hidden.

## Files changed
- `src/HealthText.cs` — `AfterSetVisible` postfix.
- Version 1.0.4 in plugin, csproj, Package/dist manifests and changelogs.

## Build
```
dotnet build -c Release
```
Succeeded, 0 warnings.

DLL: `D:\AI-Agent-Projects\cjaycraft\Cjayride.SocialSystemTweaks\bin\Release\net481\Cjayride.SocialSystemTweaks.dll`
Zip: `D:\AI-Agent-Projects\cjaycraft\Cjayride.SocialSystemTweaks\dist\cjayride-SocialSystemTweaks-1.0.4.zip`

## Next steps
1. In-game: disconnect a party member — HP text should vanish with the bar while the disconnected timer is shown.
