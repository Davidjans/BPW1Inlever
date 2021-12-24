using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class AnimateDoors : MonoBehaviour
{
	[SerializeField] private Transform m_LeftDoor;
	[SerializeField] private Transform m_RightDoor;
	 private Vector3 m_LeftClosedLocalposition;
	private Vector3 m_RightClosedLocalposition;
	[SerializeField] private Vector3 m_LeftOpenLocalposition;
	[SerializeField] private Vector3 m_RightOpenLocalposition;
	private bool m_Closing = true;

	[Button]
	private void SetOpenPosition()
	{
		m_LeftOpenLocalposition = m_LeftDoor.localPosition;
		m_RightOpenLocalposition = m_RightDoor.localPosition;
	}
	[Button]
	private void SetClosedPosition()
	{
		m_LeftClosedLocalposition = m_LeftDoor.localPosition;
		m_RightClosedLocalposition = m_RightDoor.localPosition;
	}

	private void Update()
	{
		if (m_Closing)
		{
			Vector3 rpos = Vector3.MoveTowards(m_RightDoor.localPosition, m_RightClosedLocalposition, 0.01f);
			m_RightDoor.localPosition = rpos;
			Vector3 lpos = Vector3.MoveTowards(m_LeftDoor.localPosition, m_LeftClosedLocalposition, 0.01f);
			m_LeftDoor.localPosition = lpos;
		}
		else
		{
			Vector3 rpos = Vector3.MoveTowards(m_RightDoor.localPosition, m_RightOpenLocalposition, 0.01f);
			m_RightDoor.localPosition = rpos;
			Vector3 lpos = Vector3.MoveTowards(m_LeftDoor.localPosition, m_LeftOpenLocalposition, 0.01f);
			m_LeftDoor.localPosition = lpos;
		}
	}

	[Button]
	public void OpenDoor()
	{
		m_Closing = false;
	}
	[Button]
	public void CloseDoor()
	{
		m_Closing = true;
	}
	[Button]
	public void SetClosedPos()
	{
		m_RightDoor.localPosition = m_RightClosedLocalposition;
		m_LeftDoor.localPosition = m_LeftClosedLocalposition;
	}
	[Button]
	public void SetOpenPos()
	{
		m_RightDoor.localPosition = m_RightOpenLocalposition;
		m_LeftDoor.localPosition = m_LeftOpenLocalposition;
	}
}
