using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class JumpingPlayer : BasePlayerMovement
{
	[SerializeField] private float m_JumpPower = 40;
	[HideInEditorMode] [SerializeField] private bool m_JumpOnCooldown = false;
	public override void Initialize(PlayerCharacterManager owner)
	{
		base.Initialize(owner);
	}

	public override void OnEnter()
	{
		base.OnEnter();
		if (!m_JumpOnCooldown)
		{
			SetVerticalVelocity(m_JumpPower);
			m_JumpOnCooldown = true;
		}

		if (!IsInvoking("JumpCooldown"))
		{
			Invoke("JumpCooldown", 0.1f);
		}
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
		var input = m_Owner.m_PlayerInput.gameplay.move.ReadValue<Vector2>();
		CheckStateTransitions(input);
	}
	public override void OnExit()
	{
		base.OnExit();
	}

	protected override void CheckStateTransitions(Vector2 input)
	{
		if (AirMoveCheck(input))
			return;
		if (FallingCheck())
			return;
	}

	private void JumpCooldown()
	{
		m_JumpOnCooldown = false;
	}
}
