using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	public Track track;

	public float tempo = 120.0f;
	public int signatureHi = 4;
	public int signatureLo = 4;
	public int subdivisions = 1;

	private float subdivisionLength;
	private int playheadPosition = -1;
	private float nextBeat;

	[HideInInspector] public List<AudioSample> samples = new List<AudioSample> ();

	public AudioClip kick;
	public AudioClip snare;
	public AudioClip pad;

	// Use this for initialization
	void Start () {

		subdivisionLength = 60.0f / (tempo * subdivisions);
		nextBeat = 0;
	
	}	
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time >= nextBeat) {

			DetermineNextBeat ();
			nextBeat += subdivisionLength;

			//print (++playheadPosition);
			playheadPosition++;
			if (playheadPosition == signatureHi) playheadPosition = 0;

		}
	
	}

	void DetermineNextBeat () {

		samples.Add (new AudioSample (kick, nextBeat + subdivisionLength));

		for (int i = 0; i < snareHits.Count; i++) {
			if (playheadPosition == snareHits[i]) {

				samples.Add (new AudioSample (snare, nextBeat + subdivisionLength));

			}

		}

		track.WriteNextAudioFrame (samples, nextBeat - subdivisionLength, subdivisionLength);

	}

	private List<int> snareHits = new List<int> ();

	public void GenerateNewBeat () {

		tempo = Random.Range (80f, 160f);
		subdivisionLength = 60.0f / (tempo * subdivisions);
		signatureHi = Random.Range (3, 10);

		snareHits.Clear ();

		int numberOfSnares = Random.Range (2, signatureHi);

		for (int i = 0; i < numberOfSnares; i++) {

			snareHits.Add (Random.Range (1, signatureHi - 1));

		}



	}

}
