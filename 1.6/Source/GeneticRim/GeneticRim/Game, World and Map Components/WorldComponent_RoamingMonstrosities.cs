using RimWorld;
using RimWorld.Planet;
using Verse;

namespace GeneticRim
{



    public class WorldComponent_RoamingMonstrosities : WorldComponent
    {
        public int tickCounter;
        public int ticksToNextAssault = 60000 * 30; // First raid at 30 days
        public static IntRange incidentDays = new IntRange(30,40); // Every 35 days average


        public WorldComponent_RoamingMonstrosities(World world) : base(world)
        {
        }

      

        public override void WorldComponentTick()
        {
            base.WorldComponentTick();


            if (!GeneticRim_Mod.settings.GR_DisableHybridRaids)
            {
                if (Current.Game.storyteller.difficultyDef != InternalDefOf.Peaceful)
                {


                    if (tickCounter > ticksToNextAssault)
                    {
                        if (Find.FactionManager.FirstFactionOfDef(InternalDefOf.GR_RoamingMonstrosities) != null)
                        {
                            IncidentParms parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, this.world);
                            Map map = Find.AnyPlayerHomeMap;
                            if (map != null && map.Biome.defName!= "OuterSpaceBiome" && !map.Biome.inVacuum) {
                                parms.target = map;

                                IncidentDef def = InternalDefOf.GR_ManhunterMonstrosities;
                                def.Worker.TryExecute(parms);

                                ticksToNextAssault = (int)(60000 * incidentDays.RandomInRange * GeneticRim_Mod.settings.GR_RaidsRate);
                                tickCounter = 0;
                            }
                            
                        }



                    }
                    tickCounter++;
                }
            }
            



            
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.tickCounter, nameof(this.tickCounter));
            Scribe_Values.Look(ref this.ticksToNextAssault, nameof(this.ticksToNextAssault));
        }
    }
}
