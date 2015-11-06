/// <summary>
/// November 6, 2015
/// Author: Zamana Max
/// Database
/// 
/// Stores all the item data of type T in a single list. 
/// Gives access to this list.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.ItemSystem
{
	public class Database<T> : ScriptableObject where T: class
	{
		#region public
		/// <summary>
		/// Gets the items list.
		/// </summary>
		/// <value>The items.</value>
		public List<T> Items
		{
			get { return m_Database; }
		}		

		/// <summary>
		/// Returns count of objects in the list.
		/// </summary>
		public int Count ()
		{
			return m_Database.Count;
		}

		/// <summary>
		/// Gets object specified by Index.
		/// </summary>
		/// <param name="Index">Index in the list.</param>
		public T Get (int Index)
		{
			return m_Database [Index];
		}
		#endregion

		#region proteced
		// [HideInInspector]
		[SerializeField] protected List<T> m_Database = new List<T>(); 
		#endregion
	}
}
