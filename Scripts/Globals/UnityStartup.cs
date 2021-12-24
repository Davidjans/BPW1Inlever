using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[ExecuteInEditMode]
public class UnityStartup : MonoBehaviour
{
	[Button]
	private void Awake()
	{
		if (Application.isEditor)
		{
			UnityStarted();
		}
	}

	[Button]
	private void Start()
	{
		if (Application.isEditor)
		{
			UnityStarted();
		}
	}
	private void UnityStarted()
	{
		Devmode.LoadSettings();
	}
}
