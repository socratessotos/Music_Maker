using UnityEngine;
using System.Collections;

public class Chord {

	public int[] notes;
	public int root;
	public int scaleDegree;
	public Scale referenceScale;

	public Chord (Scale scale, int rootScaleDegree) {

		referenceScale = scale;
		notes = new int[3];
		scaleDegree = rootScaleDegree - 1;
		root = scale.notes[scaleDegree];

		for (int i = 0; i < notes.Length; i++) {
			notes [i] = scale.notes [(rootScaleDegree + (2 * i)) % scale.notes.Length];

		}

	}

	public Chord (Scale scale, int rootScaleDegree, Chord_Type type) {

		int[] chordIntervals = GetChordIntervals (type);

		referenceScale = scale;
		notes = new int[chordIntervals.Length + 1];
		scaleDegree = rootScaleDegree - 1;
		root = scale.notes[scaleDegree];

		notes [0] = root;

		for (int i = 0; i < chordIntervals.Length; i++) {
			
			notes [i + 1] = Scale.allNotes [(root + chordIntervals[i]) % Scale.allNotes.Length];

		}

	}

	static int[] majIntervals = {

		4,
		7,

	};

	static int[] maj7Intervals = {

		4,
		7,
		11

	};

	static int[] minIntervals = {

		3,
		7,

	};

	static int[] min7Intervals = {

		3,
		7,
		14

	};

	static int[] min9Intervals = {

		3,
		7,
		14

	};

	static int[] dom7Intervals = {

		4,
		7,
		10

	};

	int[] GetChordIntervals (Chord_Type type) {

		if (type == Chord_Type.MAJ) {

			return majIntervals; 

		} else if (type == Chord_Type.MAJ7) {

			return maj7Intervals; 

		} else if (type == Chord_Type.MIN) {

			return minIntervals; 

		} else if (type == Chord_Type.MIN7) {

			return min7Intervals; 

		} else if (type == Chord_Type.MIN9) {

			return min9Intervals; 

		} else if (type == Chord_Type.DOM7) {

			return dom7Intervals; 

		}

		return majIntervals;

	}


}
