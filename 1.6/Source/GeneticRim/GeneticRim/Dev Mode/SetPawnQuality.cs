using System;
using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using Verse;

namespace GeneticRim
{
    public static class SetPawnQuality
    {
        [DebugAction("Vanilla Genetics Expanded", "Set pawn quality", allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void SetQuality()
        {
            var options = new List<DebugMenuOption>();

            foreach (QualityCategory quality in Enum.GetValues(typeof(QualityCategory)))
            {
                options.Add(new DebugMenuOption(quality.ToString(), DebugMenuOptionMode.Tool, () =>
                {
                    foreach (var pawn in UI.MouseCell().GetThingList(Find.CurrentMap).OfType<Pawn>().ToList())
                    {
                        var comp = pawn.GetComp<CompHybrid>();
                        if (comp != null)
                            comp.quality = quality;
                    }
                }));
            }

            Find.WindowStack.Add(new Dialog_DebugOptionListLister(options));
        }
    }
}