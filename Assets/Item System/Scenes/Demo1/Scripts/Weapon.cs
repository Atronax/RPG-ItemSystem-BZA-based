using UnityEngine;
using System.Collections;
using RPG.ItemSystem;

[DisallowMultipleComponent]
public class Weapon : MonoBehaviour 
{
	#region public
	public string m_WeaponName;
	public string m_Description;
	public Sprite m_Icon;
	public int m_Price;
	public int m_Weight;
	public Sprite m_Quality;
	public int m_MinDamage;
	public int m_MaxDamage;
	public int m_CurDurability;
	public int m_MaxDurability;
	#endregion
}
