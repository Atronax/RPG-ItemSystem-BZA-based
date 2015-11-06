using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public interface I_ISArmor
	{
		int CurrentArmor { get; set; }
		int MaximumArmor { get; set; }

	}
}
