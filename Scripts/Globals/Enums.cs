using Sirenix.OdinInspector;
[PropertyTooltip("Absolute:\nResets movement speed and continues with its own.\n\n" +
	"Continueation: \nContinues the force it had before switching.\n\n" +
	"Still: \nNo movement")]
public enum MovementStyle
{
	Absolute,
	Continueation,
	Still
}

public enum AxisPermitted
{
	X = 1 << 1,
	Y = 1 << 2,
	Z = 1 << 3,
	All = X | Y | Z
}

public enum DevmodeDisplays
{
	Normal,
	Developer,
	Designer,
	Artist
}
public enum AIMovementTypes
{
	Patrol,
	Wander
}

public enum UpdateFrequency
{
	VeryLow,
	Low,
	Medium,
	High,
	Ultra
}

public enum RangedWeaponUpgradeFeature
{
	Damage,
	Cooldown,
	ProjectilePerShot,
	SpreadReduction,
	ReloadReduction,
	ProjectileCapacity,
	ProjectileSpeed
}