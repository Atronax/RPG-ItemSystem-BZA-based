/// <summary>
/// November 6, 2015
/// Author: Zamana Max
/// DatabaseManager
/// 
/// In runtime mode returns any of stored databases.
/// In editor mode generates empty databases.
/// </summary>

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public class DatabaseManager : ScriptableObject 
	{
		#region public
		/// <summary>
		/// Gets the quality database.
		/// </summary>
		/// <value>The quality database.</value>
		public static ISQualityDatabase QualityDatabase
		{
			get 
			{ 
				if (m_QualityDatabase == null)
					m_QualityDatabase = GetDatabase<ISQualityDatabase> (DATABASE_FOLDER_NAME, QUALITY_DATABASE_FILE_NAME);
				return m_QualityDatabase; 
			}
		}

		/// <summary>
		/// Gets the weapon database.
		/// </summary>
		/// <value>The weapon database.</value>
		public static ISWeaponDatabase WeaponDatabase
		{
			get 
			{ 
				if (m_WeaponDatabase == null)
					m_WeaponDatabase = GetDatabase<ISWeaponDatabase> (DATABASE_FOLDER_NAME, WEAPON_DATABASE_FILE_NAME);
				return m_WeaponDatabase; 
			}
		}

		/// <summary>
		/// Gets the armor database.
		/// </summary>
		/// <value>The armor database.</value>
		public static ISArmorDatabase ArmorDatabase
		{
			get 			
			{ 
				if (m_ArmorDatabase == null)
					m_ArmorDatabase = GetDatabase<ISArmorDatabase> (DATABASE_FOLDER_NAME, ARMOR_DATABASE_FILE_NAME);
				return m_ArmorDatabase; 
			}
		}

		#endregion

		#region private
		/// <summary>
		/// Gets the database using its Resources folder relative path.
		/// </summary>
		/// <returns>The database of type DatabaseType.</returns>
		/// <param name="FolderName">Folder name.</param>
		/// <param name="FileName">File name.</param>
		private static DatabaseType GetDatabase<DatabaseType> (string FolderName, string FileName) where DatabaseType : ScriptableObject
		{
			DatabaseType Result;

			Result = Resources.Load (FolderName + "/" + FileName) as DatabaseType;

			#if UNITY_EDITOR
			if (Result == null) 
			{
				if (!AssetDatabase.IsValidFolder ("Assets/Resources/" + FolderName))								
					AssetDatabase.CreateFolder ("Assets/Resources/", FolderName);
			
				Result = ScriptableObject.CreateInstance<DatabaseType> () as DatabaseType;
				AssetDatabase.CreateAsset (Result, "Assets/Resources/" + FolderName + "/" + FileName + ".asset");
				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();							
			}
			#endif

			return Result;
		}


		// Constants
		private const string DATABASE_FOLDER_NAME = "Database";
		private const string QUALITY_DATABASE_FILE_NAME = "RPG_QualityDatabase";
		private const string WEAPON_DATABASE_FILE_NAME = "RPG_WeaponDatabase";
		private const string ARMOR_DATABASE_FILE_NAME = "RPG_ArmorDatabase";

		private static ISQualityDatabase m_QualityDatabase;
		private static ISWeaponDatabase m_WeaponDatabase;
		private static ISArmorDatabase m_ArmorDatabase;
		#endregion
	}
}
