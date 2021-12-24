using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*!
	Manages the audio of the game.
*/
/*!
	Current main functionality is Spawning a spatial clip with customizable parameters.
*/
public class AudioManager : MonoBehaviour
{
	

	public static AudioManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<AudioManager>();
				if (_instance == null)
				{
					_instance = new GameObject("AudioManager").AddComponent<AudioManager>();
				}
			}
			return _instance;
		}
	}

	private static AudioManager _instance;

	void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this);
			return;
		}

		_instance = this;
	}

	public AudioSource PlaySpatialClipAt(AudioClip[] clipList, Vector3 pos, float volume, float spatialBlend = 1f,
		float randomizePitch = 0)
	{
		return PlaySpatialClipAt(clipList[Random.Range(0, clipList.Length)], pos, volume, spatialBlend, randomizePitch);
	}

	public AudioSource PlaySpatialClipAt(AudioClip clip, Vector3 pos, float volume, float spatialBlend = 1f, float randomizePitch = 0)
	{
		if (clip == null)
		{
			return null;
		}
		GameObject go = new GameObject("SpatialAudio - Temp");
		go.transform.position = pos;

		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;

		source.pitch = getRandomizedPitch(randomizePitch);
		source.spatialBlend = spatialBlend;
		source.volume = volume;
		source.Play();

		Destroy(go, clip.length);

		return source;
	}

	private float getRandomizedPitch(float randomAmount)
	{
		if (randomAmount != 0)
		{
			float randomPitch = Random.Range(-randomAmount, randomAmount);
			return Time.timeScale + randomPitch;
		}

		return Time.timeScale;
	}
}
