using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
public class UIManager : MonoBehaviour
{
	#region instancing
	public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    Debug.LogError("Some reason no uimanager");
                }
            }
            return _instance;
        }
    }

    private static UIManager _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }
	#endregion
	[HideInEditorMode]
    [SerializeField]
    private PlayerCharacterManager m_CharacterManager;
    [FoldoutGroup("XP")]
    [SerializeField] private Image m_XPBar;
    [FoldoutGroup("XP")]
    [SerializeField] private GameObject m_XPParent;
    [FoldoutGroup("XP")]
    [SerializeField] private float m_TimeUntilXPDisables = 5;
    [FoldoutGroup("Health")]
    [SerializeField] private Image m_HealthBar;
    [FoldoutGroup("Health")]
    [SerializeField] private GameObject m_HealthParent;
    [FoldoutGroup("Health")]
    [SerializeField] private float m_TimeUntilHPDisables = 5;

    [SerializeField] private GameObject m_SkillScreenParent;
    [SerializeField] private GameObject m_PauzeMenuParent;
    public void SetXPBar()
	{
        LookForPlayer();
        m_XPParent.SetActive(true);
        m_XPBar.fillAmount = (m_CharacterManager.m_CharacterXP.m_XPData.m_CurrentXP / m_CharacterManager.m_CharacterXP.m_XPNeeded[m_CharacterManager.m_CharacterXP.m_XPData.m_CurrentLevel]);
        CancelInvoke(nameof(DisableXP));
        Invoke(nameof(DisableXP), m_TimeUntilXPDisables);
    }

    public void SetHealthBar()
    {
        LookForPlayer();
        m_HealthParent.SetActive(true);
        m_HealthBar.fillAmount = (m_CharacterManager.m_CharacterHealth.m_CurrentHealth / m_CharacterManager.m_CharacterHealth.m_MaxHealth);

        CancelInvoke(nameof(DisableHP));
        Invoke(nameof(DisableHP), m_TimeUntilHPDisables);
    }

    private void DisableHP()
	{
        m_HealthParent.SetActive(false);
	}
    private void DisableXP()
    {
        m_XPParent.SetActive(false);
    }
    public void LookForPlayer()
	{
        if(m_CharacterManager == null)
		{
            m_CharacterManager = FindObjectOfType<PlayerCharacterManager>();
		}
	}

    public void SwitchSkillScreenActive()
    {
	    LookForPlayer();
        m_SkillScreenParent.SetActive(!m_SkillScreenParent.activeSelf);
        m_CharacterManager.gameObject.SetActive(!m_SkillScreenParent.activeSelf);
        Cursor.visible = !m_CharacterManager.gameObject.activeSelf;
    }

    public void SwitchPauzeScreenActive()
    {
	    LookForPlayer();
        m_PauzeMenuParent.SetActive(!m_PauzeMenuParent.activeSelf);
        m_CharacterManager.gameObject.SetActive(!m_PauzeMenuParent.activeSelf);
        Cursor.visible = !m_CharacterManager.gameObject.activeSelf;
    }
}
