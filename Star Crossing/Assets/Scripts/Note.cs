using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	//public double height;
	//public double position;
	public double duration;
	public bool sustain;
	public int pointValue;

	// Use this for initialization
	void Start () {
		//height = 1;
		//position = 1;
		duration = 1;
		sustain = false;
		pointValue = 5;
		animation.Play ();
	}

//	void HitNote () {
//		hit = true;
//	}

	// Update is called once per frame
	void Update () {
//		if (hit) 
//		{
//			// draw note as explosion or whatever
//			// handle sound playback
//			// handle score
//			// set countdown for Destroy
//		}
//		else 
//		{
//			// draw note as a note
//			//this.transform = whatever;
//		}
	}
}
