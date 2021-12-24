using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem.Interactions;

public class BaseWeapon : MonoBehaviour
{
	[FoldoutGroup("WeaponSettings")] [SerializeField]
	public float m_BaseDamage;
	[FoldoutGroup("WeaponSettings")]
	public Vector2 m_AllowAttackBetweenDistances = new Vector2(1,10);
	[FoldoutGroup("WeaponSettings/Cooldown")] [SerializeField]
	public float m_AttackCooldown;
	[FoldoutGroup("WeaponSettings/Cooldown")] 
	[HideInEditorMode] 
	[SerializeField]
	protected bool m_CurrentlyOnCooldown = false;
	[FoldoutGroup("WeaponSettings/Cooldown")]
	[HideInEditorMode]
	[SerializeField]
	protected float m_CurrentCooldownTimer;

	public BaseCharacter m_WeaponOwner;

	protected delegate void OnCooldown();

	protected event OnCooldown m_CooldownDelegate;
	protected virtual void Start()
	{
		m_CurrentCooldownTimer = m_AttackCooldown;

		OnPickedUp();

	}

	public void OnPickedUp()
	{
		m_WeaponOwner = GetComponentInParent<BaseCharacter>();
		m_WeaponOwner.m_CurrentWeapon = this;
	}

	protected virtual void Update()
	{
		m_CooldownDelegate?.Invoke();
	}

	public virtual void Attack()
	{
		CheckCooldown();
	}

	protected virtual void CheckCooldown()
	{
		StartCooldown();
	}

	protected virtual void StartCooldown()

	{
		m_CurrentlyOnCooldown = true;
		m_CurrentCooldownTimer = m_AttackCooldown;
		m_CooldownDelegate += DoCooldown;
	}

	protected virtual void DoCooldown()
	{
		m_CurrentCooldownTimer -= Time.deltaTime;
		if (m_CurrentCooldownTimer < 0)
		{
			m_CurrentlyOnCooldown = false;
			m_CooldownDelegate -= DoCooldown;
		}
	}

	public void LookAtSpot(Transform target)
	{
		transform.LookAt(target);
	}
}
