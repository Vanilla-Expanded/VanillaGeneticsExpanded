
using GeneticRim;
using RimWorld;
using UnityEngine;
using Verse;

namespace GeneticRim
{
    public class PawnRenderNode_Burrower : PawnRenderNode_AnimalPart
    {
        public PawnRenderNode_Burrower(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            if (pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.GR_Burrowing) != null)
            {
                Graphic graphic = pawn.ageTracker.CurKindLifeStage.bodyGraphicData.Graphic;
                return GraphicDatabase.Get<Graphic_Multi>("Things/Pawn/Animal/Special/GR_Special_Burrowing", ShaderDatabase.Cutout, graphic.drawSize, Color.white);
            }

            return base.GraphicFor(pawn);
        }
    }
}
