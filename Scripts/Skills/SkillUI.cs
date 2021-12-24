using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
public class SkillUI : MonoBehaviour
{
	public BaseSkill m_Skill;
    [FoldoutGroup("ActiveUI")]
	public GameObject m_ActiveUIParent;
	[FoldoutGroup("InActiveUI")]
	public GameObject m_InActiveUIParent;
	[FoldoutGroup("MaxedUI")]
	public GameObject m_MaxedUIParent;

	[FoldoutGroup("ThingsToAssign")]
	public Image[] m_AbilityImage;
	[FoldoutGroup("ThingsToAssign")]
	public TextMeshProUGUI m_AbilityLevelText;
	public SkillUI m_PreviousSkillUi;

	[HideInEditorMode] public SkillScreenManager m_SkillScreenManager;
	private void Start()
	{
		//OnStart();
	}

	public void OnStart()
	{
		if (m_SkillScreenManager.m_CharacterSkillManager.m_SkillsDictionary.ContainsKey(m_Skill.m_SkillName))
		{
			m_Skill = m_SkillScreenManager.m_CharacterSkillManager.m_SkillsDictionary[m_Skill.m_SkillName];
		}
		else
		{
			Debug.LogError("Skill not in dictionary");
		}
		UpdateUI();
	}
	public void UpdateUI()
	{
		if (m_PreviousSkillUi == null || (m_PreviousSkillUi != null && m_PreviousSkillUi.m_Skill.m_SkillData.m_SkillLevel > 0))
		{
			if (m_Skill.m_SkillData.m_MaxSkillLevel == m_Skill.m_SkillData.m_SkillLevel)
			{
				m_Skill.m_SkillData.m_Purchaseable = false;
				EnableMaxedUI();
			}
			else
			{
				m_Skill.m_SkillData.m_Purchaseable = true;
				EnableActiveUI();
			}
		}
		else
		{
			m_Skill.m_SkillData.m_Purchaseable = false;
			EnableInactiveUI();
		}

		if (m_AbilityImage.Length > 0 && m_Skill.m_SkillSprite != null)
		{
			foreach (var VARIABLE in m_AbilityImage)
			{
				VARIABLE.sprite = m_Skill.m_SkillSprite;
				VARIABLE.color = m_Skill.m_SkillSpriteColor;
			}
		}

		if (m_AbilityLevelText != null)
		{
			m_AbilityLevelText.text = m_Skill.m_SkillData.m_SkillLevel.ToString();
			if(m_Skill.m_SkillData.m_MaxSkillLevel == 1)
			{
				m_AbilityLevelText.gameObject.SetActive(false);
			}
		}
	}

	public void EnableActiveUI()
	{
		m_ActiveUIParent.SetActive(true);
		DisableInactiveUI();
		DisableMaxedUI();
	}

	public void EnableInactiveUI()
	{
		m_InActiveUIParent.SetActive(true);
		DisableActiveUI();
		DisableMaxedUI();
	}

	public void EnableMaxedUI()
	{
		m_MaxedUIParent.SetActive(true);
		DisableActiveUI();
		DisableInactiveUI();
	}

	private void DisableActiveUI()
	{
		m_ActiveUIParent.SetActive(false);
	}

	private void DisableInactiveUI()
	{
		m_InActiveUIParent.SetActive(false);
	}

	private void DisableMaxedUI()
	{
		m_MaxedUIParent.SetActive(false);
	}

	public void SelectSkill()
	{
		if (m_SkillScreenManager == null)
		{
			m_SkillScreenManager = GetComponentInParent<SkillScreenManager>();
		}

		m_SkillScreenManager.SelectSkill(m_Skill,this);
	}
}
