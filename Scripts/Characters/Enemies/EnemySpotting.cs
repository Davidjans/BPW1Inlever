using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEditor;

public class EnemySpotting : MonoBehaviour
{
	[HideInEditorMode] public BaseEnemyCharacter m_Owner;
	[FoldoutGroup("Spotting")]
	public float m_MinDistanceToDetect;
	[FoldoutGroup("Spotting")]
	public float m_MaxDistanceToDetect;
	[FoldoutGroup("Spotting")]
	public float m_FieldOfViewAngle;
	[FoldoutGroup("Spotting")]
	public float m_TimeToLostWithoutSight;
	[FoldoutGroup("Spotting")]
	public float m_DistanceTillTargetLost;
	[FoldoutGroup("Spotting")]
	public UpdateFrequency m_TargetSpottingFrequency;
	[FoldoutGroup("Spotting/Assigning")]
	public GameObject m_CurrentTarget;
	[FoldoutGroup("Spotting/Assigning")]
	public LayerMask m_ObstacleLayers;
	[FoldoutGroup("Spotting/Assigning")]
	public Transform m_DetectionPoint;

	[FoldoutGroup("Events")]
	public UnityEvent m_OnTargetEnterMinimumRange;
	[FoldoutGroup("Events")]
	public UnityEvent m_OnNoLongerSeeTarget;

	[FoldoutGroup("Debug")]
	public bool m_DebugDrawVision;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public bool m_EnteredMinimumRange;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public bool m_SeeingPlayerLastFrame;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public bool m_SeeingTarget;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public bool m_TargetInLineOfSight;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public bool m_HasPositionOfTarget;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public float m_CanseeTargetUpdateTime;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public Vector3 m_LastTargetPosition;
	[FoldoutGroup("Debug/Values")]
	[HideInEditorMode]
	public float m_LostTargetTime;

	private void Start()
	{
		m_CurrentTarget = FindObjectOfType<PlayerCharacterManager>().transform.gameObject;
	}

	public virtual void HandleTarget()
	{
		m_SeeingPlayerLastFrame = m_SeeingTarget;
		if (m_HasPositionOfTarget && m_CurrentTarget.transform)
		{
			m_LastTargetPosition = m_CurrentTarget.transform.position;
		}
		m_CanseeTargetUpdateTime -= Time.deltaTime;
		if (m_CanseeTargetUpdateTime > 0)
		{
			return;
		}

		if (m_CurrentTarget != null && m_CurrentTarget.transform)
		{
			m_TargetInLineOfSight = CheckCanSeeTarget();
			if (!m_TargetInLineOfSight || targetDistance >= (m_MaxDistanceToDetect + m_DistanceTillTargetLost))
			{
				m_SeeingTarget = false;
				if (m_LostTargetTime < Time.time)
				{
					m_HasPositionOfTarget = false;
					m_LostTargetTime = Time.time + m_TimeToLostWithoutSight;
				}
			}
			else
			{
				m_SeeingTarget = true;
				m_LostTargetTime = Time.time + m_TimeToLostWithoutSight;
				m_HasPositionOfTarget = true;
				Debug.Log("Let the ai know it has seen the target");
				//m_CurrentTarget.isLost = false;
			}
		}
		else
		{
			m_SeeingTarget = false;
			m_TargetInLineOfSight = false;
			m_HasPositionOfTarget = false;
		}
		if (m_SeeingPlayerLastFrame && !m_SeeingTarget)
		{
			m_OnNoLongerSeeTarget.Invoke();
		}
		//HandleLostTarget();
		m_CanseeTargetUpdateTime = GetUpdateTimeFromQuality(m_TargetSpottingFrequency);
	}

