using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagers : MonoBehaviour
{
	public List<BaseEnemyCharacter> m_Enemies = new List<BaseEnemyCharacter>();
	public AnimateDoors m_ExitDoor;

	public void Update()
	{
		for (int i = 0; i < m_Enemies.Count; i++)
		{
			if (m_Enemies[i] == null)
			{
				m_Enemies.RemoveAt(i);
			}
		}

		if (m_Enemies.Count <= 0)
		{
			m_ExitDoor.OpenDoor();
		}
	}
}
