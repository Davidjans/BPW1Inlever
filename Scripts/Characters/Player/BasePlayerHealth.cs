using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BasePlayerHealth : BaseHealth
{
	public override void TakeDamage(float damage)
	{
		base.TakeDamage(damage);
		UIManager.Instance.SetHealthBar();
	}
	protected override void Dead()
	{
		m_IsDead = true;
		SceneManager.LoadScene(4);
	}
}
