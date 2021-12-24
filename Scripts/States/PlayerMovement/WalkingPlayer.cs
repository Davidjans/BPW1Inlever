using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayer : BasePlayerMovement
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
		var input = m_Owner.m_PlayerInput.gameplay.move.ReadValue<Vector2>();
		Move(input);
		CheckStateTransitions(input);
	}
	public override void OnExit()
	{
		base.OnExit();
	}

	protected override void CheckStateTransitions(Vector2 input)
	{
		if (NoInputCheck(input))
			return;
		if (SprintInputCheck())
			return;
		if (JumpInputCheck())
			return;
	}
}
