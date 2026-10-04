using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using SocialSystem.Client;
using SocialSystem.UI;

namespace Cjayride.SocialSystemTweaks
{
    [HarmonyPatch]
    [HarmonyPriority(Priority.Last)]
    internal static class CombatInvites
    {
        static readonly MethodInfo IsInCombat = AccessTools.PropertyGetter(typeof(CombatState), nameof(CombatState.IsInCombat));
        static readonly MethodInfo Replacement = AccessTools.Method(typeof(CombatInvites), nameof(BlocksPrompts));

        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(SocialPrompts), nameof(SocialPrompts.Enqueue));
            yield return AccessTools.Method(typeof(SocialPrompts), nameof(SocialPrompts.Tick));
            yield return AccessTools.Method(typeof(SocialPrompts), "ShowNextIfPossible");
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(IsInCombat))
                    instruction.operand = Replacement;
                yield return instruction;
            }
        }

        static bool BlocksPrompts()
        {
            if (Plugin.InvitesDuringCombat.Value)
                return false;
            return CombatState.IsInCombat;
        }
    }
}
