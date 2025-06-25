﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace GeneticRim
{
    

    [StaticConstructorOnStartup]
    public class CompGenomorpher : ThingComp
    {
        public  float progress = -1;
        public  int   duration;
        public Thing growthCell;
        public  bool  BringIngredients => this.genomeDominant != null && this.progress < 0;

        public Thing genomeDominant;
        public Thing genomeSecondary;
        public Thing frame;
        public Thing booster;
        public PawnKindDef mainResult;

        public string genomeDominantLabel;
        public string genomeSecondaryLabel;
        public string frameLabel;
        public string boosterLabel;

        public CompPowerTrader compPowerTrader;

        public CompProperties_Genomorpher Props => (CompProperties_Genomorpher)this.props;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.compPowerTrader = this.parent.GetComp<CompPowerTrader>();
        }
       
        public void Initialize(Thing genomeDominant, Thing genomeSecondary, Thing frame, Thing booster, int durationTicks, PawnKindDef mainResult)
        {
            this.genomeDominant = genomeDominant;
            this.genomeSecondary = genomeSecondary;
            this.frame = frame;
            this.booster = booster;
            this.mainResult = mainResult;

            genomeDominantLabel = genomeDominant.def.LabelCap;
            genomeSecondaryLabel = genomeSecondary.def.LabelCap;
            frameLabel = frame.def.LabelCap;
            boosterLabel = booster?.def.LabelCap ?? "";

            this.duration    = durationTicks;
        }

        public void StartGrowthProcess()
        {
            this.growthCell = ThingMaker.MakeThing(InternalDefOf.GR_GrowthCell);
            CompGrowthCell cell = this.growthCell?.TryGetComp<CompGrowthCell>();
            if (cell != null) {
                cell.genomeDominant = this.genomeDominant?.def;
                cell.genomeSecondary = this.genomeSecondary?.def;
                cell.genoframe = this.frame?.def;
                cell.booster = this.booster?.def;
                cell.mainResult = this.mainResult?.LabelCap ?? "";
                cell.bodySize = this.mainResult?.RaceProps?.baseBodySize ?? 1;



                this.genomeDominant = null;
                this.genomeSecondary = null;
                this.frame = null;
                this.booster = null;
                this.mainResult = null;

                this.progress = 0f;
            }
          
        }

        public override void CompTick()
        {
            base.CompTick();

            if (this.progress >= 0)
            {
                if (compPowerTrader?.PowerOn == true) {
                    this.progress += 1f / this.duration;

                    if (this.progress >= 1)
                    {
                        GenSpawn.Spawn(this.growthCell, this.parent.InteractionCell, this.parent.Map);
                        this.duration = -1;
                        this.progress = -1f;
                        this.growthCell = null;
                        if (!GeneticRim_Mod.settings.GR_DisableGrowthCellAlerts)
                        {
                            Messages.Message("GR_Genomorpher_Finished".Translate(), this.parent,MessageTypeDefOf.PositiveEvent, true);
                        }
                    }

                }
                
            }
        }

        public override string CompInspectStringExtra()
        {
            StringBuilder sb = new StringBuilder(base.CompInspectStringExtra());

            if (this.progress != -1f) { 
                sb.AppendLine("GR_Genomorpher_Progress".Translate(this.progress.ToStringPercent()));
                if (boosterLabel != "") {
                    sb.AppendLine("GR_Genomorpher_Ingredients".Translate(genomeDominantLabel,genomeSecondaryLabel,frameLabel,boosterLabel));
                } else
                {
                    sb.AppendLine("GR_Genomorpher_Ingredients_NoFrame".Translate(genomeDominantLabel, genomeSecondaryLabel, frameLabel));
                }
            }
            

            return sb.ToString().Trim();
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if(this.progress== -1f) {
                yield return new Command_Action
                {
                    defaultLabel = "GR_DesignateGrowthCell".Translate(),
                    defaultDesc = "GR_DesignateGrowthCellDesc".Translate(),
                    icon = ContentFinder<Texture2D>.Get("Things/Item/GR_Growthcell", true),

                    action = delegate
                    {
                        Find.WindowStack.Add(new Window_DesignateGrowthCell(this));
                    }
                };

            }
            
            if (DebugSettings.ShowDevGizmos)
            {
                Command_Action command_Action = new Command_Action();
                command_Action.defaultLabel = "DEBUG: Finish growth cell";
                command_Action.action = delegate
                {
                    if (this.progress >= 0)
                    { this.progress = 1; }
                        
                };
                yield return command_Action;

                Command_Action command_Action2 = new Command_Action();
                command_Action2.defaultLabel = "DEBUG: Reset All";
                command_Action2.action = delegate
                {
                    duration = -1;
                    progress = -1f;
                    growthCell = null;
                    genomeDominant = null;
                    genomeSecondary = null;
                    frame = null;
                    booster = null;

                };
                yield return command_Action2;

            }
        }

        private static readonly Material barFilledMat   = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f,  0.475f, 0.1f));
        private static readonly Material barUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f,  0.15f));

        public override void PostDraw()
        {
            base.PostDraw();
            if(this.parent.Rotation==Rot4.North|| this.parent.Rotation == Rot4.South) {
                GenDraw.FillableBarRequest fillableBarRequest = default(GenDraw.FillableBarRequest);
                fillableBarRequest.center = this.parent.DrawPos + Vector3.forward * 0.1f + Vector3.left * 1.49f;
                fillableBarRequest.size = new Vector2(1.6f, 0.2f);
                fillableBarRequest.fillPercent = this.progress;
                fillableBarRequest.filledMat = barFilledMat;
                fillableBarRequest.unfilledMat = barUnfilledMat;
                fillableBarRequest.margin = 0.15f;
                fillableBarRequest.rotation = this.parent.Rotation.Rotated(RotationDirection.Clockwise);
                GenDraw.DrawFillableBar(fillableBarRequest);
                fillableBarRequest.center = this.parent.DrawPos + Vector3.forward * 0.1f + Vector3.right * 1.49f;
                GenDraw.DrawFillableBar(fillableBarRequest);
            }
            else
            {
                GenDraw.FillableBarRequest fillableBarRequest = default(GenDraw.FillableBarRequest);
                fillableBarRequest.center = this.parent.DrawPos + Vector3.forward * -1.36f;
                fillableBarRequest.size = new Vector2(1.6f, 0.2f);
                fillableBarRequest.fillPercent = this.progress;
                fillableBarRequest.filledMat = barFilledMat;
                fillableBarRequest.unfilledMat = barUnfilledMat;
                fillableBarRequest.margin = 0.15f;
                fillableBarRequest.rotation = this.parent.Rotation.Rotated(RotationDirection.Clockwise);
                GenDraw.DrawFillableBar(fillableBarRequest);
                
            }
            
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref this.progress, nameof(this.progress), -1f);
            Scribe_Values.Look(ref this.duration, nameof(this.duration));

            Scribe_Deep.Look(ref this.growthCell, nameof(this.growthCell));

            Scribe_References.Look(ref this.genomeDominant, nameof(this.genomeDominant));
            Scribe_References.Look(ref this.genomeSecondary, nameof(this.genomeSecondary));
            Scribe_References.Look(ref this.frame, nameof(this.frame));
            Scribe_References.Look(ref this.booster, nameof(this.booster));
            Scribe_Deep.Look(ref this.mainResult, nameof(this.mainResult));


            Scribe_Values.Look(ref this.genomeDominantLabel, nameof(this.genomeDominantLabel));
            Scribe_Values.Look(ref this.genomeSecondaryLabel, nameof(this.genomeSecondaryLabel));
            Scribe_Values.Look(ref this.frameLabel, nameof(this.frameLabel));
            Scribe_Values.Look(ref this.boosterLabel, nameof(this.boosterLabel));
        }

       
    }
}
