/// <summary>
/// November 6, 2015
/// Author: Zamana Max
/// 
/// Represent databases of objects, inherited to ISObject class.
/// Gives ability to manipulate their list storage, to display their editor data.
/// As a result - to create new or to edit existing ones.
/// </summary>

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace RPG.ItemSystem.Editor
{
	[System.Serializable]
	public class ISObjectDatabaseH<D,T> where D: Database<T> where T: ISObject, new()
	{
		#region public
		/// <summary>
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.Editor.ISObjectDatabaseH`2"/> class.
		/// </summary>
		/// <param name="DatabaseFileName">Database file name.</param>
		/// <param name="DatabaseObjectName">Wanted database object name.</param>
		public ISObjectDatabaseH (string DatabaseFileName, string DatabaseObjectName)
		{
			m_DatabaseFileName = DatabaseFileName;
			m_DatabaseObjectName = DatabaseObjectName;
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			if (m_Database == null)
				LoadDatabase ();
		}

		/// <summary>
		/// Adds the specified Item to the list.
		/// </summary>
		/// <param name="Item">Item.</param>
		public void Add (T Item)
		{
			m_Database.Items.Add (Item);
			UpdateDatabase (); 
		}

		/// <summary>
		/// Inserts the specified Item at position Index in the list.
		/// </summary>
		/// <param name="Index">Index of the list, where you want to place this Item.</param>
		/// <param name="Item">Item.</param>
		public void Insert (int Index, T Item)
		{
			m_Database.Items.Insert (Index, Item);
			UpdateDatabase ();
		}

		/// <summary>
		/// Removes the specified Item from the list.
		/// </summary>
		/// <param name="Item">Item.</param>
		public void Remove (T Item)
		{
			m_Database.Items.Remove (Item);
			UpdateDatabase ();
		}

		/// <summary>
		/// Removes the Item stored at Index position in the list.
		/// </summary>
		/// <param name="Index">Index of item, which you want to remove.</param>
		public void Remove (int Index)
		{
			m_Database.Items.RemoveAt (Index);
			UpdateDatabase ();
		}

		/// <summary>
		/// Replaces the item at position Index to Item.
		/// </summary>
		/// <param name="Index">Index of element in the list, that you want to replace.</param>
		/// <param name="Item">Item.</param>
		public void Replace (int Index, T Item)
		{
			m_Database.Items[Index] = Item;
			UpdateDatabase ();
		}
		
		// Editor GUI Stuff
		/// <summary>
		/// Displays the list of currenly stored items in the database.
		/// </summary>
		/// <param name="BlockedWhenEditing">If set to <c>true</c> list becomes blocked when editing details.</param>
		public void DisplayList (bool BlockedWhenEditing)
		{
			m_ListScrollPosition = EditorGUILayout.BeginScrollView (m_ListScrollPosition, "Box", GUILayout.Width(m_ListWidth), GUILayout.ExpandHeight (true));
			
			if (BlockedWhenEditing && m_DetailsAreShown)
				GUI.enabled = false;
			
			for (int i = 0; i < m_Database.Count(); ++i) 
			{
				bool IthItemButtonIsClicked = GUILayout.Button (m_Database.Get(i).Name, "Box", GUILayout.ExpandWidth (true));
				if (IthItemButtonIsClicked)
				{
					m_SelectedIndex = i;
					m_TemporaryItem = new T ();
					m_TemporaryItem.Clone(m_Database.Get(m_SelectedIndex));
					m_DetailsAreShown = true;
				}
			}
			
			if (BlockedWhenEditing)
				GUI.enabled = true;
			
			EditorGUILayout.EndScrollView ();
		}

		/// <summary>
		/// Displays the details window elements.
		/// If no details editor is shown, than shows add button.
		/// Else shows details editor window.
		/// </summary>
		public void DisplayDetails ()
		{
			GUILayout.BeginVertical ("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
			if (!m_DetailsAreShown) 
				m_DetailsAreShown = GUILayout.Button ("Add new " + m_DatabaseObjectName);				
			else 
				EditItemDetails ();
			GUILayout.EndVertical ();
		}
		#endregion

		#region private
		// GUI Manipulations
		/// <summary>
		/// Displays detail editor window based on items internal DisplayDetailsEditor function.
		/// After that shows buttons needed to save, delete or cancel.
		/// </summary>
		private void EditItemDetails ()
		{
			m_DetailsAreShown = true;
			
			if (m_TemporaryItem == null)
				m_TemporaryItem = new T();
			
			GUILayout.BeginVertical ("Box", GUILayout.ExpandHeight(true));
			m_TemporaryItem.DisplayDetailsEditor ();
			GUILayout.EndVertical ();
			
			GUILayout.BeginHorizontal ();
			EditItemDetailsButtons ();
			GUILayout.EndHorizontal ();
			
			
		}

		/// <summary>
		/// Shows item details editor buttons.
		/// </summary>
		private void EditItemDetailsButtons ()
		{
			ButtonSave ();
			ButtonDelete ();
			ButtonCancel ();
		}

		/// <summary>
		/// Save button.
		/// If clicked, examines if any item from the list is selected.
		/// If so, replaces its data with updated version.
		/// If not, adds new items to the database.
		/// Resets needed variable to default state.
		/// </summary>
		private void ButtonSave ()
		{
			bool SaveButtonClicked = GUILayout.Button ("SAVE");
			if (SaveButtonClicked) 
			{
				if (m_SelectedIndex == -1)
					Add(m_TemporaryItem);
				else
					Replace(m_SelectedIndex, m_TemporaryItem);
				
				m_SelectedIndex = -1;
				m_TemporaryItem = null;
				m_DetailsAreShown = false;
			}
		}

		/// <summary>
		/// Delete button.
		/// If clicked, asks the user if he is sure with his desision.
		/// If so, removes current item from the database and resets needed variable to default state.
		/// If not, does nothing.		 
		/// </summary>
		private void ButtonDelete ()
		{
			if (m_SelectedIndex != -1) // if any of database items is selected
			{ 
				bool DeleteButtonClicked = GUILayout.Button ("DELETE");
				if (DeleteButtonClicked) 
				{
					bool DeletionChosen = EditorUtility.DisplayDialog ("Removing " + m_DatabaseObjectName + " from the database", 
					                                                   "Are you sure you want to delete " + m_Database.Get(m_SelectedIndex).Name + " from the database?",
					                                                   "Yes", "No");
					if (DeletionChosen)
					{
						Remove(m_SelectedIndex);
						
						m_SelectedIndex = -1;
						m_TemporaryItem = null;
						m_DetailsAreShown = false;
					}
				}
			}
		}

		/// <summary>
		/// Cancel button.
		/// If clicked, resets needed variable to default state.
		/// If not, does nothing.
		/// </summary>
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
		
		// Database Manipulations
		/// <summary>
		/// Loads the database of this instance using Resorces.Load.
		/// If database does not exist, creates it.
		/// </summary>
		private void LoadDatabase ()
		{
			m_Database = Resources.Load (m_DatabaseFolderName + "/" + m_DatabaseFileName) as D;
			if (m_Database == null)
				CreateDatabase ();
		}

		/// <summary>
		/// Creates the database.
		/// If there is no folder for this database, creates it.
		/// Then instantiates database as scriptable object and casts it to needed type.
		/// Creates corresponding asset and refreshed the whole asset database to import changes.
		/// </summary>
		private void CreateDatabase ()
		{
			if (!AssetDatabase.IsValidFolder("Assets/Resources/" + m_DatabaseFolderName))								
				AssetDatabase.CreateFolder("Assets/Resources/", m_DatabaseFolderName);
			
			m_Database = ScriptableObject.CreateInstance<D> () as D;
			AssetDatabase.CreateAsset(m_Database, "Assets/Resources/" + m_DatabaseFolderName + "/" + m_DatabaseFileName + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();							
		}

		/// <summary>
		/// Updates the database.
		/// Every time we update the database, we mark it as dirty, so it reloads after restarting the unity.
		/// </summary>
		private void UpdateDatabase ()
		{
			EditorUtility.SetDirty (m_Database); 
		}
		// basic database information
		[SerializeField] private D m_Database;
		[SerializeField] string m_DatabaseFileName;
		[SerializeField] string m_DatabaseObjectName;
		[SerializeField] string m_DatabaseFolderName = @"Database";

		// database list preferences
		private Vector2 m_ListScrollPosition;
		private int m_ListWidth = 200;

		// database details preferences
		private T m_TemporaryItem;
		private bool m_DetailsAreShown = false;
		private int m_SelectedIndex = -1;
		#endregion
	}
}