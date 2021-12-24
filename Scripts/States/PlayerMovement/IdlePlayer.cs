using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayer : BasePlayerMovement
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
		Vector2 input = m_Owner.m_PlayerInput.gameplay.move.ReadValue<Vector2>();
		CheckStateTransitions(input);
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
		if (JumpInputCheck())
			return;
	}
}
