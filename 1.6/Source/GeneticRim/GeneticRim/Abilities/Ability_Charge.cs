using System;
using RimWorld;
using Verse;
using VEF.Abilities;

namespace GeneticRim
{

    public class Ability_Charge : VEF.Abilities.Ability
    {
        public override void Cast(LocalTargetInfo target)
        {
            base.Cast(target);

            LongEventHandler.QueueLongEvent(() =>
            {
                IntVec3 destination = target.Cell + ((this.pawn.Position - target.Cell).ToVector3().normalized * 2).ToIntVec3();
                Map map = this.pawn.Map;

                AbilityPawnFlyer flyer = (AbilityPawnFlyer)PawnFlyer.MakeFlyer(InternalDefOf.GR_StraightFlyer, this.pawn, destination,null,null);
                flyer.ability = this;
                flyer.DestinationCell = destination;
                GenSpawn.Spawn(flyer, target.Cell, map);
                target.Thing.TakeDamage(new DamageInfo(DamageDefOf.Cut, this.GetPowerForPawn(), float.MaxValue, instigator: this.pawn));

            }, "chargeAbility", false, null);
        }
    }
}