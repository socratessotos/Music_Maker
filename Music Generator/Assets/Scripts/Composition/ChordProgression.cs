using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChordProgression : MonoBehaviour {

	public static Chord GetNextChordMajor (Chord chord) {

		int r = Random.Range (0, 100);

		switch (chord.scaleDegree + 1) {

		case 1:

			if (r < 40) return new Chord (chord.referenceScale, 4, Chord_Type.MAJ7);
			if (r < 70) return new Chord (chord.referenceScale, 5, Chord_Type.MAJ7);
			if (r < 80) return new Chord (chord.referenceScale, 2, Chord_Type.MIN7);
			if (r < 100) return new Chord (chord.referenceScale, 6, Chord_Type.MIN9);

			break;

		case 2:

			if (r < 80) return new Chord (chord.referenceScale, 5, Chord_Type.MAJ7);
			if (r < 100) return new Chord (chord.referenceScale, 3, Chord_Type.MIN7);

			break;

		case 3:

			if (r < 80) return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);
			if (r < 100) return new Chord (chord.referenceScale, 3, Chord_Type.MIN7);

			break;

		case 4:

			if (r < 25) return new Chord (chord.referenceScale, 5, Chord_Type.MAJ7);
			if (r < 70) return new Chord (chord.referenceScale, 6, Chord_Type.MIN9);
			if (r < 85) return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);
			if (r < 100) return new Chord (chord.referenceScale, 2, Chord_Type.MIN7);

			break;

		case 5:

			if (r < 70) return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);
			if (r < 100) return new Chord (chord.referenceScale, 6, Chord_Type.MIN9);

			break;

		case 6:

			if (r < 40) return new Chord (chord.referenceScale, 3, Chord_Type.MIN7);
			if (r < 70) return new Chord (chord.referenceScale, 4, Chord_Type.MAJ7);
			if (r < 90) return new Chord (chord.referenceScale, 2, Chord_Type.MIN7);
			if (r < 100) return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);

			break;

		case 7:

			return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);

			break;

			default:

			return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);
			break;
		}

		return new Chord (chord.referenceScale, 1, Chord_Type.MAJ7);

	}

}
