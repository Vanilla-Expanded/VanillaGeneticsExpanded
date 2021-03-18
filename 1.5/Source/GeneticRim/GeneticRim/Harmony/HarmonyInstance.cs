﻿using HarmonyLib;

using System.Reflection;
using Verse;


namespace GeneticRim
{
    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.vanillageneticrimexpanded");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }
}
