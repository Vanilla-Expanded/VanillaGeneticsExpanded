﻿using RimWorld;
using Verse;
using Verse.AI.Group;
namespace GeneticRim
{
    public class DeathActionWorker_SmallBomb : DeathActionWorker
    {



        public override void PawnDied(Corpse corpse, Lord lord)
        {
            float radius;
            if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0)
            {
                radius = 1.9f;
            }
            else if (corpse.InnerPawn.ageTracker.CurLifeStageIndex == 1)
            {
                radius = 1.9f;
            }
            else
            {
                radius = 1.9f;
            }
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, DamageDefOf.Bomb, corpse.InnerPawn, -1, -1, null, null, null, null, ThingDef.Named("Filth_Blood"), .7f, 1, null, null, 255, false, null, 0f, 1);
        }


    }
}
