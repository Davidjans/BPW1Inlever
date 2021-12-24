using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalVariableHolder : MonoBehaviour
{
	#region Instancing
	public static GlobalVariableHolder Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GlobalVariableHolder>();
				if (_instance == null)
				{
					_instance = new GameObject("GlobalVariableHolder").AddComponent<GlobalVariableHolder>();
				}
			}
			return _instance;
		}
	}

	private static GlobalVariableHolder _instance;

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			
		}
		else if (_instance != this)
		{
			Debug.LogError("Multiple of " + name + " in the scene");
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
	#endregion
}
