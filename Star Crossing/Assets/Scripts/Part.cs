using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Part : MonoBehaviour {

	public string partName;
	public Note sampleNote;
	public ArrayList notes;
	//public List<Note> notes;
	private bool playerControlled;

	// Use this for initialization
	void Start () {
		partName = "melody";
		notes = new ArrayList ();
		playerControlled = true;
		WritePart ();
	}

	// Temp function that hard-codes in a dummy part
	private void WritePart () {
		notes.Add (Instantiate (sampleNote, new Vector3 (0, 0, 0), Quaternion.identity));
		notes.Add (Instantiate (sampleNote, new Vector3 (2, 1, 0), Quaternion.identity));
		notes.Add (Instantiate (sampleNote, new Vector3 (4, 2, 0), Quaternion.identity));
		notes.Add (Instantiate (sampleNote, new Vector3 (6, 3, 0), Quaternion.identity));
		notes.Add (Instantiate (sampleNote, new Vector3 (8, 4, 0), Quaternion.identity));
		notes.Add (Instantiate (sampleNote, new Vector3 (10, 2, 0), Quaternion.identity));
		notes.Add (Instantiate (sampleNote, new Vector3 (12, 0, 0), Quaternion.identity));
		foreach (Note note in notes) {
			note.transform.parent = gameObject.transform;
		}
	}

	// FixedUpdate is a better Update
	void FixedUpdate () {
		this.transform.position = new Vector3 (this.transform.position.x - 3 * Time.deltaTime, this.transform.position.y, this.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
