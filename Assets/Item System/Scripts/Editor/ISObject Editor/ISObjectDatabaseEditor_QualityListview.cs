using UnityEditor;
using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor
	{
		#region public
		public void DisplayQualityList (bool BlockedWhenEditing)
		{
			m_ListScrollPosition = EditorGUILayout.BeginScrollView (m_ListScrollPosition, "Box", GUILayout.Width(m_ListWidth), GUILayout.ExpandHeight (true));

			if (BlockedWhenEditing && m_DetailsAreShown)
				GUI.enabled = false;
			
			for (int i = 0; i < m_QualityDB.Count(); ++i) 
			{
				bool IthItemButtonIsClicked = GUILayout.Button (m_QualityDB.Get(i).Name, "Box", GUILayout.ExpandWidth (true));
				if (IthItemButtonIsClicked)
				{
					m_SelectedIndex = i;
					m_TemporaryItem = new ISQuality ();
					m_TemporaryItem.Clone(m_QualityDB.Get(m_SelectedIndex));
					m_DetailsAreShown = true;
				}
			}
			
			if (BlockedWhenEditing)
				GUI.enabled = true;
			
			EditorGUILayout.EndScrollView ();
		}
		#endregion

		#region private
		private bool m_DetailsAreShown;
		private Vector2 m_ListScrollPosition;
		private int m_ListWidth = 200;
		#endregion
	}
}
