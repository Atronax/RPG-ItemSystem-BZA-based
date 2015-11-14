/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Represents weapon item parameters.
/// </summary>

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
		/// <summary>
		/// Default constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISWeapon"/> class using default values.
		/// </summary>
		public ISWeapon ()
		{
		}

		/// <summary>
		/// Prototyping constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISWeapon"/> class using values of the other instance of that class.
		/// </summary>
		/// <param name="RHS">Instance, used as a prototype.</param>
		public ISWeapon (ISWeapon RHS)
		{
			base.Clone (RHS);
			Clone (RHS);
		}

		/// <summary>
		/// Makes this instance a clone of the specified Prototype.
		/// </summary>
		/// <param name="Prototype">Prototype.</param>
		public void Clone (ISWeapon Prototype)
		{
			m_MinDamage = Prototype.MinDamage;
			m_MaxDamage = Prototype.MaxDamage;
			m_CurrentDurability = Prototype.CurrentDurability;
			m_MaximumDurability = Prototype.MaximumDurability;
			m_Prefab = Prototype.Prefab;
		}

		// ======== ISGameObject interface

		/// <summary>
		/// Gets the prefab.
		/// </summary>
		/// <value>The prefab.</value>
		public GameObject Prefab 
		{
			get { return m_Prefab; }
		}

		// ======== ISWeapon interface

		/// <summary>
		/// Attacks.
		/// </summary>
		public int Attack ()
		{
			// Weapon attack effect. Implement it.
			return 0;
		}

		/// <summary>
		/// Gets or sets the minimum damage.
		/// </summary>
		/// <value>The minimum damage.</value>
		public int MinDamage 
		{
			get { return m_MinDamage; }
			set { m_MinDamage = (value < 0) ? 0 : ((value > m_MaxDamage) ? m_MaxDamage : value); }
		}

		/// <summary>
		/// Gets or sets the max damage.
		/// </summary>
		/// <value>The max damage.</value>
		public int MaxDamage 
		{
			get { return m_MaxDamage; }
			set { m_MaxDamage = (value < 0) ? 0 : value; }
		}

		// ======== ISDestructible interface

		/// <summary>
		/// Lowers current durability level by PT.
		/// Prevents exceeding of the current durability level beyond zero.
		/// </summary>
		/// <param name="PT">Damage taken.</param>
		public void BreakBy (int PT)
		{
			if (m_CurrentDurability >= PT)
				m_CurrentDurability -= PT;
			else
				m_CurrentDurability = 0;

			ApplyBrokenEffect();
		}

		/// <summary>
		/// Applies the broken effect.
		/// </summary>
		public void ApplyBrokenEffect ()
		{
			// Weapon broken effect. For example, we may lower the damage depending on the current durability level.
		}

		/// <summary>
		/// Adds PT to the current durability level.
		/// Prevents exceeding of the current durability level beyond maximum durability level.
		/// </summary>
		/// <param name="PT">Points repaired.</param>
		public void RepairBy (int PT)
		{
			if (m_CurrentDurability <= m_MaximumDurability - PT)
				m_CurrentDurability += PT;
			else
				m_CurrentDurability = m_MaximumDurability;

			ApplyRepairedEffect ();
		}

		/// <summary>
		/// Applies the repaired effect.
		/// </summary>
		public void ApplyRepairedEffect ()
		{
			// Weapon repaired effect. For example, we may add the damage depending on the current durability level.
		}

		/// <summary>
		/// Gets the current durability level.
		/// </summary>
		/// <value>The current durability level.</value>
		public int CurrentDurability 
		{
			get { return m_CurrentDurability; }
		}

		/// <summary>
		/// Gets the maximum durability level.
		/// </summary>
		/// <value>The maximum durability level.</value>
		public int MaximumDurability 
		{
			get { return m_MaximumDurability; }
		}

		// move to another EDITOR class
		#if UNITY_EDITOR
		/// <summary>
		/// Displays the details editor.
		/// </summary>
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
