using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchSceneOnTrigger : MonoBehaviour
{
	public int SceneIndexToSwitchTo = 1;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(SceneIndexToSwitchTo);
		}
	}
}
