using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public interface I_ISStackable
	{
		int MaxStack { get; }

		int PlaceToStack (int Amount);
	}
}
