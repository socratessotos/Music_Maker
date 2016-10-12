using UnityEngine;
using System.Collections;

public abstract class Instrument : MonoBehaviour {

	public AudioClip[] samples;

	public virtual void AddSampleToScore (AudioClip clip, float songPosition) {

		SampleManager.samples.Add (new AudioSample (clip, songPosition));
		
	}

}
