using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickupWeapon : MonoBehaviour
{
	public BaseWeapon m_WeaponToPickup;
	private void OnTriggerStay(Collider other)
	{
		if (m_WeaponToPickup.m_WeaponOwner == null && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
		{
			BaseWeapon oldWeapon = other.GetComponentInChildren<BaseWeapon>();
			m_WeaponToPickup.transform.parent = oldWeapon.transform.parent;
			m_WeaponToPickup.transform.localPosition= oldWeapon.transform.localPosition;
			m_WeaponToPickup.transform.localRotation = oldWeapon.transform.localRotation;
			m_WeaponToPickup.m_WeaponOwner = oldWeapon.m_WeaponOwner;
			oldWeapon.m_WeaponOwner.m_CurrentWeapon = m_WeaponToPickup;
			Destroy(oldWeapon.gameObject);
			Destroy(GetComponent<Collider>());
			Destroy(this);
		}
	}
}
