using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class BaseCharacterXP : MonoBehaviour
{

	private BaseCharacter m_LinkedCharacter;
	public XPDATA m_XPData;
	
	public List<float> m_XPNeeded;

	[Serializable]
	public  struct XPDATA
	{
		public float m_CurrentXP;
		public int m_CurrentLevel;
		public int m_SkillPoints;
	}


	public void Start()
	{
		if (m_LinkedCharacter == null)
		{
			m_LinkedCharacter = transform.root.GetComponentInChildren<BaseCharacter>();
		}

		string fileName = m_LinkedCharacter.m_CharacterName + "-" + "Experience";
		if (SaveManager.CheckJsonExistence(fileName))
		{
			m_XPData = JsonUtility.FromJson<XPDATA>(SaveManager.LoadTheJson(fileName));
		}
		else
		{
			SaveManager.SaveTheJson(fileName, SaveManager.TurnIntoJson<XPDATA>(m_XPData));
			m_XPData = JsonUtility.FromJson<XPDATA>(SaveManager.LoadTheJson(fileName));
		}
	}
	[Button]
	public virtual void GainXP(float xpGained)
	{
		m_XPData.m_CurrentXP += xpGained;
		if (m_XPData.m_CurrentLevel < m_XPNeeded.Count)
		{
			if (m_XPData.m_CurrentXP > m_XPNeeded[m_XPData.m_CurrentLevel])
			{
				m_XPData.m_CurrentXP -= m_XPNeeded[m_XPData.m_CurrentLevel];
				m_XPData.m_CurrentLevel++;
				m_XPData.m_SkillPoints++;
			}
		}
		SaveData();
	}
	[Button]
	public void SpendSkillPoint(int ammountOfSkillPointsToSpend)
	{
		m_XPData.m_SkillPoints -= ammountOfSkillPointsToSpend;
		SaveData();
	}

	public void SaveData()
	{
		string fileName = m_LinkedCharacter.m_CharacterName + "-" + "Experience";
		SaveManager.SaveTheJson(fileName, SaveManager.TurnIntoJson<XPDATA>(m_XPData));
	}

	private void OnDestroy()
	{
		SaveData();
	}
}
