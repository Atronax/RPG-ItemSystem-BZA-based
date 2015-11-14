/// <summary>
/// November 9, 2015
/// Author: Zamana Max
/// 
/// Partial class of ISObjectDatabaseEditor.
/// Represents editor window TabBar functionals.
/// </summary>


using UnityEngine;
using System.Collections;

namespace RPG.ItemSystem.Editor
{
	public partial class ISObjectDatabaseEditor 
	{
		#region private
		/// <summary>
		/// Displays the tabulations bar.
		/// </summary>
		private void DisplayTabulationsBar()
		{
			GUILayout.BeginHorizontal ("Box", GUILayout.ExpandWidth (true));

				Quality ();
				Weapon ();
				Armor ();
				Consumables ();
				About ();				

			GUILayout.EndHorizontal ();
		}

		/* ======================== SPECIALIZED TABS ======================= */
		private void Quality()
		{
			bool QualityButtonClicked = GUILayout.Button ("Quality");
			if (QualityButtonClicked)
				m_TabState = TabState.QUALITY;
		}

		private void Weapon()
		{
			bool WeaponsButtonClicked = GUILayout.Button ("Weapons");
			if (WeaponsButtonClicked)
				m_TabState = TabState.WEAPON;
		}

		private void Armor()
		{
			bool ArmorButtonClicked = GUILayout.Button ("Armor");
			if (ArmorButtonClicked)
				m_TabState = TabState.ARMOR;
		}

		private void Consumables()
		{
			bool ConsumablesButtonClicked = GUILayout.Button ("Consumables");
			if (ConsumablesButtonClicked)
				m_TabState = TabState.CONSUMABLES;
		}

		private void About()
		{
			bool AboutButtonClicked = GUILayout.Button ("About");
			if (AboutButtonClicked)
				m_TabState = TabState.ABOUT;
		}

		private enum TabState { QUALITY, WEAPON, ARMOR, CONSUMABLES, ABOUT };
		private TabState m_TabState;
		#endregion
	}
}