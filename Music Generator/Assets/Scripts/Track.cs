using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track : MonoBehaviour {

	const int frequency = 44100;
	const float trackDuration = 2.0f;

	private float[] PCMBuffer = 
		new float[ Mathf.CeilToInt(frequency * trackDuration) * 2];

	void CompositeSampleOntoTrack (AudioClip sample, float startTime, float clipStartTime, float clipEndTime) {

		if (clipStartTime > sample.length) return;
		if ((clipStartTime + clipEndTime) > sample.length) clipEndTime = sample.length;

		int startSample = Mathf.CeilToInt (startTime*frequency) * 2;

		float[] sampleData = new float[sample.samples];
		sample.GetData (sampleData, 0);
		clipStartTime *= frequency * 2;

		for (int i = (int)clipStartTime; i < clipEndTime * frequency * 2; i++) {

			if (i >= sampleData.Length) return;

			int targetIndex = startSample + i;

			if (targetIndex < PCMBuffer.Length) {
				PCMBuffer [targetIndex] += sampleData[i];

			} else {

				PCMBuffer [Mathf.Abs(targetIndex % PCMBuffer.Length)] += sampleData[i];

			}

		}

	}
		
	public AudioClip test;

	AudioClip track;
	public AudioSource playback;

	void GenerateTrack () {

		//CompositeSampleOntoTrack (test, 1.9f, 0.0f, test.length);

		track = AudioClip.Create ("Music", 
			Mathf.CeilToInt (frequency * trackDuration),
			2, frequency, false, false);
		
		track.SetData (PCMBuffer, 0);

		playback.clip = track;
		playback.Play ();

	}

	void Awake () {

		GenerateTrack ();

	}

	void Update () {

		//print (playback);

	}

	void ClearPreviousAudioFrame (float playbackTime, float subdivisionLength) {
		
		float deleteAmt = subdivisionLength;

		for (int i = Mathf.CeilToInt ((playbackTime - deleteAmt) * frequency * 2), l = Mathf.CeilToInt(playbackTime * frequency * 2); i < l; i++) {

			PCMBuffer[Mathf.Abs(i % PCMBuffer.Length)] = 0;

		}


	}

	public void WriteNextAudioFrame (List<AudioSample> score, float playbackTime, float subdivisionLength) {

		ClearPreviousAudioFrame (playbackTime, subdivisionLength);

		for (int i = 0; i < score.Count; i++) {

			if (score[i].HasBeenComposited ()) {
				score.Remove(score[i]);

			}

			CompositeSampleOntoTrack (score[i].clip, (score[i].startTime + score[i].currentWriteTime) % trackDuration, score[i].currentWriteTime, subdivisionLength);
			score[i].UpdateCurrentWriteTime (subdivisionLength);

		}

		track.SetData (PCMBuffer, 0);

	}


}
