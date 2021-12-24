using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class SkillDescription : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI m_SkillNameText;
	[SerializeField] private TextMeshProUGUI m_SkillTreeText;
	[SerializeField] private TextMeshProUGUI m_SkillDescriptionText;
	[SerializeField] private TextMeshProUGUI m_SkillLevelText;
	[SerializeField] private TextMeshProUGUI m_PurchaseButtonText;
	[SerializeField] private Image m_AbilityImage;
	[SerializeField] private GameObject m_FunctionalParent;
	[SerializeField] private SkillScreenManager m_SkillScreenManager;

	public void UpdateSelectedSkill()
	{
		if (m_SkillScreenManager == null)
		{
			m_SkillScreenManager = transform.parent.GetComponentInChildren<SkillScreenManager>();
		}

		BaseSkill skill = m_SkillScreenManager.m_CurrentSelectedSkill;
		m_FunctionalParent.SetActive(true);
		m_SkillNameText.text = skill.m_SkillName;
		m_SkillTreeText.text = skill.m_SkillData.m_SkillTree;
		m_SkillDescriptionText.text = skill.m_SkillData.m_DescriptionText;
		m_SkillLevelText.text = skill.m_SkillData.m_SkillLevel.ToString();
		m_AbilityImage.sprite = skill.m_SkillSprite;
		m_AbilityImage.color = skill.m_SkillSpriteColor;
		if (skill.m_SkillData.m_MaxSkillLevel == 1)
		{
			m_SkillLevelText.gameObject.SetActive(false);
		}

		if (m_SkillScreenManager.m_CharacterSkillManager.m_LinkedCharacter.m_CharacterXP.m_XPData.m_SkillPoints >= 1)
		{
			if (skill.m_SkillData.m_Purchaseable)
			{
				m_PurchaseButtonText.text = "Buy skill";
			}
			else if (!skill.m_SkillData.m_Purchaseable)
			{
				if (skill.m_SkillData.m_MaxSkillLevel == skill.m_SkillData.m_SkillLevel)
				{
					m_PurchaseButtonText.text = "Max level";
				}
				else
				{
					m_PurchaseButtonText.text = "Not available";
				}
			}
		}
		else
		{
			m_PurchaseButtonText.text = "No skill points";
		}
	}

	public void UpgradeSkill()
	{
		if (m_SkillScreenManager.m_CharacterSkillManager.m_LinkedCharacter.m_CharacterXP.m_XPData.m_SkillPoints >= 1)
		{
			if (m_SkillScreenManager.m_CurrentSelectedSkill.m_SkillData.m_Purchaseable)
			{

				m_SkillScreenManager.m_CurrentSelectedSkill.m_SkillData.m_SkillLevel++;
				string fileName = m_SkillScreenManager.m_CharacterSkillManager.m_LinkedCharacter.m_CharacterName + "-" +
				                  m_SkillScreenManager.m_CurrentSelectedSkill.m_SkillName;
				SaveManager.SaveTheJson(fileName,
					SaveManager.TurnIntoJson<BaseSkill>(m_SkillScreenManager.m_CurrentSelectedSkill));
				UpdateSelectedSkill();
				m_SkillScreenManager.UpdateCurrentSkillTree();
				m_SkillScreenManager.UpdateCurrentSkillTree();
				m_SkillScreenManager.m_CharacterSkillManager.m_LinkedCharacter.m_CharacterXP.m_XPData.m_SkillPoints--;
				Debug.Log("Bought skill");
			}
			else
			{
				if (m_SkillScreenManager.m_CurrentSelectedSkill.m_SkillData.m_SkillLevel >=
				    m_SkillScreenManager.m_CurrentSelectedSkill.m_SkillData.m_MaxSkillLevel)
				{
					Debug.Log("Already maxed");
				}
				else
				{
					Debug.Log("Not avalible");
				}
			}
		}
		else
		{
			Debug.Log("No skill points");
		}

		UpdateSelectedSkill();
	}
}