	protected virtual bool CheckCanSeeTarget()
	{
		if (m_CurrentTarget != null && m_CurrentTarget.transform != null && InFOVAngle(m_CurrentTarget.transform.position, m_FieldOfViewAngle))
		{
			var eyesPoint = m_DetectionPoint ? m_DetectionPoint.position : transform.position + Vector3.up * (m_Owner.m_PrimaryCharacterCollider.bounds.size.y * 0.8f);
			if (!Physics.Linecast(eyesPoint, m_CurrentTarget.transform.position, m_ObstacleLayers, QueryTriggerInteraction.Ignore))
			{
				if (m_DebugDrawVision)
					Debug.DrawLine(eyesPoint, m_CurrentTarget.transform.position, Color.green, GetUpdateTimeFromQuality(m_TargetSpottingFrequency));
				return true;
			}
			else
			{
				if (m_DebugDrawVision)
					Debug.DrawLine(eyesPoint, m_CurrentTarget.transform.position, Color.red, GetUpdateTimeFromQuality(m_TargetSpottingFrequency));
			}
		}

		return false;
	}

	protected virtual bool InFOVAngle(Vector3 viewPoint, float fieldOfView)
	{
		var eyesPoint = (m_DetectionPoint ? m_DetectionPoint.position : m_Owner.m_PrimaryCharacterCollider.bounds.center);
		if (Vector3.Distance(eyesPoint, viewPoint) < m_MinDistanceToDetect)
		{
			if (!m_EnteredMinimumRange)
			{
				m_EnteredMinimumRange = true;
				m_OnTargetEnterMinimumRange.Invoke();
			}
			return true;
		}
		if (Vector3.Distance(eyesPoint, viewPoint) > m_MaxDistanceToDetect) return false;

		var lookDirection = viewPoint - eyesPoint;
		var rot = Quaternion.LookRotation(lookDirection, Vector3.up);
		var detectionAngle = m_DetectionPoint ? m_DetectionPoint.eulerAngles : transform.eulerAngles;
		var newAngle = rot.eulerAngles - detectionAngle;
		var fovAngleY = newAngle.NormalizeAngle().y;
		var fovAngleX = newAngle.NormalizeAngle().x;
		return fovAngleY <= (fieldOfView * 0.5f) && fovAngleY >= -(fieldOfView * 0.5f) && fovAngleX <= (fieldOfView * 0.5f) && fovAngleX >= -(fieldOfView * 0.5f);
	}
	private float GetUpdateTimeFromQuality(UpdateFrequency frequency)
	{
		return frequency == UpdateFrequency.VeryLow ? 2 : frequency == UpdateFrequency.Low ? 1f : frequency == UpdateFrequency.Medium ? 0.75f : frequency == UpdateFrequency.High ? .25f : 0.1f;
	}
	
	public virtual float targetDistance
	{
		get
		{
			Debug.Log("To do add dead posibility to target");
			if (m_CurrentTarget == null/* || m_CurrentTarget.isDead*/) return Mathf.Infinity;
			return Vector3.Distance(m_CurrentTarget.transform.position, transform.position);
		}
	}

	void OnDrawGizmosSelected()
	{
		DrawWireArc(m_DetectionPoint.position, m_DetectionPoint.forward, m_FieldOfViewAngle, m_MinDistanceToDetect, Color.red);
		DrawWireArc(m_DetectionPoint.position,m_DetectionPoint.forward,m_FieldOfViewAngle,m_MaxDistanceToDetect,Color.green);
	}

	public static void DrawWireArc(Vector3 position, Vector3 dir, float anglesRange, float radius, Color color, float maxSteps = 20)
	{
		Mathf.Clamp(radius, 0, Mathf.Infinity);
		var srcAngles = GetAnglesFromDir(position, dir);
		var initialPos = position;
		var posA = initialPos;
		var stepAngles = anglesRange / maxSteps;
		var angle = srcAngles - anglesRange / 2;
		Gizmos.color = color;
		for (var i = 0; i <= maxSteps; i++)
		{
			var rad = Mathf.Deg2Rad * angle;
			var posB = initialPos;
			posB += new Vector3(radius * Mathf.Cos(rad), 0, radius * Mathf.Sin(rad));

			Gizmos.DrawLine(posA, posB);

			angle += stepAngles;
			posA = posB;
		}
		Gizmos.DrawLine(posA, initialPos);
	}

	static float GetAnglesFromDir(Vector3 position, Vector3 dir)
	{
		var forwardLimitPos = position + dir;
		var srcAngles = Mathf.Rad2Deg * Mathf.Atan2(forwardLimitPos.z - position.z, forwardLimitPos.x - position.x);

		return srcAngles;
	}
}
