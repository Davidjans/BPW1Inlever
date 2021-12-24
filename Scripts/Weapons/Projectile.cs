using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Projectile : MonoBehaviour
{
    public float m_Damage;
	[HideInEditorMode]
	public BaseWeapon m_Owner;
    [SerializeField] private float m_LifeTime = 10;

    void Start()
    {
        Invoke("Destroy", m_LifeTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger)
		{
			return;
		}
		BaseCharacter hitCharacter = other.transform.root.GetComponentInChildren<BaseCharacter>();
		if (hitCharacter)
		{
			hitCharacter.m_CharacterHealth.TakeDamage(m_Damage);
			if(m_Owner.m_WeaponOwner.m_CharacterXP != null)
			{
				if(hitCharacter == null || (hitCharacter != null && hitCharacter.m_CharacterHealth.m_IsDead))
				{
					m_Owner.m_WeaponOwner.m_CharacterXP.GainXP(hitCharacter.m_EXPWorth);
				}
			}
		}
		
		Destroy(gameObject);
	}

	private void Destroy()
    {
        Destroy(gameObject);
    }
}
