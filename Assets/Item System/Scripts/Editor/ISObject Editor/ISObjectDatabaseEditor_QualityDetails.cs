using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor
	{
		#region public
		public void DisplayQualityDetails ()
		{
			GUILayout.BeginVertical ("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
			if (!m_DetailsAreShown) 
				m_DetailsAreShown = GUILayout.Button ("Add new Quality");				
			else 
				EditItemDetails ();
			GUILayout.EndVertical ();
		}
		#endregion
		
		#region private	
		private void EditItemDetails ()
		{
			m_DetailsAreShown = true;
			
			if (m_TemporaryItem == null)
				m_TemporaryItem = new ISQuality();
			
			GUILayout.BeginVertical ("Box", GUILayout.ExpandHeight(true));
			m_TemporaryItem.DisplayDetailsEditor ();
			GUILayout.EndVertical ();
			
			GUILayout.BeginHorizontal ();
			EditItemDetailsButtons ();
			GUILayout.EndHorizontal ();		
		}
		
		private void EditItemDetailsButtons ()
		{
			ButtonSave ();
			ButtonDelete ();
			ButtonCancel ();
		}
		
		private void ButtonSave ()
		{
			bool SaveButtonClicked = GUILayout.Button ("SAVE");
			if (SaveButtonClicked) 
			{
				if (m_SelectedIndex == -1)
					m_QualityDB.Items.Add(m_TemporaryItem);
				else
				{
					m_QualityDB.Items.RemoveAt(m_SelectedIndex);
					m_QualityDB.Items.Insert(m_SelectedIndex, m_TemporaryItem);
				}
				
				m_SelectedIndex = -1;
				m_TemporaryItem = null;
				m_DetailsAreShown = false;
			}
		}
		
		private void ButtonDelete ()
		{
			if (m_SelectedIndex != -1) // if any of database items is selected
			{ 
				bool DeleteButtonClicked = GUILayout.Button ("DELETE");
				if (DeleteButtonClicked) 
				{
					bool DeletionChosen = EditorUtility.DisplayDialog ("Removing quality type from the database", 
					                                                   "Are you sure you want to delete " + m_QualityDB.Get(m_SelectedIndex).Name + " from the database?",
					                                                   "Yes", "No");
					if (DeletionChosen)
					{
						m_QualityDB.Items.RemoveAt(m_SelectedIndex);
						
						m_SelectedIndex = -1;
						m_TemporaryItem = null;
						m_DetailsAreShown = false;
					}
				}
			}
		}
		
		private void ButtonCancel ()
		{
			bool CancelButtonClicked = GUILayout.Button ("CANCEL");
			if (CancelButtonClicked) 
			{
				m_SelectedIndex = -1;
				m_TemporaryItem = null;
				m_DetailsAreShown = false;
			}	
		}

		private ISQuality m_TemporaryItem;
		private int m_SelectedIndex = -1;
		#endregion
	}
}
