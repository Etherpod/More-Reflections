﻿using HarmonyLib;
using modweaver.core;
using NLog;

namespace modweaver_template
{
    public class ModMain : Mod
    {
        public override void Init()
        {
            // your manifest is the mw.mod.toml file
            // use Metadata to access the values you provided in the manifest. Manifest is also available, and provides the other data such as your dependencies and incompats
            Logger.Info("Loading {0} v{1} by {2}!", Metadata.title, Metadata.version,
                string.Join(", ", Metadata.authors));
            
            // this section currently patches any [HarmonyPatch]s you use, like the one named NoMoreLaserCubes below. if you don't patch anything, you can remove these
            // you should keep the logging messages as they help users and developers with debugging
            Logger.Debug("Setting up patcher...");
            Harmony harmony = new Harmony(Metadata.id); 
            Logger.Debug("Patching...");
            harmony.PatchAll();
        }

        public override void Ready()
        {
            Logger.Info("Loaded {0}!", Metadata.title);
        }

        public override void OnGUI(ModsMenuPopup ui)
        {
            // you can add data to your mods page here
            // we recommend if you are going to add ui here, put
            // ui.CreateDivider() first
            // you'll see why :3
        }
    }
    
    // no more laser cubes! this stops laser cubes from firing their beams. you can do whatever you want this this.
    [HarmonyPatch(typeof(LaserCube), nameof(LaserCube.ActivateBeams))]
    public static class NoMoreLaserCubes
    {
        public static bool Prefix()
        {
            return false; // this stops the original code from running!
        }
    }
}