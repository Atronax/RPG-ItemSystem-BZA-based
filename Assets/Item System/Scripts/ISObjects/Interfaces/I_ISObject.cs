using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public interface I_ISObject
	{
		string Name { get; set; } 
		Sprite Icon { get; set; }
		ISQuality Quality { get; set; } 
		string Description { get; set; }
		int Price { get; set; }
		int Weight { get; set; }

		// these go to other item interfaces
		// Quest item flag
	}
}
