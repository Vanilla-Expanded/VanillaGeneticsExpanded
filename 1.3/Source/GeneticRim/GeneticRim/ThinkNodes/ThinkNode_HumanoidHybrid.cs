
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace GeneticRim
{
	public class ThinkNode_HumanoidHybrid : ThinkNode_Conditional
	{
		

		protected override bool Satisfied(Pawn pawn)
		{
			if (StaticCollectionsClass.IsHumanoidHybrid(pawn))
			{
				return true;
			}
			return false;
		}
	}
}
