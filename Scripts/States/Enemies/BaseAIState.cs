using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Sirenix.OdinInspector;

public class BaseAIState : BaseState<BaseEnemyCharacter>
{
	#region InfoBox
	[ShowIf("@Devmode.m_DevmodeDisplayType", DevmodeDisplays.Developer)]
	[SerializeField] protected string m_SimpleInfo = "\nClick for more details";
	[ShowIf("@Devmode.m_DevmodeDisplayType", DevmodeDisplays.Developer)]
	[SerializeField] protected string m_DetailedInfo = "";
	#endregion
	[DetailedInfoBox("$m_SimpleInfo",
		"$m_DetailedInfo", InfoMessageType.Info)]
	[FoldoutGroup("StateSettings")]
	public float m_MovementSpeed = 7;
	

	public override void Initialize(BaseEnemyCharacter owner)
	{
		this.m_Owner = owner;
	}

	public override void OnEnter()
	{
	}
	public override void OnUpdate()
	{
	}
	public override void OnExit()
	{

	}

	public bool StartMovement()
	{
		//Debug.LogError(m_Owner);
		//Debug.LogError(m_Owner.m_EnemyStateManager);
		//Debug.LogError(typeof(PatrolEnemy));
		switch (m_Owner.m_DefaultMovementType)
		{
			case AIMovementTypes.Patrol:
				m_Owner.m_EnemyStateManager.SwitchState(typeof(PatrolEnemy));
				return true;
			case AIMovementTypes.Wander:
				m_Owner.m_EnemyStateManager.SwitchState(typeof(WanderEnemy));
				return true;
			default:
				return false;
		}
	}

	public void MoveToPlayer()
	{
		m_Owner.m_NavMeshAI.destination = m_Owner.m_EnemySpotting.m_CurrentTarget.transform.position;
	}
	
}
