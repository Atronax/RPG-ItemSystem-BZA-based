using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public interface I_ISWeapon
	{
		int MinDamage { get; set; }
		int MaxDamage { get; set; }

		int Attack ();
	}
}