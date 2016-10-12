using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track {

	private const int frequency = 44100; //sampling frequency of the audio
	private float trackDuration; //how long will the track be


	//a default contructor for the track
	public Track () {
		trackDuration = 2.0f; //default track duration
		CalculatePCMBufferSize (); //initialize the audio clip PCM float[]
		GenerateTrack (); //create the actual audio clip using the PCM data

	}

	//a constructor that allows for custom track length
	public Track (float _trackDuration) {
		trackDuration = _trackDuration; //user decided track duration
		CalculatePCMBufferSize (); //initialize the audio clip PCM float[]
		GenerateTrack (); //create the actual audio clip using the PCM data

	}

	private float[] PCMBuffer; //float[] containing the audio data from the track

	void CalculatePCMBufferSize () {
		PCMBuffer = new float[Mathf.CeilToInt(frequency * trackDuration) * 2]; //Initialized the PCM []

	}

	public AudioClip track; //Procedurally generated audio clip that will contain the PCMBuffer

	void GenerateTrack () {

		//Creates the actual audio file that will be written to
		track = AudioClip.Create ("Music", 
			Mathf.CeilToInt (frequency * trackDuration),
			2, frequency, false, false); //create the audio clip

		track.SetData (PCMBuffer, 0); //set its data to the PCM Buffer

	}

	int FastAbs (int value) {
		if (value > 0) return value;
		else return -value;
	
	}

	//Function for writing PCM data from audio samples to the track
	void CompositeSampleOntoTrack (AudioClip clip, float songPosition, float clipStartPos, float clipEndPos, float volume = 1) {

		int songPositionSample = Mathf.CeilToInt (songPosition * frequency) * 2; //find the position in the song where we will start writing
		int clipStartPosSample = Mathf.CeilToInt (clipStartPos * frequency) * 2; //find the position in the sample where we will start writing
		int clipEndPosSample = clipStartPosSample + (Mathf.CeilToInt (clipEndPos * frequency) * 2); //find the position in the sample where we will stop writing

		float[] clipData = new float[clip.samples];
		clip.GetData (clipData, 0); //Cache the PCM data of the sample

		clip.GetData (clipData, 100);

		Profiler.BeginSample("BigLoop");
		//iterate through the appropriate data of the sample and copy it to the track
		for (int i = clipStartPosSample; i < clipEndPosSample; i++) {

			if (i >= clipData.Length) return; //return if i is outside of the clipData[] 

			int targetIndex = songPositionSample + i; //Determine where in the PCM to write
			PCMBuffer [FastAbs(targetIndex % PCMBuffer.Length)] += clipData[i] * volume; //Write the data and wrap if it is outside of the scope

		}
		Profiler.EndSample();

	}

	//Clear the previous region data
	public void ClearPreviousAudioRegion (float playbackTime, float subdivisionLength) {
		
		float deleteAmt = subdivisionLength; //Determines the size of the region to clear

		//iterate over the appropriate section of the PCM Buffer
		for (int i = Mathf.CeilToInt ((playbackTime - deleteAmt) * frequency * 2), l = Mathf.CeilToInt(playbackTime * frequency * 2f); i < l; i++) {
			PCMBuffer[FastAbs(i % PCMBuffer.Length)] = 0; //Set the PCM data back to zero

		}

	}

	//Check which audiofiles need to be written and write the next Region of the track
	public void WriteNextAudioRegion (List<AudioSample> samples, float subdivisionLength) {

		for (int i = 0; i < samples.Count; i++) {

			//Write the samples that are up next in the song
			if (samples[i].shouldWrite) {
				CompositeSampleOntoTrack (samples[i].clip, (samples[i].songPos) % trackDuration, samples[i].clipWritePos, subdivisionLength, samples[i].volume);

			}

		}

	}

	public void UpdateTrackData () {
		track.SetData (PCMBuffer, 0); //Set the track's data to the new PCM Buffer

	}

}
