/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Represents item quality type, which is described by its name and background icon.
/// </summary>

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISQuality : ISDatabaseObject, I_ISQuality
	{
		#region public
		/// <summary>
		/// Default contructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISQuality"/> class using default values.
		/// </summary>
		public ISQuality ()
		{

		}

		/// <summary>
		/// Prototyping constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISQuality"/> class using values of the other instance of that class.
		/// </summary>
		/// <param name="RHS">Instance, used as a prototype.</param>
		public ISQuality (ISQuality RHS)
		{
			base.Clone (RHS);
		}

		#if UNITY_EDITOR
		/// <summary>
		/// Displays the details editor.
		/// </summary>
		public override void DisplayDetailsEditor ()
		{
			base.DisplayDetailsEditor ();
		}
		#endif
		#endregion
	}
}