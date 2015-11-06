#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem
{
	[System.Serializable]
	public class ISWeapon : ISObject, I_ISGameObject, I_ISWeapon, I_ISDestructible
	{
		#region public
		public ISWeapon ()
		{
		}

		public ISWeapon (ISWeapon RHS)
		{
			base.Clone (RHS);
			Clone (RHS);
		}

		public void Clone (ISWeapon Prototype)
		{
			m_MinDamage = Prototype.MinDamage;
			m_MaxDamage = Prototype.MaxDamage;
			m_CurrentDurability = Prototype.CurrentDurability;
			m_MaximumDurability = Prototype.MaximumDurability;
			m_Prefab = Prototype.Prefab;
		}

		// ======== ISGameObject interface
		public GameObject Prefab 
		{
			get { return m_Prefab; }
		}

		// ======== ISWeapon interface
		public int Attack ()
		{
			// Weapon attack effect. Implement it.
			return 0;
		}

		public int MinDamage 
		{
			get { return m_MinDamage; }
			set { m_MinDamage = (value < 0) ? 0 : ((value > m_MaxDamage) ? m_MaxDamage : value); }
		}

		public int MaxDamage 
		{
			get { return m_MaxDamage; }
			set { m_MaxDamage = (value < 0) ? 0 : value; }
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
			// Weapon broken effect. For example, we may lower the damage depending on the current durability level.
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
			// Weapon repaired effect. For example, we may add the damage depending on the current durability level.
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
				m_MinDamage = EditorGUILayout.IntSlider("Min Damage: ", m_MinDamage, 0, m_MaxDamage);
				m_MaxDamage = EditorGUILayout.IntSlider("Max Damage: ", m_MaxDamage, m_MinDamage, DAMAGE_CAP);
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
		private static int DAMAGE_CAP = 9999;
		private static int DURABILITY_CAP = 100;
		#endif

		[SerializeField] private int m_MinDamage;
		[SerializeField] private int m_MaxDamage;
		[SerializeField] private int m_CurrentDurability;
		[SerializeField] private int m_MaximumDurability;
		[SerializeField] private GameObject m_Prefab;
		#endregion
	}
}
