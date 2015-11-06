#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISQuality : I_ISQuality 
	{
		#region public
		public ISQuality ()
		{

		}

		public ISQuality (ISQuality RHS)
		{
			Clone (RHS);
		}

		public ISQuality (string QName, Sprite QIcon)
		{
			m_Name = Name;
			m_Icon = Icon;
		}

		public void Clone (ISQuality Prototype)
		{
			m_Name = Prototype.Name;
			m_Icon = Prototype.Icon;
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

		#if UNITY_EDITOR
		public void DisplayDetailsEditor ()
		{
			m_Name = EditorGUILayout.TextField ("Name: ", m_Name);
			m_Icon = EditorGUILayout.ObjectField ("Icon: ", m_Icon, typeof(Sprite), false) as Sprite;
		}
		#endif
		#endregion

		#region private
		[SerializeField] private string m_Name;
		[SerializeField] private Sprite m_Icon;
		#endregion
	}
}