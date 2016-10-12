using UnityEngine;

public class AudioSample {

	public AudioClip clip;
	public float songPos;
	public float clipWritePos;
	public float volume;
	public bool shouldWrite;

	public AudioSample (AudioClip _clip, float _startTime) {

		clip = _clip;
		songPos =_startTime;
		volume = 1.0f;
		clipWritePos = 0.0f;
		shouldWrite = false;

	}

	public AudioSample (AudioClip _clip, float _startTime, float _volume) {

		clip = _clip;
		songPos =_startTime;
		volume = _volume;
		clipWritePos = 0.0f;
		shouldWrite = false;

	}

	public bool HasBeenComposited () {

		return (clipWritePos > clip.length);

	}

	public void UpdateCurrentWriteTime (float time) {
		clipWritePos += time;
	}

}
