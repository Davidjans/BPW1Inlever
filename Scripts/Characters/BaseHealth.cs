using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class BaseHealth : MonoBehaviour
{
	public BaseCharacter m_Owner;
    public float m_MaxHealth;
    [HideInEditorMode] [ProgressBar(0,100, ColorGetter = "GetHealthBarColor")]
    public float m_CurrentHealth;
	public bool m_IsDead = false;

    protected virtual void Start()
	{
		m_CurrentHealth = m_MaxHealth;
	}

	public virtual void TakeDamage(float damage)
	{
		m_CurrentHealth -= damage;
		if (m_CurrentHealth < 0)
		{
			Dead();
		}
	}

	protected virtual void Dead()
	{
		m_IsDead = true;
		Destroy(transform.root.gameObject);
	}

	private Color GetHealthBarColor(float value)
	{
		return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / 100f, 2));
	}
}
