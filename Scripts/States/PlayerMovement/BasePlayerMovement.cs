using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class BasePlayerMovement : BaseState<PlayerCharacterManager>
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
	public MovementStyle m_MovementStyle;
	[FoldoutGroup("StateSettings")]
	[HideIf("m_MovementStyle", MovementStyle.Still)]
	public AxisPermitted m_MovementAxis = AxisPermitted.All;
	[FoldoutGroup("StateSettings")]
	[HideIf("m_MovementStyle", MovementStyle.Still)]
	public float m_MovementSpeed = 7;
	public override void Initialize(PlayerCharacterManager owner)
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

	protected void Move(Vector2 input)
	{
		SetHorizontalVelocity(CalculateMovementVelocity(input)); 
	}

	protected virtual Vector3 CalculateMovementDirection(Vector2 input)
	{
		Vector3 _velocity = Vector3.zero;

		_velocity += m_Owner.m_CharacterTransform.right * input.x;
		_velocity += m_Owner.m_CharacterTransform.forward * input.y;

		if (_velocity.magnitude > 1f)
			_velocity.Normalize();

		return _velocity;
	}
	
	protected virtual Vector3 CalculateMovementVelocity(Vector2  input)
	{
		Vector3 _velocity = CalculateMovementDirection(input);

		_velocity *= m_MovementSpeed;

		return _velocity;
	}

	public void SetHorizontalVelocity(Vector3 _velocity)
	{
		_velocity.y = m_Owner.m_CharacterRigidBody.velocity.y;
		m_Owner.m_CharacterRigidBody.velocity = _velocity;
	}

	public void SetVerticalVelocity(float JumpPower)
	{
		m_Owner.m_CharacterRigidBody.AddForce(0,JumpPower,0);
	}
	#region GoToStatesChecks

	protected virtual void CheckStateTransitions(Vector2 input)
	{
	}

	public bool NoInputCheck(Vector2 input)
	{
		if (input.sqrMagnitude < 0.01f)
		{
			Vector3 velocity = m_Owner.m_CharacterRigidBody.velocity;
			velocity.y = 0;
			if (velocity.sqrMagnitude > 0.01f)
			{
				m_Owner.m_PlayerMovementState.SwitchState(typeof(SlowDownPlayer));
			}
			else
			{
				m_Owner.m_PlayerMovementState.SwitchState(typeof(IdlePlayer));
			}

			return true;
		}

		return false;
	}



	public bool WalkInputCheck()
	{
		if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
		{
			m_Owner.m_PlayerMovementState.SwitchState(typeof(WalkingPlayer));
			return true;
		}
		return false;
	}

	public bool SprintInputCheck()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			m_Owner.m_PlayerMovementState.SwitchState(typeof(SprintPlayer));
			return true;
		}
		return false;
	}

	public bool JumpInputCheck()
	{
		if (Input.GetKey(KeyCode.Space) )
		{
			m_Owner.m_PlayerMovementState.SwitchState(typeof(JumpingPlayer));
			return true;
		}
		return false;
	}

	public bool AirMoveCheck(Vector2 input)
	{
		if (input.sqrMagnitude > 0.01f)
		{
			m_Owner.m_PlayerMovementState.SwitchState(typeof(AirMovePlayer));
		}
		return false;
	}

	public bool FallingCheck()
	{
		if (m_Owner.m_CharacterRigidBody.velocity.y <0)
		{
			m_Owner.m_PlayerMovementState.SwitchState(typeof(FallingPlayer));
			return true;
		}
		return false;
	}

	public bool GroundedCheck()
	{
		RaycastHit _hit;
		Vector3 boxSize = m_Owner.m_PrimaryCharacterCollider.bounds.extents;
		boxSize.x /=  2;
		boxSize.z /=  2;
		boxSize.y = 0.1f;
		float distance = m_Owner.m_PrimaryCharacterCollider.bounds.extents.y * 1.02f;
		Physics.BoxCast(m_Owner.m_CharacterTransform.position, boxSize, m_Owner.m_CharacterTransform.up *-1, out _hit, Quaternion.identity
			, distance, m_Owner.m_GroundedLayerMask, QueryTriggerInteraction.Ignore);
 		if (_hit.collider != null)
		{
			return true;
		}

		return false;
	}
	#endregion

	//void OnDrawGizmos()
	//{
	//	if (boxSize != null)
	//	{
	//		if (_hit.collider)
	//		{
	//			//Draw a Ray forward from GameObject toward the hit
	//			Gizmos.DrawRay(m_Owner.m_CharacterTransform.position, (m_Owner.m_CharacterTransform.up * -1) * _hit.distance);
	//			//Draw a cube that extends to where the hit exists
	//			Gizmos.DrawWireCube(m_Owner.m_CharacterTransform.position + (m_Owner.m_CharacterTransform.up * -1) * _hit.distance, boxSize);
	//		}
	//		else
	//		{
	//			Gizmos.DrawRay(m_Owner.m_CharacterTransform.position, (m_Owner.m_CharacterTransform.up * -1) * (m_Owner.m_PrimaryCharacterCollider.bounds.extents.y * 1.02f));
	//			//Draw a cube that extends to where the hit exists
	//			Gizmos.DrawWireCube(m_Owner.m_CharacterTransform.position + (m_Owner.m_CharacterTransform.up * -1) * (m_Owner.m_PrimaryCharacterCollider.bounds.extents.y * 1.02f), boxSize);
	//		}
	//	}

	//}
}
