﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticRim
{
    using Verse;
    using Verse.AI;

    public class JobDriver_InsertGrowthCell : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed) =>
            this.pawn.Reserve(this.job.GetTarget(TargetIndex.A), this.job, 1, -1, null, false) &&
            this.pawn.Reserve(this.job.GetTarget(TargetIndex.B), this.job, 1, -1, null, false);

        protected override IEnumerable<Toil> MakeNewToils()
        {
            CompElectroWomb comp = job.GetTarget(TargetIndex.A).Thing.TryGetComp<CompElectroWomb>();
            yield return Toils_Reserve.Reserve(TargetIndex.B, 1, 1);
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.B).FailOnSomeonePhysicallyInteracting(TargetIndex.B);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, true, false).FailOnDestroyedNullOrForbidden(TargetIndex.B);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(200, TargetIndex.None).FailOnDestroyedNullOrForbidden(TargetIndex.B).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            yield return Toils_General.DoAtomic(delegate
                                                {
                                                    comp.InitProcess(this.GetActor().carryTracker.CarriedThing);
                                                });
        }
    }
}
