using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrumKit : Instrument {

	public int[] kicks;
	public int[] snares;
	public int[] hats;

	void Awake () {

		kicks = new int[Metronome.playhead.signatureHi * Metronome.playhead.subdivisions];
		snares = new int[Metronome.playhead.signatureHi * Metronome.playhead.subdivisions];
		hats = new int[Metronome.playhead.signatureHi * Metronome.playhead.subdivisions];

		for (int i = 0; i < kicks.Length; i++) {
			kicks[i] = Random.Range (0, 2);

		}

		kicks [0] = 1;

		int firstSnare = (Random.Range (1,4)) * 4;
		int snareFrequency = Random.Range (3, 9);
		for (int i = firstSnare - 1; i < snares.Length; i+= snareFrequency) {

			snares [i] = 1;

		}

		for (int i = 0; i < hats.Length; i++) {
			hats[i] = Random.Range (0, 10);

		}

	}

	public override void AddSampleToScore (AudioClip clip, float songPosition) {
		base.AddSampleToScore (clip, songPosition);

	}

	bool shouldChange = false;
	public void DetermineSamplesToAdd (float songPosition) {

		int currentSubdivision = (Metronome.playhead.currentSubdivision + (Metronome.playhead.currentBeat - 1) * Metronome.playhead.subdivisions) - 1;

		if (currentSubdivision < 1) return;

		if (kicks [currentSubdivision] == 1) {

			AddSampleToScore (samples[0], songPosition);

		}

		if (snares [currentSubdivision] == 1) {

			if (snares [(currentSubdivision + 1) % snares.Length] == 1) {
				AddSampleToScore (samples[1], songPosition);

			} else {
				AddSampleToScore (samples[2], songPosition);

			}


		}

		if (hats [currentSubdivision] < 2) {

		} else if (hats [currentSubdivision] < 3) {
			AddSampleToScore (samples[4], songPosition);

		} else {
			AddSampleToScore (samples[3], songPosition);

		}

		shouldChange = true;

	}

	void Update () {

		if (shouldChange) {
			int currentSubdivision = (Metronome.playhead.currentSubdivision + (Metronome.playhead.currentBeat - 1) * Metronome.playhead.subdivisions) % (Metronome.playhead.signatureHi * Metronome.playhead.subdivisions);

			//kicks [currentSubdivision] = Random.Range (0, 2);
			//snares [currentSubdivision] = Random.Range (0, 2);
			hats[currentSubdivision] = Random.Range (0, 10);

			shouldChange = !shouldChange;
		}

	}
		
}
