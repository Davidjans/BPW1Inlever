using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPlayerHere : MonoBehaviour
{

	void Start()
    {
	    FindObjectOfType<PlayerCharacterManager>().transform.position = transform.position;
    }
}
