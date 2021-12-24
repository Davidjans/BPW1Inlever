using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class SkillScreenManager : MonoBehaviour
{
	[SerializeField] private SkillTree[] m_SkillTrees;
	public CharacterSkillManager m_CharacterSkillManager;
	[HideInEditorMode] public BaseSkill m_CurrentSelectedSkill;
	[HideInEditorMode] public SkillUI m_CurrentSelectedSkillUI;
	private SkillDescription m_SkillDescription;
	[Button]
	public void OnStart()
	{
		if (m_CharacterSkillManager == null)
		{
			m_CharacterSkillManager = FindObjectOfType<CharacterSkillManager>();
		}
		foreach (var VARIABLE in m_SkillTrees)
		{
			foreach (var skillInTree in VARIABLE.m_SkillsInTree)
			{
				skillInTree.m_SkillScreenManager = this;
				skillInTree.OnStart();
				skillInTree.OnStart();
			}
		}
	}

	public void SelectSkill(BaseSkill skill, SkillUI skillUI)
	{
		m_CurrentSelectedSkill = skill;
		m_CurrentSelectedSkillUI = skillUI;
		if (m_SkillDescription == null)
		{
			m_SkillDescription = transform.parent.GetComponentInChildren<SkillDescription>(true);
		}
		m_SkillDescription.UpdateSelectedSkill();
	}

	public void UpdateCurrentSkillTree()
	{
		SkillTree skillTree = m_CurrentSelectedSkillUI.GetComponentInParent<SkillTree>();
		foreach (var skillUI in skillTree.m_SkillsInTree)
		{
			skillUI.UpdateUI();
		}
	}
}
