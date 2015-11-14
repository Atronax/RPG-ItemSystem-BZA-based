/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Partial class of ISObjectDatabaseEditor.
/// Represents editor window BottomBar functionals.
/// </summary>


using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor
	{
		#region private
		/// <summary>
		/// Displays the bottom bar.
		/// </summary>
		private void DisplayBottomBar ()
		{
			GUILayout.BeginHorizontal ("Box", GUILayout.ExpandWidth (true));
				GUILayout.Label ("I'm a bottom bar!");
			GUILayout.EndHorizontal ();
		}
		#endregion
	}
}
