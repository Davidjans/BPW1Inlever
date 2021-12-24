using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Sirenix.OdinInspector;

public class AttackingEnemy : BaseAIState
{
	public override void Initialize(BaseEnemyCharacter owner)
	{
		base.Initialize(owner);

	}

	public override void OnEnter()
	{
		base.OnEnter();
		//if (m_Owner.m_NavMeshAI != null) m_Owner.m_NavMeshAI.onSearchPath += OnUpdate;
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (m_Owner.m_EnemySpotting.m_SeeingTarget)
		{
			if (m_Owner.m_CurrentWeapon.GetType() == typeof(GenericRangedWeapon))
			{
				m_Owner.m_CurrentWeapon.LookAtSpot(m_Owner.m_EnemySpotting.m_CurrentTarget.transform);
			}
			else
			{
				MoveToPlayer();
			}
			if (CheckInAttackRange())
			{
				m_Owner.PerformWeaponAttack();
			}
		}
		else
		{
			MoveToPlayer();
		}
		m_Owner.m_EnemySpotting.HandleTarget();
		if (!m_Owner.m_EnemySpotting.m_HasPositionOfTarget)
		{
			StartMovement();
		}
	}
	public override void OnExit()
	{
		base.OnExit();
		//if (m_Owner.m_NavMeshAI != null) m_Owner.m_NavMeshAI.onSearchPath -= OnUpdate;
	}

	public bool CheckInAttackRange()
	{
		float remainingDistance = m_Owner.m_NavMeshAI.remainingDistance;
		if (remainingDistance > m_Owner.m_CurrentWeapon.m_AllowAttackBetweenDistances.x &&
		    remainingDistance < m_Owner.m_CurrentWeapon.m_AllowAttackBetweenDistances.y)
		{
			return true;
		}

		return false;
	}
}
