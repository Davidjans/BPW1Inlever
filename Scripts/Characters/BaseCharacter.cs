using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class BaseCharacter : SerializedMonoBehaviour
{
    public CharacterStats m_Stats = new CharacterStats();
    public string m_CharacterName = "DefaultCharacterName";

    public float m_EXPWorth = 5;
    [FoldoutGroup("Settings")]
    public LayerMask m_GroundedLayerMask;
    [HideInEditorMode]
    public BaseWeapon m_CurrentWeapon;
    [HideInEditorMode]
    public CharacterSkillManager m_SkillManager;
    [FoldoutGroup("CharacterComponents")]
    [HideInEditorMode] public BaseHealth m_CharacterHealth;
    [FoldoutGroup("CharacterComponents")]
    [HideInEditorMode] public BaseCharacterXP m_CharacterXP;
    protected void Start()
    {
		m_SkillManager = GetComponent<CharacterSkillManager>();
		m_CharacterHealth = GetComponentInChildren<BaseHealth>();
		m_CharacterHealth.m_Owner = this;
		m_CharacterXP = GetComponentInChildren<BaseCharacterXP>();
    }
    
	protected void Update()
    {

    }

	public virtual void PerformWeaponAttack()
	{
        m_CurrentWeapon.Attack();
	}

    [System.Serializable]
    public class CharacterStats
    {
        public float m_Strength;
        public float m_Dexterity;
        public float m_Constitution;
        public float m_Wisdom;
        public float m_Intelligence;
        public float m_Charisma;
		public float m_Luck;
	}
}
