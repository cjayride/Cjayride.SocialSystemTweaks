# PROGRESS — SocialSystem Tweaks 1.0.1

## Goal
Verify Release build against Valheim 1.0.21, update BepInExPack to 5.4.2360, changelog, Thunderstore `/dist` zip, git tag `v1.0.1`.

## Status
Completed (Release build succeeded).

## Files changed
- `Package/manifest.json` — version 1.0.1, BepInEx 5.4.2360
- `CHANGELOG.md`, `Package/CHANGELOG.md` — 1.0.1 entry
- `Cjayride.SocialSystemTweaks.csproj`, `src/Plugin.cs` — version 1.0.1
- `Environment.props.example` — SocialSystem 1.0.5 Gale cache path
- `.gitignore` — dist zip/icon
- Local `Environment.props` — SocialSystem 1.0.5 path (gitignored)

## Build
```
dotnet build -c Release
```
Succeeded, 0 warnings.

## Output
- DLL: `D:\AI-Agent-Projects\cjaycraft\Cjayride.SocialSystemTweaks\bin\Release\net481\Cjayride.SocialSystemTweaks.dll`
- Zip: `D:\AI-Agent-Projects\cjaycraft\Cjayride.SocialSystemTweaks\dist\cjayride-SocialSystemTweaks-1.0.1.zip`
