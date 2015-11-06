#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISObject : I_ISObject
	{
		#region public
		public ISObject ()
		{
			m_Icon = null;
			m_Quality = null;
		}

		public ISObject (ISObject RHS)
		{
			Clone (RHS);
		}

		public void Clone (ISObject Prototype)
		{
			m_Name = Prototype.Name;
			m_Icon = Prototype.Icon;
			m_Quality = Prototype.Quality;
			m_Description = Prototype.Description;
			m_Price = Prototype.Price;
			m_Weight = Prototype.Weight;
		}


		public string Name 
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		public Sprite Icon 
		{
			get { return m_Icon; }
			set { m_Icon = value; }
		}

		public ISQuality Quality 
		{
			get { return m_Quality; }
			set { m_Quality = value; }
		}

		public string Description 
		{
			get { return m_Description; }
			set { m_Description = value; }
		}

		public int Price 
		{
			get { return m_Price; }
			set { m_Price = value; }
		}

		public int Weight 
		{
			get { return m_Weight; }
			set { m_Weight = value; }
		}

		public int QualityIndex
		{
			get { return m_QualityIndex; }
		}

		// Move to editor class. Quality assinging field make much overhead as they are called every frame.
		// Since it is an editor, we may stay it for a while, but sometimes we surely need to fix this problem.
		#if UNITY_EDITOR
		public virtual void DisplayDetailsEditor ()
		{
			m_Name = EditorGUILayout.TextField ("Name: ", m_Name);
			m_Icon = EditorGUILayout.ObjectField ("Icon: ", m_Icon, typeof(Sprite), false) as Sprite;
			m_QualityIndex = (m_Quality == null) ? 0 : DatabaseManager.QualityDatabase.GetIndexByName (m_Quality.Name);
			m_QualityIndex = EditorGUILayout.Popup ("Quality type: ", m_QualityIndex, DatabaseManager.QualityDatabase.NamesAsStringArray());
			m_Quality = DatabaseManager.QualityDatabase.Get (m_QualityIndex);
			m_Description = EditorGUILayout.TextField ("Description: ", m_Description);
			m_Price = EditorGUILayout.IntField ("Price: ", m_Price);
			m_Weight = EditorGUILayout.IntField ("Weight: ", m_Weight);
		}
		#endif
		#endregion

		#region private
		[SerializeField] private string m_Name;
		[SerializeField] private Sprite m_Icon;
		[SerializeField] private ISQuality m_Quality; // we may also store index representing position of quality type in the database
		[SerializeField][HideInInspector] private int m_QualityIndex;
		[SerializeField] private string m_Description;
		[SerializeField] private int m_Price;
		[SerializeField] private int m_Weight;
		#endregion
	}
}
