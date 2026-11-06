# PROGRESS — SocialSystem Tweaks 1.0.3

## Goal
Hide party HP overlay when a member disconnects, and avoid orphaned HP text when SocialSystem reparents party rows.

## Status
Completed — Release build succeeded; `dist` zip updated.

## Acceptance criteria
- [x] HP number hidden when party member is offline (not only when bar root inactive).
- [x] Label stays parented to the same row as `_root` so it does not float after disconnect/recycle.

## Files changed
- `src/HealthText.cs` — `EnsureLabelParent` syncs label parent to `bar._root.transform.parent` each update; `show` requires `client.IsOnline(playerId)`.
- `src/Plugin.cs`, `Cjayride.SocialSystemTweaks.csproj` — version 1.0.3.
- `Package/manifest.json`, `Package/CHANGELOG.md`, `Package/README.md`, root `CHANGELOG.md`, root `README.md`, `dist/manifest.json`, `dist/CHANGELOG.md`, `dist/README.md` — version / docs.
- `Package/Cjayride.SocialSystemTweaks.dll`, `dist/Cjayride.SocialSystemTweaks.dll`, `dist/cjayride-SocialSystemTweaks-1.0.3.zip` — build outputs.

## Commands run
```
dotnet build -c Release
```
Result: **succeeded**, 0 warnings, 0 errors.

DLL build output:
`D:\AI-Agent-Projects\cjaycraft\Cjayride.SocialSystemTweaks\bin\Release\net481\Cjayride.SocialSystemTweaks.dll`

Copied to `Package/` and `dist/`; created:
`D:\AI-Agent-Projects\cjaycraft\Cjayride.SocialSystemTweaks\dist\cjayride-SocialSystemTweaks-1.0.3.zip`

## Decisions
- Use `SocialClient.IsOnline(playerId)` so we hide HP for disconnected members even if last vitals are still cached.
- Reparent when `label.Text.transform.parent` differs from `bar._root.transform.parent` (fixes orphaned UI when rows are recycled).

## Next steps
1. In-game: party with another player, disconnect other client — HP text should vanish with the bar / disconnected state.
2. Publish Thunderstore zip when ready.
