// connect to the db
// spawn items from the db

using UnityEngine;
using System.Collections;
using RPG.ItemSystem;

[DisallowMultipleComponent]
public class Demo1 : MonoBehaviour 
{
	#region public	
	public void OnGUI()
	{
		for (int i = 0; i < DatabaseManager.WeaponDatabase.Count(); ++i) 
		{
			bool SpawnButtonClicked = GUILayout.Button ("Spawn " + DatabaseManager.WeaponDatabase.Get(i).Name);
			if (SpawnButtonClicked)
				Spawn (i);
		}
		
	}
	#endregion

	#region private
	private void Spawn (int Index)
	{
		ISWeapon Weapon = DatabaseManager.WeaponDatabase.Get(Index);

		GameObject WeaponGO = Instantiate (Weapon.Prefab);


		Weapon WeaponComponent = WeaponGO.AddComponent<Weapon> ();
		WeaponComponent.m_WeaponName = WeaponGO.name = Weapon.Name;
		WeaponComponent.m_Description = Weapon.Description;
		WeaponComponent.m_Icon = Weapon.Icon;
		WeaponComponent.m_Price = Weapon.Price;
		WeaponComponent.m_Weight = Weapon.Weight;
		WeaponComponent.m_Quality = Weapon.Quality.Icon;
		WeaponComponent.m_MinDamage = Weapon.MinDamage;
		WeaponComponent.m_MaxDamage = Weapon.MaxDamage;
		WeaponComponent.m_CurDurability = Weapon.CurrentDurability;
		WeaponComponent.m_MaxDurability = Weapon.MaximumDurability;
	}
	#endregion
}
