using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	public string noteName;
	public double position;
	public double duration;
	private bool hit;

	// Use this for initialization
	void Start () {
		noteName = "c4";
		position = 1;
		duration = 1;
		hit = false;
	}

	void HitNote () {
		hit = true;
	}

	// Update is called once per frame
	void Update () {
		if (hit) 
		{
			// draw note as explosion or whatever
			// handle sound playback
			// handle score
			// set countdown for Destroy
		}
		else 
		{
			// draw note as a note
			//this.transform = whatever;
		}
	}
}
