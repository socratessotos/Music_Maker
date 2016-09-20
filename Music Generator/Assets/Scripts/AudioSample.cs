using UnityEngine;

public class AudioSample {

	public AudioClip clip;
	public float startTime;
	public float currentWriteTime;

	public AudioSample (AudioClip _clip, float _startTime) {

		clip = _clip;
		startTime =_startTime;
		currentWriteTime = 0.0f;

	}

	public bool HasBeenComposited () {

		return currentWriteTime >= clip.length;

	}

	public void UpdateCurrentWriteTime (float time) {
		currentWriteTime += time;
	}

}
