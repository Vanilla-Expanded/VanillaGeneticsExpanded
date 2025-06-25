﻿using RimWorld;
using Verse;
namespace GeneticRim
{
	public class HediffCompProperties_GiveHediffsInRangeExpanded : HediffCompProperties
	{
		public float range;

		public TargetingParameters targetingParameters;

		public HediffDef hediff;

		public ThingDef mote;

		public bool hideMoteWhenNotDrafted;

		public float initialSeverity = 1f;

		public bool affectSameDef;

		public HediffCompProperties_GiveHediffsInRangeExpanded()
		{
			compClass = typeof(HediffComp_GiveHediffsInRangeExpanded);
		}
	}
}
