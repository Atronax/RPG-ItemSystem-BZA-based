using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor
	{
		#region public
		#endregion

		#region private
		private void DisplayBottomBar ()
		{
			GUILayout.BeginHorizontal ("Box", GUILayout.ExpandWidth (true));
				GUILayout.Label ("I'm a bottom bar!");
			GUILayout.EndHorizontal ();
		}
		#endregion
	}
}
