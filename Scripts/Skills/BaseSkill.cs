using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "BaseSkill", menuName = "PlayerStates/BaseSkill", order = 0)]
public class BaseSkill : ScriptableObject
{
    [FoldoutGroup("SkillInfo")]
    public string m_SkillName = "BaseSkill";
    [FoldoutGroup("SkillInfo")]
    public Sprite m_SkillSprite;
    [FoldoutGroup("SkillInfo")]
    public Color m_SkillSpriteColor = Color.white;
	[FoldoutGroup( "SkillInfo")]
    public SkillData m_SkillData;
    [HideInEditorMode]
    public BaseCharacter m_SkillOwner;
	[Serializable]
    public class SkillData
	{
		[FoldoutGroup("SkillInfo")]
		public int m_SkillLevel = 0;
		[FoldoutGroup("SkillInfo")]
		public int m_SkillTier = 1;
		[FoldoutGroup("SkillInfo")]
		public string m_SkillTree = "Acrobat";
		[FoldoutGroup("SkillInfo")]
		public float m_SkillEffectPerLevel = 1;
		[FoldoutGroup("SkillInfo")]
		public int m_MaxSkillLevel = 1;
		[FoldoutGroup("SkillInfo")]
		public int m_SkillPointCostTolevel = 1;
		[FoldoutGroup("SkillInfo")]
		[HideInEditorMode]
		public bool m_Purchaseable = true;
		[FoldoutGroup("Description")]
		[TextArea]
		public string m_DescriptionText;
	}

    public void OnStart()
	{
    }
    
    public void OnUpdate()
	{

	}

    public void OnSpecial()
	{

	}

    public void OnBuyUpgrade()
    {

    }

	public void SetInstanceValues(string skillName, Sprite skillSprite, Color spriteColor, SkillData startData)
	{
		m_SkillName = skillName;
		m_SkillSprite = skillSprite;
		m_SkillSpriteColor = spriteColor;
		m_SkillData = startData;
	}

    public void LoadSkill()
	{
		string fileName = m_SkillOwner.m_CharacterName + "-" + m_SkillName;
		m_SkillData = JsonUtility.FromJson<SkillData>(SaveManager.LoadTheJson(fileName));
	}
    public void SaveSkill()
    {
        string fileName = m_SkillOwner.m_CharacterName + "-" + m_SkillName;
		SaveManager.SaveTheJson(fileName, SaveManager.TurnIntoJson<SkillData>(m_SkillData));
    }

    public float GetValue()
    {
	    return m_SkillData.m_SkillEffectPerLevel * m_SkillData.m_SkillLevel;
    }
}
