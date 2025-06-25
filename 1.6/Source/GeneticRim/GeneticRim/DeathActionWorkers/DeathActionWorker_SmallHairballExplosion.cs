﻿
using RimWorld;
using Verse;
using Verse.AI.Group;
namespace GeneticRim
{
    class DeathActionWorker_SmallHairballExplosion : DeathActionWorker
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



            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, Named("GR_HairballExplosion"), corpse.InnerPawn, 1, -1, SoundDef.Named("Explosion_Bomb"), null, null, null, ThingDef.Named("GR_Hairballs"), 1f, 1, null, null, 255, false, null, 0f, 1);
        }

        public static DamageDef Named(string defName)
        {
            return DefDatabase<DamageDef>.GetNamed(defName, true);
        }


    }
}