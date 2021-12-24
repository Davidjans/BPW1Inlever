using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pathfinding;
using UnityEngine.Events;

public class BaseEnemyCharacter : BaseCharacter
{
	[FoldoutGroup("States")]
	[SerializeField] private GameObject m_EnemyStatesParents;

	[FoldoutGroup("States")]
	public AIMovementTypes m_DefaultMovementType;
	[FoldoutGroup("States")]
	[HideInEditorMode]
	public FSM<BaseEnemyCharacter> m_EnemyStateManager;

	

	[FoldoutGroup("Assigning")]
	public AIPath m_NavMeshAI;
	

	[HideInEditorMode]
	public PlayerInput m_PlayerInput;

	
	[HideInEditorMode] public Rigidbody m_CharacterRigidBody;
	[HideInEditorMode] public Transform m_CharacterTransform;
	[HideInEditorMode] public Collider m_PrimaryCharacterCollider;
	
	[ShowIf("@Devmode.m_DebugMode")] public string m_CurrentState;

	[FoldoutGroup("CharacterComponents")]
	[HideInEditorMode] public EnemySpotting m_EnemySpotting;
	public void Awake()
	{
		m_PlayerInput = new PlayerInput();
		m_PlayerInput.Enable();
		m_CharacterRigidBody = GetComponent<Rigidbody>();
		m_CharacterTransform = GetComponent<Transform>();
		m_PrimaryCharacterCollider = GetComponent<Collider>();

		m_EnemySpotting = GetComponentInChildren<EnemySpotting>();
		//AddAttackActions();
	}

	private void AssignComponents()
	{
		m_EnemySpotting = GetComponentInChildren<EnemySpotting>();
		m_EnemySpotting.m_Owner = this;
	}

	private void GetInstances()
	{
		m_EnemyStateManager = new FSM<BaseEnemyCharacter>(this, typeof(IdleEnemy), m_EnemyStatesParents.GetComponents<BaseState<BaseEnemyCharacter>>());
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
		m_EnemyStateManager?.OnUpdate();
		m_CurrentState = m_EnemyStateManager?.currentState.GetType().ToString();
	}
	public override void PerformWeaponAttack()
	{
		//base.PerformWeaponAttack();
		m_CurrentWeapon.Attack();
	}
}
