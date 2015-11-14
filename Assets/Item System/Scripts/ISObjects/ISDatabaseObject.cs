/// <summary>
/// November 14, 2015
/// Author: Zamana Max
/// 
/// Represents basic database object.
/// All objects in our item system must have name and icon.
/// Besides, all of them must implement function, that displays its values to editor, so that user can edit them.
/// </summary>

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISDatabaseObject : I_ISDatabaseObject 
	{
		#region public
		/// <summary>
		/// Default constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISDatabaseObject"/> class using default values.
		/// </summary>
		public ISDatabaseObject ()
		{
		}

		/// <summary>
		/// Initializing consructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISDatabaseObject"/> class using inputed values.
		/// </summary>
		/// <param name="Name">Name.</param>
		/// <param name="Icon">Icon.</param>
		public ISDatabaseObject (string Name, Sprite Icon)
		{
			m_Name = Name;
			m_Icon = Icon;
		}

		/// <summary>
		/// Prototyping constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISDatabaseObject"/> class using values of the other instance of that class.
		/// </summary>
		/// <param name="RHS">Prototype.</param>
		public ISDatabaseObject (ISDatabaseObject RHS)
		{
			Clone (RHS);
		}

		/// <summary>
		/// Makes this instance a clone of the specified Prototype.
		/// </summary>
		/// <param name="Prototype">Prototype.</param>
		public virtual void Clone (ISDatabaseObject Prototype)
		{
			m_Name = Prototype.Name;
			m_Icon = Prototype.Icon;
		}

		/// <summary>
		/// Displays the details editor.
		/// </summary>
		# if UNITY_EDITOR
		public virtual void DisplayDetailsEditor ()
		{
			m_Name = EditorGUILayout.TextField ("Name: ", m_Name);
			m_Icon = EditorGUILayout.ObjectField ("Icon: ", m_Icon, typeof(Sprite), false) as Sprite;
		}
		#endif

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name 
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		
		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public Sprite Icon 
		{
			get { return m_Icon; }
			set { m_Icon = value; }
		}
		#endregion

		#region private
		[SerializeField] private string m_Name;
		[SerializeField] private Sprite m_Icon;
		#endregion
	}
}
