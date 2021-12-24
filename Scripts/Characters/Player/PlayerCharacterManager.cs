using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Interactions;

public class PlayerCharacterManager : BaseCharacter
{
	[FoldoutGroup("States")]
	[SerializeField] private GameObject m_MovementStatesParent;
	[FoldoutGroup("States")]
	[HideInEditorMode]
	public FSM<PlayerCharacterManager> m_PlayerMovementState;
	[HideInEditorMode]
	public PlayerInput m_PlayerInput;

	[HideInEditorMode] public Rigidbody m_CharacterRigidBody;
	[HideInEditorMode] public Transform m_CharacterTransform;
	[HideInEditorMode] public Collider m_PrimaryCharacterCollider;
	[ShowIf("@Devmode.m_DebugMode")] public string m_CurrentState;
	public void Awake()
	{
		if(!transform.IsChildOf(GlobalVariableHolder.Instance.transform))
			Destroy(gameObject);
		m_PlayerInput = new PlayerInput();
		m_PlayerInput.Enable();
		m_CharacterRigidBody = GetComponent<Rigidbody>();
		m_CharacterTransform = GetComponent<Transform>();
		m_PrimaryCharacterCollider = GetComponent<Collider>();
		AddAttackActions();
		AddUIActions();
		Cursor.visible = false;
	}

	
	private void GetInstances()
	{
		m_PlayerMovementState = new FSM<PlayerCharacterManager>(this, typeof(IdlePlayer), m_MovementStatesParent.GetComponents<BaseState<PlayerCharacterManager>>());
	}
	// Start is called before the first frame update
	new protected void Start()
    {
		base.Start();
		GetInstances();
    }

    // Update is called once per frame
    new protected void Update()
    {
		base.Update();
		m_PlayerMovementState.OnUpdate();
		m_CurrentState = m_PlayerMovementState.currentState.GetType().ToString();
	}
    public override void PerformWeaponAttack()
    {
		//base.PerformWeaponAttack();
		m_CurrentWeapon.Attack();
    }

	protected virtual void AddAttackActions()
    {
	    m_PlayerInput.gameplay.fire.performed +=
		    ctx =>
		    {
			    //if (ctx.interaction is SlowTapInteraction)
			    //{
			    //	StartCoroutine(BurstFire((int)(ctx.duration * burstSpeed)));
			    //}
			    //else
			    //{
			    Debug.Log("performed");
			    PerformWeaponAttack();
			    //}
			    //m_Charging = false;
		    };
	    m_PlayerInput.gameplay.fire.started +=
		    ctx =>
		    {
			    //if (ctx.interaction is SlowTapInteraction)
			    //	m_Charging = true;
			    Debug.Log("started");
		    };
	    m_PlayerInput.gameplay.fire.canceled +=
		    ctx =>
		    {
			    //m_Charging = false;
			    Debug.Log("Canceled");
		    };
    }

	protected virtual void AddUIActions()
	{
		m_PlayerInput.gameplay.SkillMenu.started +=
			ctx =>
			{
				UIManager.Instance.SwitchSkillScreenActive();
			};
		m_PlayerInput.gameplay.PauseMenu.started +=
			ctx =>
			{
				UIManager.Instance.SwitchPauzeScreenActive();
			};
	}
	private void SwitchCursorState()
	{
		Cursor.visible = !Cursor.visible;
		Screen.lockCursor = !Screen.lockCursor;
	}
}
