/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Partial class of ISObjectDatabaseEditor.
/// Represents base editor window functionals.
/// </summary>

using UnityEditor;
using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor : EditorWindow
	{
		#region public
		/// <summary>
		/// Initializes editor window.
		/// Makes it callable using Ctrl+Shift+i hotkey combination
		/// </summary>
		[MenuItem("RPG/Database/Item System Editor %#i")]
		public static void Init()
		{
			ISObjectDatabaseEditor ODE_Window = EditorWindow.GetWindow<ISObjectDatabaseEditor> ();
			ODE_Window.minSize = new Vector2 (800, 600);
			ODE_Window.titleContent = new GUIContent("Item System");
			ODE_Window.Show ();
		}	

		/// <summary>
		/// During the enable event, initializes databases needed for editors work and sets default tab state.
		/// </summary>
		public void OnEnable()
		{
			InitializeDatabases ();

			m_TabState = TabState.ABOUT;
		}

		/// <summary>
		/// Shows UI to the user.
		/// </summary>
		public void OnGUI()
		{
			DisplayTabulationsBar ();
			GUILayout.BeginHorizontal ();			

				switch (m_TabState) 
				{
					case TabState.QUALITY:
						m_QualityDB.DisplayList(true);
						m_QualityDB.DisplayDetails();
						break;

					case TabState.WEAPON:
						m_WeaponDB.DisplayList(true);
						m_WeaponDB.DisplayDetails();
						break;

					case TabState.ARMOR:
						m_ArmorDB.DisplayList(true);
						m_ArmorDB.DisplayDetails();
						break;

					case TabState.CONSUMABLES:
						GUILayout.Label ("CONSUMABLES TAB");
						break;

					case TabState.ABOUT:
						GUILayout.Label ("ABOUT TAB");
						break;
				}
				
			GUILayout.EndHorizontal ();
			DisplayBottomBar ();
		}
		#endregion

		#region private
		/// <summary>
		/// Initializes the databases.
		/// Use DatabaseManager in runtime.
		/// Use ISObjectDatabaseH in editor.
		/// </summary>
		private void InitializeDatabases()
		{
			// m_QualityDB = DatabaseManager.QualityDatabase;
			// m_WeaponDB = DatabaseManager.WeaponDatabase;
			// m_ArmorDB = DatabaseManager.ArmorDatabase;

			m_QualityDB = new ISObjectDatabaseH<ISQualityDatabase, ISQuality> (QUALITY_DATABASE_FILE_NAME, "Quality");
			m_WeaponDB = new ISObjectDatabaseH<ISWeaponDatabase, ISWeapon> (WEAPON_DATABASE_FILE_NAME, "Weapon");
			m_ArmorDB = new ISObjectDatabaseH<ISArmorDatabase, ISArmor> (ARMOR_DATABASE_FILE_NAME, "Armor");

			m_QualityDB.Initialize ();
			m_WeaponDB.Initialize ();
			m_ArmorDB.Initialize ();
		}

		// ISOBJECT DATABASES
		private const string QUALITY_DATABASE_FILE_NAME = "RPG_QualityDatabase";
		private const string WEAPON_DATABASE_FILE_NAME = "RPG_WeaponDatabase";
		private const string ARMOR_DATABASE_FILE_NAME = "RPG_ArmorDatabase";

		// private ISQualityDatabase m_QualityDB;
		private ISObjectDatabaseH<ISWeaponDatabase, ISWeapon> m_WeaponDB;
		private ISObjectDatabaseH<ISArmorDatabase, ISArmor> m_ArmorDB;		
		private ISObjectDatabaseH<ISQualityDatabase, ISQuality> m_QualityDB;
		#endregion
	}
}