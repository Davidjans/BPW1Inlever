
public class BaseEnemyHealth : BaseHealth
{
	protected override void Dead()
	{
		m_Owner.m_CurrentWeapon.transform.parent = null;
		base.Dead();
	}
}
