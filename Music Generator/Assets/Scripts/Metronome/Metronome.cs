using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour{
	
	public static Playhead playhead;

	public DrumKit drums;
	public Composer composer;

	public float tempo = 120.0f;  //The tempo of the current song
	public int signatureHi = 4;  //The numerator of the time signature
	public int signatureLo = 4;  //The denominator of the time signature
	public int subdivisions = 4;  //The smallest unit of measurement

	private float initialDSPTime;
	private float subdivisionLength;
	private float nextSubdivision;

	// Use this for initialization
	void Awake () {

		tempo = Random.Range (80.0f, 140.0f);
		signatureHi = Random.Range (3, 6);
		
		subdivisionLength = 60.0f / (tempo * subdivisions);  //Determine the subdivision length in seconds
		nextSubdivision = subdivisionLength + lookAhead; //The time when the next subdivision will be determined

		playhead = new Playhead (signatureHi, signatureLo, subdivisions); //A playhead object that will keep track of the measures and beats of the music
	
	}

	float lookAhead = 1.0f; //how far in the future will audio samples be calculated

	void Update () {

		//Simple timer that will act as a metronome
		if (Time.time + lookAhead > nextSubdivision) {
			playhead.Advance ();  //Move the music forward
			nextSubdivision += subdivisionLength; //Move the timer forward

			//Add Samples to the Sample Manager here
			drums.DetermineSamplesToAdd (nextSubdivision);

			int currentSubdivision = (Metronome.playhead.currentMeasure * signatureHi * Metronome.playhead.subdivisions + Metronome.playhead.currentSubdivision + (Metronome.playhead.currentBeat - 1) * Metronome.playhead.subdivisions) - 1;

			if (currentSubdivision % 64 == 0) {
				composer.DetermineSamplesToAdd (nextSubdivision);

			}


		}

	}

}
