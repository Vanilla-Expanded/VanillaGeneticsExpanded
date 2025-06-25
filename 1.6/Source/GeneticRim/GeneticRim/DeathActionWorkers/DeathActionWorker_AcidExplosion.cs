
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace GeneticRim
{
    public class DeathActionWorker_AcidExplosion : DeathActionWorker
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
                radius = 2.9f;
            }
            else
            {
                radius = 3.9f;
            }



            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, InternalDefOf.GR_Acid, corpse.InnerPawn, 10, -1, InternalDefOf.GR_Pop, null, null, null, ThingDef.Named("Filth_Fuel"), 0.3f, 1,null, null, 255, false, null, 0f, 1);
        }


    }
}
