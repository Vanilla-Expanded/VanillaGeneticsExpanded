
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace GeneticRim
{
    class CompFleshFlies : ThingComp
    {


        public CompProperties_FleshFlies Props
        {
            get
            {
                return (CompProperties_FleshFlies)this.props;
            }
        }


        public override void CompTickInterval(int delta)
        {
            base.CompTickInterval(delta);

            if (this.parent.IsHashIntervalTick(20, delta))
            {

                Pawn pawn = this.parent as Pawn;
                if (pawn.Downed)
                {
                  
                    pawn.Kill(null);

                }


            }
            

        }

    }

}

