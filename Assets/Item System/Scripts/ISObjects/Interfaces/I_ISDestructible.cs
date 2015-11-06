using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public interface I_ISDestructible
	{
		int CurrentDurability { get; }
		int MaximumDurability { get; }

		void BreakBy (int PT);
		void ApplyBrokenEffect ();

		void RepairBy (int PT);
		void ApplyRepairedEffect ();
	}
}
