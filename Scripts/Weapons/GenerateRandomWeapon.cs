using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

public class GenerateRandomWeapon : MonoBehaviour
{
	[MinMaxSlider(0, 500, true)] public Vector2 m_AllocationRange = new Vector2(0, 50);
	[HideInEditorMode] public float m_AllocationPoints;
	[FoldoutGroup("PurchaseValues")] public float m_DamagePerPoint;
	[FoldoutGroup("PurchaseValues")] public float m_CooldownPerPoint;
	[FoldoutGroup("PurchaseValues")] public float m_ProjectilePerPoint;
	[FoldoutGroup("PurchaseValues")] public float m_SpreadReductionPerPoint;
	[FoldoutGroup("PurchaseValues")] public float m_ReloadreductionPerPoint;
	[FoldoutGroup("PurchaseValues")] public float m_ProjectileCapacityPerPoint;
	[FoldoutGroup("PurchaseValues")] public float m_ProjectileSpeedPerPoint;
	public GenericRangedWeapon m_WeaponToChange;

	private float m_BaseDamage;
	private float m_BaseCooldown;
	private float m_BaseProjectileAmount;
	private Vector2 m_BaseSpread;
	private float m_BaseReload;
	private float m_BaseProjectileCapacity;
	private float m_BaseProjectileSpeed;
	private List<bool> m_PurchaseAllows;
	async private void Awake()
	{
		m_PurchaseAllows = new List<bool>();
		SetBaseValues();

		for (int i = 0; i < Enum.GetValues(typeof(RangedWeaponUpgradeFeature)).Length; i++)
		{
			m_PurchaseAllows.Add(true);
		}
		m_AllocationPoints = Mathf.RoundToInt(Random.Range(m_AllocationRange.x, m_AllocationRange.y));
		while (m_AllocationPoints > 0)
		{
			int itemToPurchase = Random.Range(0, m_PurchaseAllows.Count);
			if (m_PurchaseAllows[itemToPurchase])
			{
				PurchaseUpgrade(itemToPurchase);
				m_AllocationPoints--;
			}
		}
		SetWeaponValues();
		Destroy(this,2);
	}

	private void PurchaseUpgrade(int thingToUpgrade)
	{
		RangedWeaponUpgradeFeature upgradeFeature = (RangedWeaponUpgradeFeature)thingToUpgrade;
		switch (upgradeFeature)
		{
			case RangedWeaponUpgradeFeature.Damage:
				UpgradeDamage();
				break;
			case RangedWeaponUpgradeFeature.Cooldown:
				UpgradeCooldown();
				break;
			case RangedWeaponUpgradeFeature.ProjectilePerShot:
				UpgradeProjectileShot();
				break;
			case RangedWeaponUpgradeFeature.SpreadReduction:
				UpgradeSpread();
				break;
			case RangedWeaponUpgradeFeature.ReloadReduction:
				UpgradeReload();
				break;
			case RangedWeaponUpgradeFeature.ProjectileCapacity:
				UpgradeCapacity();
				break;
			case RangedWeaponUpgradeFeature.ProjectileSpeed:
				UpgradeSpeed();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void UpgradeDamage()
	{
		m_BaseDamage += m_DamagePerPoint;
	}
	private void UpgradeCooldown()
	{
		m_BaseCooldown += m_CooldownPerPoint;
	}
	private void UpgradeProjectileShot()
	{
		m_BaseProjectileAmount += m_ProjectilePerPoint;
	}
	private void UpgradeSpread()
	{
		m_BaseSpread  = new Vector2( m_BaseSpread.x += m_SpreadReductionPerPoint, m_BaseSpread.y += m_SpreadReductionPerPoint);
	}
	private void UpgradeReload()
	{
		m_BaseReload += m_ReloadreductionPerPoint;
	}
	private void UpgradeCapacity()
	{
		m_BaseProjectileCapacity += m_ProjectileCapacityPerPoint;
	}
	private void UpgradeSpeed()
	{
		m_BaseProjectileSpeed += m_ProjectileSpeedPerPoint;
	}

	private void SetBaseValues()
	{
		m_BaseDamage = m_WeaponToChange.m_BaseDamage;
		m_BaseCooldown = m_WeaponToChange.m_AttackCooldown;
		m_BaseProjectileAmount =  m_WeaponToChange.m_ProjectilesPerShot;
		m_BaseSpread = m_WeaponToChange.m_Spread;
		m_BaseReload = m_WeaponToChange.m_ReloadDuration;
		m_BaseProjectileCapacity = m_WeaponToChange.m_ProjectileCapacity;
		m_BaseProjectileSpeed = m_WeaponToChange.m_ProjectileSpeed;
	}

	private void SetWeaponValues()
	{
		m_WeaponToChange.m_BaseDamage = m_BaseDamage;
		m_WeaponToChange.m_AttackCooldown = m_BaseCooldown;
		m_WeaponToChange.m_ProjectilesPerShot = Mathf.RoundToInt(m_BaseProjectileAmount);
		m_WeaponToChange.m_Spread = m_BaseSpread;
		m_WeaponToChange.m_ReloadDuration = m_BaseReload;
		m_WeaponToChange.m_ProjectileCapacity = Mathf.RoundToInt(m_BaseProjectileCapacity);
		m_WeaponToChange.m_ProjectileSpeed = m_BaseProjectileSpeed ;
	}
}
