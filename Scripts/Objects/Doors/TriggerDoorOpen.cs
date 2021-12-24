using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorOpen : MonoBehaviour
{
	public bool m_Open;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (m_Open)
			{
				transform.parent.GetComponentInChildren<AnimateDoors>().OpenDoor();
			}
			else
			{
				transform.parent.GetComponentInChildren<AnimateDoors>().CloseDoor();
			}
		}
	}
}
