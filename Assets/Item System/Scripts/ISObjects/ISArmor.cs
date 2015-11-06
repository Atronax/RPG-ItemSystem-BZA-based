#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISArmor : ISObject, I_ISGameObject, I_ISArmor, I_ISDestructible
	{	
		#region public
		public ISArmor ()
		{
		}

		public ISArmor (ISArmor RHS)
		{
			base.Clone (RHS);
			Clone (RHS);
		}

		public void Clone (ISArmor Prototype)		                   
		{
			m_CurrentArmor = Prototype.CurrentArmor;
			m_MaximumArmor = Prototype.MaximumArmor;
			m_CurrentDurability = Prototype.CurrentDurability;
			m_MaximumDurability = Prototype.MaximumDurability;
			m_Prefab = Prototype.Prefab;
		}

		// ======== ISArmor interface
		public int CurrentArmor 
		{
			get { return m_CurrentArmor; }	
			set { m_CurrentArmor = (value < 0) ? 0 : ((value > m_MaximumArmor) ? m_MaximumArmor : value); }
		}

		public int MaximumArmor 
		{
			get { return m_MaximumArmor; }
			set { m_MaximumArmor = (value < 0) ? 0 : value; }
		}

		// ======== ISGameObject interface
		public GameObject Prefab 
		{
			get { return m_Prefab; }
		}
		
		// ======== ISDestructible interface
		public void BreakBy (int PT)
		{
			if (m_CurrentDurability >= PT)
				m_CurrentDurability -= PT;
			else
				m_CurrentDurability = 0;
			
			ApplyBrokenEffect();
		}
		
		public void ApplyBrokenEffect ()
		{
			// Armor broken effect. For example, we may lower the armor points depending on the current durability level.
			m_CurrentArmor = m_CurrentDurability / m_MaximumDurability * m_MaximumArmor;
		}
		
		public void RepairBy (int PT)
		{
			if (m_CurrentDurability <= m_MaximumDurability - PT)
				m_CurrentDurability += PT;
			else
				m_CurrentDurability = m_MaximumDurability;
			
			ApplyRepairedEffect ();
		}
		
		public void ApplyRepairedEffect ()
		{
			// Weapon repaired effect. For example, we may add the armor points depending on the current durability level.
		}
		
		public int CurrentDurability 
		{
			get { return m_CurrentDurability; }
		}
		
		public int MaximumDurability 
		{
			get { return m_MaximumDurability; }
		}
		
		// move to another EDITOR class
		#if UNITY_EDITOR
		public override void DisplayDetailsEditor ()
		{
			base.DisplayDetailsEditor ();
			
			EditorGUILayout.BeginHorizontal ();
			m_CurrentArmor = EditorGUILayout.IntSlider ("Current Armor: ", m_CurrentArmor, 0, m_MaximumArmor);
			m_MaximumArmor = EditorGUILayout.IntSlider ("Maximum Armor: ", m_MaximumArmor, 0, ARMOR_CAP);
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginHorizontal ();
			m_CurrentDurability = EditorGUILayout.IntSlider ("Current Durability: ", m_CurrentDurability, 0, m_MaximumDurability);
			m_MaximumDurability = EditorGUILayout.IntSlider ("Maximum Durability: ", m_MaximumDurability, m_CurrentDurability, DURABILITY_CAP);
			EditorGUILayout.EndHorizontal ();
			
			m_Prefab = EditorGUILayout.ObjectField ("Prefab: ", m_Prefab, typeof(GameObject), false) as GameObject;
			
		}
		#endif
		#endregion
		
		#region private
		#if UNITY_EDITOR
		private static int ARMOR_CAP = 9999;
		private static int DURABILITY_CAP = 100;
		#endif
		
		[SerializeField] private int m_CurrentArmor;
		[SerializeField] private int m_MaximumArmor;
		[SerializeField] private int m_CurrentDurability;
		[SerializeField] private int m_MaximumDurability;
		[SerializeField] private GameObject m_Prefab;
		#endregion
	}
}