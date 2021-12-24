using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
	private void Start()
	{
		Cursor.visible = true;
	}
    public void StartLevel(int sceneToLoad)
	{
		SceneManager.LoadScene(sceneToLoad);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
