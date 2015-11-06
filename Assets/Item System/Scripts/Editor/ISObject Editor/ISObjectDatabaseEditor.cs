using UnityEditor;
using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor : EditorWindow
	{
		#region public
		[MenuItem("RPG/Database/Item System Editor %#i")]
		public static void Init()
		{
			ISObjectDatabaseEditor ODE_Window = EditorWindow.GetWindow<ISObjectDatabaseEditor> ();
			ODE_Window.minSize = new Vector2 (800, 600);
			ODE_Window.titleContent = new GUIContent("Item System");
			ODE_Window.Show ();
		}	

		public void OnEnable()
		{
			InitializeDatabases ();

			m_TabState = TabState.ABOUT;
		}

		public void OnGUI()
		{
			DisplayTabulationsBar ();
			GUILayout.BeginHorizontal ();			

				switch (m_TabState) 
				{
					case TabState.QUALITY:
						DisplayQualityList(true);
						DisplayQualityDetails();
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
		private void InitializeDatabases()
		{
			m_QualityDB = DatabaseManager.QualityDatabase;
			m_WeaponDB = new ISObjectDatabaseH<ISWeaponDatabase, ISWeapon> (WEAPON_DATABASE_FILE_NAME, "Weapon");
			m_ArmorDB = new ISObjectDatabaseH<ISArmorDatabase, ISArmor> (ARMOR_DATABASE_FILE_NAME, "Armor");

			m_WeaponDB.Initialize ();
			m_ArmorDB.Initialize ();
		}

		// ISOBJECT DATABASES
		private const string QUALITY_DATABASE_FILE_NAME = "RPG_QualityDatabase";
		private const string WEAPON_DATABASE_FILE_NAME = "RPG_WeaponDatabase";
		private const string ARMOR_DATABASE_FILE_NAME = "RPG_ArmorDatabase";

		private ISQualityDatabase m_QualityDB;
		private ISObjectDatabaseH<ISWeaponDatabase, ISWeapon> m_WeaponDB;
		private ISObjectDatabaseH<ISArmorDatabase, ISArmor> m_ArmorDB;		
		#endregion
	}
}