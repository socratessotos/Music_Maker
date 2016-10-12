using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (AudioSource))]
public class SampleManager: MonoBehaviour {

	public static List<AudioSample> samples = new List<AudioSample> (); //The audio samples of the song

	public Track track; //Track that the song will be written to

	private const float bufferLength = 0.5f; //how long a single audio region is
	private float nextWriteTime;  //The next time samples will be written to the track

	void Awake () {
		track = new Track ();  //initialize the track
		GetComponent<AudioSource> ().clip = track.track; //Put the track into the AudioSource of this object
		GetComponent<AudioSource> ().Play (); //Play the track;

		nextWriteTime = 0.0f; //initialize nextWriteTime

	}

	// Update is called once per frame
	void Update () {
		
		DetermineWritableSamples (); //Check which samples need to be written in the next region

		//Check if it is time to write the next region
		if (Time.time >= nextWriteTime) {
			WriteSamplesToTrack (); //write the next audio region to the track
			nextWriteTime += bufferLength; //Update the timer

		}

	}

	//Check which samples need to be written in the next region
	void DetermineWritableSamples () {
	
		for (int i = 0; i < samples.Count; i++) {

			if (Time.time + bufferLength * 2 > samples[i].songPos) { //if the sample if close enough to the playhead
				samples[i].shouldWrite = true; //Set the sample to be writable

			}

		}
	
	}

	//Manage the sample array
	void UpdateSamples () {

		//iterate backwards for accurate index
		for (int i = samples.Count - 1; i >= 0; i--) {

			//remove samples that have been fully written
			if (samples[i].HasBeenComposited ()) {
				samples.Remove(samples[i]);
				continue;

			}

			//keep track of how much of each sample was written
			if (samples[i].shouldWrite) {
				samples[i].UpdateCurrentWriteTime (bufferLength);
			}

		}

	}

	void WriteSamplesToTrack () {
		
		track.ClearPreviousAudioRegion (nextWriteTime, bufferLength); //Clear previous audio region
		track.WriteNextAudioRegion (samples, bufferLength); //Write next audio region
		track.UpdateTrackData (); //Update the PCM data of the track

		UpdateSamples ();  //Manage the sample array

	}

}
