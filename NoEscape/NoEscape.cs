using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoEscape
{
    public class NoEscape : NeosMod
    {
        public override string Name => "NoEscape";
        public override string Author => "runtime";
        public override string Version => "1.0.1";
        public override string Link => "https://github.com/zkxs/NoEscape";

        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("net.michaelripley.NoEscape");
            MethodInfo originalMethod = AccessTools.DeclaredMethod(typeof(CommonTool), "HoldMenu", new Type[] {});
            if (originalMethod == null)
            {
                Error("Could not find CommonTool.HoldMenu()");
                return;
            }
            MethodInfo replacementMethod = AccessTools.DeclaredMethod(typeof(NoEscape), nameof(HoldMenuPrefix));
            harmony.Patch(originalMethod, prefix: new HarmonyMethod(replacementMethod));
            Msg("Hook installed successfully");
        }

        public static void HoldMenuPrefix(ref float ___panicCharge)
        {
            ___panicCharge = 0;
        }
    }
}
