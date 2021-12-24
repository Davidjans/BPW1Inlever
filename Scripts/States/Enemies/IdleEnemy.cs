using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class IdleEnemy : BaseAIState
{
	private bool m_InitialEnter = true;
	public override void Initialize(BaseEnemyCharacter owner)
	{
		base.Initialize(owner);
	}

	public override void OnEnter()
	{
		base.OnEnter();
		if (m_InitialEnter)
		{
			m_InitialEnter = false;
			Invoke("StartMovement", 0.2f);
		}
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
	}
	public override void OnExit()
	{
		base.OnExit();
	}
}