/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Represents armor item parameters.
/// </summary>

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
		/// <summary>
		/// Default constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISArmor"/> class using default values.
		/// </summary>
		public ISArmor ()
		{
		}

		/// <summary>
		/// Prototyping constructor.
		/// Initializes a new instance of the <see cref="RPG.ItemSystem.ISArmor"/> class using values of the other instance of that class.
		/// </summary>
		/// <param name="RHS">Instance, used as a prototype.</param>
		public ISArmor (ISArmor RHS)
		{
			base.Clone (RHS);
			Clone (RHS);
		}

		/// <summary>
		/// Makes this instance a clone of the specified Prototype.
		/// </summary>
		/// <param name="Prototype">Prototype.</param>
		public void Clone (ISArmor Prototype)		                   
		{
			m_CurrentArmor = Prototype.CurrentArmor;
			m_MaximumArmor = Prototype.MaximumArmor;
			m_CurrentDurability = Prototype.CurrentDurability;
			m_MaximumDurability = Prototype.MaximumDurability;
			m_Prefab = Prototype.Prefab;
		}

		// ======== ISArmor interface

		/// <summary>
		/// Gets or sets the current armor value.
		/// </summary>
		/// <value>The current armor value.</value>
		public int CurrentArmor 
		{
			get { return m_CurrentArmor; }	
			set { m_CurrentArmor = (value < 0) ? 0 : ((value > m_MaximumArmor) ? m_MaximumArmor : value); }
		}

		/// <summary>
		/// Gets or sets the maximum armor value.
		/// </summary>
		/// <value>The maximum armor value.</value>
		public int MaximumArmor 
		{
			get { return m_MaximumArmor; }
			set { m_MaximumArmor = (value < 0) ? 0 : value; }
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
			// Armor broken effect. For example, we may lower the armor points depending on the current durability level.
			m_CurrentArmor = m_CurrentDurability / m_MaximumDurability * m_MaximumArmor;
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
			// Armor repaired effect. For example, we may add the armor points depending on the current durability level.
			m_CurrentArmor = m_CurrentDurability / m_MaximumDurability * m_MaximumArmor;
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
		
		// Move to editor class.
		#if UNITY_EDITOR
		/// <summary>
		/// Displays the details editor.
		/// </summary>
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