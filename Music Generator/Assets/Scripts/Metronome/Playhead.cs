using UnityEngine;
using System.Collections;

public class Playhead {

	public int currentMeasure;
	public int currentBeat;
	public int currentSubdivision;

	public int signatureHi;
	public int signatureLo;
	public int subdivisions;

	public Playhead (int _signatureHi, int _signatureLo, int _subdivisions) {

		currentMeasure = 0;
		currentBeat = 0;
		currentSubdivision = 0;

		signatureHi = _signatureHi;
		signatureLo = _signatureLo;
		subdivisions = _subdivisions;

	}

	public void Advance () {

		currentSubdivision++;

		if (currentSubdivision > subdivisions) {
			currentSubdivision = 1;
			currentBeat++;

		}

		if (currentBeat > signatureHi) {
			currentBeat = 1;
			currentMeasure++;

		}

		//Debug.Log ("Measure: " + currentMeasure + " Beat: " + currentBeat + " Subdivision: " + currentSubdivision);

	}

}
