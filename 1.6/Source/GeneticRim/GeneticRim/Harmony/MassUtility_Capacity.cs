﻿using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System.Reflection.Emit;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;
using RimWorld.Planet;



namespace GeneticRim
{


    /*This Harmony Postfix makes the creature carry more weight*/

    [HarmonyPatch(typeof(MassUtility))]
    [HarmonyPatch("Capacity")]
    public static class GeneticRim_MassUtility_Capacity_Patch
    {
        [HarmonyPostfix]
        public static void MakeHybridsCarryMore(Pawn p, ref float __result)

        {
            if (StaticCollectionsClass.manffalo_and_experience.ContainsKey(p)) {

                __result = (p.BodySize * MassUtility.MassCapacityPerBodySize) * StaticCollectionsClass.manffalo_and_experience[p];


            } else
            {
                bool flagIsCreatureMine = p.Faction != null && p.Faction.IsPlayer;
                //bool flagIsCreatureDraftable = VEF.AnimalBehaviours.AnimalCollectionClass.draftable_animals.Contains(p);
                bool flagCanCreatureCarryMore = false;
                if (flagIsCreatureMine)
                {
                    flagCanCreatureCarryMore = (p.kindDef?.GetModExtension<DefExtension_Hybrid>()?.carryingIncrease) ?? false;
                }

                if (flagCanCreatureCarryMore)
                {
                    float factor = p.kindDef?.GetModExtension<DefExtension_Hybrid>()?.carryingFactor ?? 1f;
                    __result = (p.BodySize * MassUtility.MassCapacityPerBodySize) * 1.5f;
                }
            }

            

        }
    }













}

