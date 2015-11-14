/// <summary>
/// November 14, 2015
/// Author: Zamana Max
/// 
/// Represents basic database object interface.
/// All objects in our item system must have name and icon.
/// Besides, all of them must implement function, that displays its values to editor, so that user can edit them.
/// </summary>

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public interface I_ISDatabaseObject
	{
		string Name {get; set;}
		Sprite Icon {get; set;}

		void DisplayDetailsEditor ();
	}
}
