using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
public class CharacterSkillManager : SerializedMonoBehaviour
{
	[SerializeField] private List<BaseSkill> m_CharacterSkills;
	[FoldoutGroup("Things to assign")]
	public BaseCharacter m_LinkedCharacter;
	[HideInEditorMode]
	public Dictionary<string, BaseSkill> m_SkillsDictionary= new Dictionary<string, BaseSkill>();

	public SkillScreenManager m_SkillScreenManager;
	// Start is called before the first frame update
	protected void Awake(){
		if(m_LinkedCharacter == null)
			m_LinkedCharacter = GetComponent<BaseCharacter>();

		foreach (BaseSkill skill in m_CharacterSkills)
		{

			BaseSkill skillInstance = BaseSkill.CreateInstance<BaseSkill>();
			skillInstance.SetInstanceValues(skill.m_SkillName, skill.m_SkillSprite, skill.m_SkillSpriteColor, skill.m_SkillData);
			skillInstance.m_SkillOwner = m_LinkedCharacter;
			string fileName = m_LinkedCharacter.m_CharacterName + "-" + skill.m_SkillName;
			if (SaveManager.CheckJsonExistence(fileName))
			{
				skillInstance.LoadSkill();
			}
			else
			{
				skillInstance.SaveSkill();
				skillInstance.LoadSkill();
			}

			

			if (!m_SkillsDictionary.ContainsKey(skillInstance.m_SkillName))
			{
				m_SkillsDictionary.Add(skillInstance.m_SkillName, skillInstance);
			}
			skillInstance.OnStart();
		}

		m_SkillScreenManager = UIManager.Instance.GetComponentInChildren<SkillScreenManager>(true);
		if (m_SkillScreenManager != null)
		{
			m_SkillScreenManager.m_CharacterSkillManager = this;
			m_SkillScreenManager.OnStart();
		}
	}

	public float GetSkillValue(string skillName)
	{
		if (m_SkillsDictionary.ContainsKey(skillName) && m_SkillsDictionary[skillName].m_SkillData.m_SkillLevel > 0)
		{
			return (m_SkillsDictionary[skillName].m_SkillData.m_SkillLevel * m_SkillsDictionary[skillName].m_SkillData.m_SkillEffectPerLevel);
		}
		else
		{
			return 0;
		}
	}

	void OnDestroy()
	{
		foreach (BaseSkill skill in m_SkillsDictionary.Values)
		{
			skill.SaveSkill();
		}
	}
}
