using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerXP : BaseCharacterXP
{
	public override void GainXP(float xpGained)
	{
		base.GainXP(xpGained);
		UIManager.Instance.SetXPBar();
	}
}
