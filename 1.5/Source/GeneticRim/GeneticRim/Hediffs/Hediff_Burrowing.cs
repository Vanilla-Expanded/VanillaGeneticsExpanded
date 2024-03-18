
using UnityEngine;
using RimWorld;
using Verse;


namespace GeneticRim
{
    public class Hediff_Burrowing : HediffWithComps
    {
        private bool firstTick = true;
        private bool lastTick = true;




        public override void Tick()
        {
            base.Tick();

            if (firstTick)
            {
                this.pawn.Drawer.renderer.SetAllGraphicsDirty();

                firstTick = false;
            }


            if ((this.Severity >= 1) && lastTick)
            {
                this.pawn.Drawer.renderer.SetAllGraphicsDirty();
                lastTick = false;
            }


        }


    }
}