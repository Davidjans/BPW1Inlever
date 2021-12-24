using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class SkillHolder : SerializedMonoBehaviour
{
	#region Instancing
	public static SkillHolder Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<SkillHolder>();
				if (_instance == null)
				{
					Debug.LogError("SkillHolder missing from scene");
				}
			}
			return _instance;
		}
	}

	private static SkillHolder _instance;

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
	}
	#endregion
	public Dictionary<string, BaseSkill> m_SkillsDictionary;
}
