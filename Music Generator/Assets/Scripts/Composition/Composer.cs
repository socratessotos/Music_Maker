using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Composer : MonoBehaviour {

	Scale currentScale;
	List<Chord> chords = new List <Chord> ();

	// Use this for initialization
	void Awake () {

		currentScale = new Scale ();

		CreateChordProgression (currentScale);

			
	}

	int currentChord = 0;

	void CreateChordProgression (Scale scale) {

		int numberOfChords = 4;

		chords.Add (new Chord (scale, 1, Chord_Type.MAJ));

		for (int i = 0; i < numberOfChords - 1; i++) {

			chords.Add (ChordProgression.GetNextChordMajor (chords [i]));

		}

	}

	//Add this shit to an instrument class later V

	public AudioClip[] samples;

	public void AddSampleToScore (AudioClip clip, float songPosition) {

		SampleManager.samples.Add (new AudioSample (clip, songPosition, 0.3f));

	}

	public void DetermineSamplesToAdd (float songPosition) {

		for (int i = 0; i < chords[currentChord].notes.Length; i++) {

			AddSampleToScore (samples[chords[currentChord].notes[i]], songPosition);

		}

		int lastChord = currentChord;

		currentChord++;

		if (currentChord >= chords.Count) {

			currentChord = 0;

		}

		chords [currentChord] = ChordProgression.GetNextChordMajor (chords [lastChord]);

	}
}
