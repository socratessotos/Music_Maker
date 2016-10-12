using UnityEngine;
using System.Collections;

public class Scale {

	//Length of One Octave
	public const int OCTAVE_LENGTH = 12;

	//Correlating each note to an int
	const int C = 0;
	const int CSharp = 1;
	const int D = 2;
	const int DSharp = 3;
	const int E = 4;
	const int F = 5;
	const int FSharp = 6;
	const int G = 7;
	const int GSharp = 8;
	const int A = 9;
	const int ASharp = 10;
	const int B = 11;

	//Creating an array to store all notes
	public static int[] allNotes = {
		C,
		CSharp,
		D,
		DSharp,
		E,
		F,
		FSharp,
		G,
		GSharp,
		A,
		ASharp,
		B
	};

	static int[] majorIntervals = {

		2,
		4,
		5,
		7,
		9,
		11,

	};

	static int[] minorIntervals = {

		2,
		3,
		5,
		7,
		8,
		10,

	};

	public int[] notes;

	public Scale () {

		Scale_Type scaleType = GetRandomScaleType (Random.Range (0, 1));

		int[] scaleIntervals = GetScaleIntervals (scaleType);
		int scaleLength = GetScaleLength (scaleType);
		int startingNote = GetRandomStartNote ();

		notes = ConstructScale (scaleIntervals, scaleLength, startingNote);

	}

	public Scale (Scale_Type type) {

		int[] scaleIntervals = GetScaleIntervals (type);
		int scaleLength = GetScaleLength (type);
		int startingNote = GetRandomStartNote ();

		notes = ConstructScale (scaleIntervals, scaleLength, startingNote);
		
	}

	Scale_Type GetRandomScaleType (int value) {

		switch (value) {

		case 0:
			return Scale_Type.MAJOR;
			break;
		case 1:
			return Scale_Type.MINOR;
			break;
		case 2:
			return Scale_Type.DORIAN;
			break;
		case 3:
			return Scale_Type.PHRYGIAN;
			break;
		case 4:
			return Scale_Type.LYDIAN;
			break;
		case 5:
			return Scale_Type.MIXOLYDIAN;
			break;
		case 6:
			return Scale_Type.LOCRIAN;
			break;
		case 7:
			return Scale_Type.WHOLETONE;
			break;

		default:
			return Scale_Type.MAJOR;
			break;


		}


	}

	int[] GetScaleIntervals (Scale_Type type) {
		
		if (type == Scale_Type.MAJOR) {

			return majorIntervals; 

		} else {

			return minorIntervals;

		}

	}

	int GetScaleLength (Scale_Type type) {

		if (type == Scale_Type.MAJOR ||
			type == Scale_Type.MINOR ||
			type == Scale_Type.DORIAN ||
			type == Scale_Type.PHRYGIAN ||
			type == Scale_Type.LYDIAN ||
			type == Scale_Type.MIXOLYDIAN ||
			type == Scale_Type.LOCRIAN) {

			return 7;
			
		} else if (type == Scale_Type.WHOLETONE) {

			return 6;

		}

		return 7;
		
	} 

	int GetRandomStartNote () {

		return Random.Range (0, 12);

	}

	int[] ConstructScale (int[] scaleIntervals, int scaleLength, int startingNote) {

		int[] scaleNotes = new int[scaleLength];

		scaleNotes[0] = startingNote;

		for (int i = 1; i < scaleLength; i++) {

			scaleNotes [i] = (startingNote + scaleIntervals [i - 1]) % (allNotes.Length);
			
		}

		return scaleNotes;

	}



}
