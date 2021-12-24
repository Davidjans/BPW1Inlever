
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	// this is not mine i found it somewhere and i needed it. All i did was convert it to a extension.
	public static Vector3 NormalizeAngle(this Vector3 eulerAngle)
	{
		var delta = eulerAngle;

		if (delta.x > 180) delta.x -= 360;
		else if (delta.x < -180) delta.x += 360;

		if (delta.y > 180) delta.y -= 360;
		else if (delta.y < -180) delta.y += 360;

		if (delta.z > 180) delta.z -= 360;
		else if (delta.z < -180) delta.z += 360;

		return new Vector3(delta.x, delta.y, delta.z);//round values to angle;
	}

	public static List<GameObject> GetAllChildsGameObject(this GameObject Go)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < Go.transform.childCount; i++)
		{
			list.Add(Go.transform.GetChild(i).gameObject);
		}
		return list;
	}

	public static List<Transform> GetAllChildsTransforms(this GameObject Go)
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < Go.transform.childCount; i++)
		{
			list.Add(Go.transform.GetChild(i));
		}
		return list;
	}
}
