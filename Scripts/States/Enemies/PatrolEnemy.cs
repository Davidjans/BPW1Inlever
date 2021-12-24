using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
public class PatrolEnemy : BaseAIState
{
	public GameObject m_WayPointParent;
	public float m_WaitAtPointTime = 2;
	[HideInEditorMode] public List<Transform> m_WayPoints;
	[HideInEditorMode] [SerializeField] private int m_CurrentWayPoint;
	[HideInEditorMode] [SerializeField] private bool m_Waiting = false;
	public override void Initialize(BaseEnemyCharacter owner)
	{
		base.Initialize(owner);
		m_WayPoints = m_WayPointParent.GetAllChildsTransforms();
	}

	public override void OnEnter()
	{
		base.OnEnter();
		//if (m_Owner.m_NavMeshAI != null) m_Owner.m_NavMeshAI.onSearchPath += OnUpdate;
		
		SetDestinationToClosestPath();
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (!m_Waiting)
		{
			GoToWaypoint();
			if (m_Owner.m_NavMeshAI.remainingDistance < m_Owner.m_NavMeshAI.endReachedDistance)
			{
				m_Waiting = true;
				GoToNextPoint();
			}
		}
		m_Owner.m_EnemySpotting.HandleTarget();

		if (m_Owner.m_EnemySpotting.m_SeeingTarget)
		{
			CancelInvoke(nameof(GoToNextPoint));
			m_Owner.m_EnemyStateManager.SwitchState(typeof(AttackingEnemy));
		}
	}
	public override void OnExit()
	{
		base.OnExit();
		//if (m_Owner.m_NavMeshAI != null) m_Owner.m_NavMeshAI.onSearchPath -= OnUpdate;
	}

	private void GoToWaypoint()
	{
		if (m_WayPoints[m_CurrentWayPoint] != null && m_Owner.m_NavMeshAI != null)
		{
			m_Owner.m_NavMeshAI.destination = m_WayPoints[m_CurrentWayPoint].position;
			//Debug.Log("Set destination");
		}
	}

	private async void GoToNextPoint()
	{
		Debug.LogError("went to next point");
		await Task.Delay((int)(m_WaitAtPointTime * 1000));
		m_CurrentWayPoint++;
		if (m_CurrentWayPoint >= m_WayPoints.Count)
		{
			m_CurrentWayPoint = 0;
		}
		GoToWaypoint();
		await Task.Delay(500);
		m_Waiting = false;
	}

	private void SetDestinationToClosestPath()
	{
		float distance = float.PositiveInfinity;
		int closestWaypoint = 0;
		for (int i = 0; i < m_WayPoints.Count; i++)
		{
			float tempDistance = Vector3.Distance(m_WayPoints[i].position, m_Owner.transform.position);
			if (tempDistance < distance)
			{
				distance = tempDistance;
				closestWaypoint = i;
			}
		}
		m_CurrentWayPoint = closestWaypoint;
		GoToWaypoint();
	}
}
