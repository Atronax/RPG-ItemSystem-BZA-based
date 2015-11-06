/// <summary>
/// November 6, 2015
/// Author: Zamana Max
/// 
/// ISQuality database.
/// </summary>

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	public class ISQualityDatabase : Database<ISQuality>
	{
		#region public
		/// <summary>
		/// Returns the index of quality type that is named QualityTypeName.
		/// </summary>
		/// <returns>The database index of the quality type.</returns>
		/// <param name="Name">Quality type name.</param>
		public int GetIndexByName (string QualityTypeName)
		{
			return m_Database.FindIndex (Object => Object.Name == QualityTypeName);
		}

		/// <summary>
		/// Returns all quality types names as an array of strings.
		/// </summary>
		/// <returns>String array of quality types names.</returns>
		public string[] NamesAsStringArray ()
		{
			string[] Result = new string[Count()];

			for (int i = 0; i < Count(); ++i)
				Result[i] = Get(i).Name;

			return Result;
		}

		/// <summary>
		/// Stores data of object indexed by Index variable in user defined data structures.
		/// </summary>
		/// <param name="Index">Index of needed object to store.</param>
		/// <param name="QualityTypeName">User defined quality type name variable.</param>
		/// <param name="QualityTypeTexture">User defined quality type texture variable.</param>
		public void CacheIndexedObjectData (int Index, ref string QualityTypeName, ref Texture QualityTypeTexture)
		{
			QualityTypeName = Get(Index).Name;
			if (Get(Index).Icon)
				QualityTypeTexture = Get(Index).Icon.texture;
			else
				QualityTypeTexture = null;
		}
		#endregion		
	}
}