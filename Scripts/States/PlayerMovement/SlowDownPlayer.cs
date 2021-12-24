using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlayer : IdlePlayer
{
	public override void Initialize(PlayerCharacterManager owner)
	{
		base.Initialize(owner);
	}

	public override void OnEnter()
	{
		base.OnEnter();
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
		
	}
	public override void OnExit()
	{
		base.OnExit();
	}

	protected override void CheckStateTransitions(Vector2 input)
	{
		if (input.sqrMagnitude > 0.01)
		{
			if (SprintInputCheck())
				return;
			if (WalkInputCheck())
				return;
		}
		if (m_Owner.m_CharacterRigidBody.velocity.sqrMagnitude < 0.01f)
		{
			m_Owner.m_PlayerMovementState.SwitchState(typeof(IdlePlayer));
			return;
		}
	}
}
