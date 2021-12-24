using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    public Dictionary<System.Type, BaseState<T>> stateDictionary = new Dictionary<System.Type, BaseState<T>>();
    public BaseState<T> currentState;

    public FSM(T owner, System.Type startState, params BaseState<T>[] states)
	{
		foreach (BaseState<T> state in states)
		{
			state.Initialize(owner);
			stateDictionary.Add(state.GetType(), state);
		}
		SwitchState(startState);
	}

	public void OnUpdate()
	{
		currentState?.OnUpdate();
	}

	public void SwitchState(System.Type newStateType)
	{
		currentState?.OnExit();
		if (currentState != null && stateDictionary[newStateType].AllowUpdateOnEnter && currentState.AllowUpdateOnExit)
			stateDictionary[newStateType].OnUpdate();
		currentState = stateDictionary[newStateType];
		currentState?.OnEnter();
	}
}
