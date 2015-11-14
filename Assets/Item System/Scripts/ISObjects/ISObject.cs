/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Represents base item parameters.
/// </summary>

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISObject : ISDatabaseObject, I_ISObject 
	{
		#region public
		/// <summary>
		/// Default contructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISObject"/> class using default values.
		/// </summary>
		public ISObject ()
		{
		}

		/// <summary>
		/// Prototyping constructor
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISObject"/> class using values of the other instance of that class.
		/// </summary>
		/// <param name="RHS">Instance, used as a prototype.</param>
		public ISObject (ISObject RHS)
		{
			base.Clone (RHS);
			Clone (RHS);
		}

		/// <summary>
		/// Makes this instance a clone of the specified Prototype.
		/// </summary>
		/// <param name="Prototype">Prototype.</param>
		public void Clone (ISObject Prototype)
		{
			base.Clone (Prototype);

			m_Quality = Prototype.Quality;
			m_Description = Prototype.Description;
			m_Price = Prototype.Price;
			m_Weight = Prototype.Weight;
		}

		/// <summary>
		/// Gets or sets the quality type.
		/// </summary>
		/// <value>The quality type.</value>
		public ISQuality Quality 
		{
			get { return m_Quality; }
			set { m_Quality = value; }
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description 
		{
			get { return m_Description; }
			set { m_Description = value; }
		}

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>The price.</value>
		public int Price 
		{
			get { return m_Price; }
			set { m_Price = value; }
		}

		/// <summary>
		/// Gets or sets the weight.
		/// </summary>
		/// <value>The weight.</value>
		public int Weight 
		{
			get { return m_Weight; }
			set { m_Weight = value; }
		}

		/// <summary>
		/// Gets the index of the quality type.
		/// </summary>
		/// <value>The index of the quality type.</value>
		public int QualityIndex
		{
			get { return m_QualityIndex; }
		}

		// Move to editor class. 
		// Quality assinging field make much overhead as they are called every frame.
		// Since it is an editor, we may stay it for a while, but sometimes we surely need to fix this problem.
		#if UNITY_EDITOR
		/// <summary>
		/// Displays the details editor.
		/// </summary>
		public override void DisplayDetailsEditor ()
		{
			base.DisplayDetailsEditor ();

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
		[SerializeField] private ISQuality m_Quality; // we may also store index representing position of quality type in the database
		[SerializeField][HideInInspector] private int m_QualityIndex;
		[SerializeField] private string m_Description;
		[SerializeField] private int m_Price;
		[SerializeField] private int m_Weight;
		#endregion
	}
}
