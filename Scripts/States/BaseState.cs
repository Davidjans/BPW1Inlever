using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
//[CreateAssetMenu(fileName = "BaseState", menuName = "PlayerStates/BasteState", order = 0)]
public abstract class BaseState<T> : MonoBehaviour
{
	protected T m_Owner;
	[FoldoutGroup("Advanced state settings")]
	public bool AllowUpdateOnEnter;
	[FoldoutGroup("Advanced state settings")]
	public bool AllowUpdateOnExit;
	public virtual void Initialize(T owner)
	{
		this.m_Owner = owner;
	}

	public abstract void OnEnter();
	public abstract void OnUpdate();
	public abstract void OnExit();
}
