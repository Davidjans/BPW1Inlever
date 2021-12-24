using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using Sirenix.OdinInspector;
public class GenericRangedWeapon : BaseWeapon
{

	[FoldoutGroup("WeaponSettings/Ranged")] 
	public int m_ProjectilesPerShot = 1;
	[FoldoutGroup("WeaponSettings/Ranged")]
	public Vector2 m_Spread = new Vector2(0,0);
    [FoldoutGroup("WeaponSettings/Ranged")] 
    public float m_ProjectileSpeed = 10;
    [FoldoutGroup("WeaponSettings/Ranged")]
    public float m_ReloadDuration = 10;
    [FoldoutGroup("WeaponSettings/Ranged")]
    public int m_ProjectileCapacity = 20;
    [FoldoutGroup("WeaponSettings/Ranged/Assigning")] [SerializeField]
    protected GameObject m_ProjectilePrefab;
	[FoldoutGroup("WeaponSettings/Ranged/Assigning")] [SerializeField] 
    protected Transform m_ProjectileOrigin;

	[FoldoutGroup("WeaponSettings/Ranged/Assigning")] [SerializeField]
    protected AudioClip[] m_ShotAudio = new AudioClip[0];
    [FoldoutGroup("WeaponSettings/DebugValues")] [SerializeField] [HideInEditorMode]
    protected int m_CurrentProjectileCount = 0;
    [FoldoutGroup("WeaponSettings/DebugValues")] [SerializeField] [HideInEditorMode]
    protected bool m_CurrentlyReloading;
    [FoldoutGroup("WeaponSettings/DebugValues")] [SerializeField] [HideInEditorMode]
    protected float m_CurrentReloadTimer = 0;

    protected delegate void OnReload();
    protected event OnReload m_ReloadDelegate;

    protected override void Start()
    {
        base.Start();
        m_CurrentProjectileCount = m_ProjectileCapacity;
    }
    
    protected override void Update()
    {
        base.Update();
        m_ReloadDelegate?.Invoke();
    }

    public override void Attack()
    {
	    if (m_CurrentlyOnCooldown)
		    return;
        CheckCooldown();
	    
        FireProjectile();
    }

    private void FireProjectile()
    {
        if (!m_CurrentlyReloading && m_CurrentProjectileCount > 0)
        {
            for (int i = 0; i < m_ProjectilesPerShot; i++)
            {
                GameObject bullet = Instantiate(m_ProjectilePrefab, m_ProjectileOrigin.position, m_ProjectileOrigin.rotation, m_ProjectileOrigin);
                Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();

                float randomx = Random.Range(-m_Spread.x, m_Spread.x);
                float randomy = Random.Range(-m_Spread.y, m_Spread.y);
                bullet.transform.Rotate(randomx, randomy, 0);
                bulletRigid.AddForce(bullet.transform.forward * m_ProjectileSpeed);
                bullet.transform.parent = null;
                Projectile projectile = bullet.GetComponent<Projectile>();
                float damage = m_BaseDamage;
                if (m_WeaponOwner.m_SkillManager != null && m_WeaponOwner.m_SkillManager.m_SkillsDictionary.ContainsKey("Furious"))
                {
	                damage *= 1 + ((m_WeaponOwner.m_SkillManager.m_SkillsDictionary["Furious"].m_SkillData.m_SkillLevel *
	                               m_WeaponOwner.m_SkillManager.m_SkillsDictionary["Furious"].m_SkillData
		                               .m_SkillEffectPerLevel) / 10);
                    Debug.LogError(damage);
                }
                projectile.m_Damage = damage;
                projectile.m_Owner = this;
            }
            if (m_ShotAudio.Length > 0)
            {
                AudioManager.Instance.PlaySpatialClipAt(m_ShotAudio, m_ProjectileOrigin.position, 0.6f, 1);
            }
            m_CurrentProjectileCount--;
        }
        if (m_CurrentProjectileCount <= 0)
        {
            StartReload();
        }
    }

    protected virtual void StartReload()
    {
	    m_CurrentlyReloading = true;
	    m_CurrentReloadTimer = m_ReloadDuration;
	    m_ReloadDelegate += DoReload;
    }

    protected virtual void DoReload()
    {
	    m_CurrentReloadTimer -= Time.deltaTime;
        if (m_CurrentReloadTimer < 0)
	    {
		    m_ReloadDelegate -= DoReload;
		    m_CurrentlyReloading = false;
		    m_CurrentProjectileCount = m_ProjectileCapacity;
        }
    }
}
