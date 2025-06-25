﻿namespace GeneticRim
{
    using System.Collections.Generic;
    using System;
    using System.Text;
    using Verse;
    using Verse.AI;

    public class CompGrowthCell : ThingComp
    {
        public ThingDef genomeDominant;
        public ThingDef genomeSecondary;
        public ThingDef genoframe;
        public ThingDef booster;
        public string mainResult;
        public float bodySize;

        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn selPawn)
        {

           
            List<Building> list = this?.parent?.Map?.listerBuildings?.allBuildingsColonist;
            if (list != null) {
                foreach (Building building in list)
                {
                    if (building?.TryGetComp<CompElectroWomb>()?.Free ?? false)
                    {
                        if(selPawn!=null && mainResult!=null) {
                            yield return new FloatMenuOption("GR_GrowthCell_InsertInElectroWomb".Translate(building.LabelCap, mainResult, bodySize.ToString()), () =>
                            {

                                if (selPawn.CanReserveAndReach(building, PathEndMode.OnCell, Danger.Deadly) &&
                                selPawn.CanReserveAndReach(this.parent, PathEndMode.OnCell, Danger.Deadly))
                                {
                                    Job makeJob = JobMaker.MakeJob(InternalDefOf.GR_InsertGrowthCell, building,
                                       this.parent);
                                    makeJob.haulMode = HaulMode.ToCellNonStorage;
                                    makeJob.count = 1;
                                    selPawn.jobs?.TryTakeOrderedJob(makeJob);


                                }

                            });
                        }
                        else
                        {
                            yield return new FloatMenuOption("GR_GrowthCell_InsertInElectroWomb".Translate(building.LabelCap), () =>
                            {
                                if (selPawn.CanReserveAndReach(building, PathEndMode.OnCell, Danger.Deadly) &&
                                 selPawn.CanReserveAndReach(this.parent, PathEndMode.OnCell, Danger.Deadly))
                                {
                                    Job makeJob = JobMaker.MakeJob(InternalDefOf.GR_InsertGrowthCell, building,
                                       this.parent);
                                    makeJob.haulMode = HaulMode.ToCellNonStorage;
                                    makeJob.count = 1;
                                    selPawn.jobs?.TryTakeOrderedJob(makeJob);


                                }
                            });

                        }
                        
                    }
                }
            }

        }

        public override string CompInspectStringExtra()
        {
            StringBuilder sb = new StringBuilder(base.CompInspectStringExtra());

            sb.AppendLine("GR_GrowthCell_InspectDominant".Translate(this.genomeDominant.LabelCap));
            sb.AppendLine("GR_GrowthCell_InspectSecondary".Translate(this.genomeSecondary.LabelCap));
            sb.AppendLine("GR_GrowthCell_InspectGenoframe".Translate(this.genoframe.LabelCap));
            if(this.booster != null)
                sb.AppendLine("GR_GrowthCell_InspectBooster".Translate(this.booster.LabelCap));

            return sb.ToString().Trim();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Defs.Look(ref this.genomeDominant, nameof(this.genomeDominant));
            Scribe_Defs.Look(ref this.genomeSecondary, nameof(this.genomeSecondary));
            Scribe_Defs.Look(ref this.genoframe, nameof(this.genoframe));
            Scribe_Defs.Look(ref this.booster, nameof(this.booster));
            Scribe_Values.Look(ref this.mainResult, nameof(this.mainResult));
            Scribe_Values.Look(ref this.bodySize, nameof(this.bodySize));

        }
    }
}
