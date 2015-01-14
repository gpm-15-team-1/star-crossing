using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	//public double height;
	//public double position;
	public double duration;
	public bool sustain;
	public int pointValue;
	private bool mouseClicked;
	private bool isActive;

	// Use this for initialization
	void Start () {
		//height = 1;
		//position = 1;
		duration = 1;
		sustain = false;
		pointValue = 5;
		mouseClicked = false;
		isActive = true;
		//animation.Play ();
	}

//	void HitNote () {
//		hit = true;
//	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mouseClicked = true;
		}
		else {
			mouseClicked = false;
		}
	}
	
	// Alternate update that happens after normal update
	void LateUpdate () {
		GameObject target = GameObject.Find ("Target");
		GameObject songManager = GameObject.Find ("SongManager");
		if (mouseClicked && (target.transform.position.x < this.transform.position.x + .5) && 
		    (target.transform.position.x > this.transform.position.x - .5) &&
		    (target.transform.position.y < this.transform.position.y + .5) &&
		    (target.transform.position.y > this.transform.position.y - .5)) {
			songManager.GetComponent<SongManager>().PossiblePoints += 5;
			songManager.GetComponent<SongManager>().AcquiredPoints += 5;
			Destroy(this.gameObject);
		}
		else {
			if (this.transform.position.x < -4 && isActive) {
				songManager.GetComponent<SongManager>().PossiblePoints += 5;
				isActive = false;
				//Destroy(this.gameObject);
			}
		}
//		if (note was hit just now) 
//		{
//			// draw note as explosion or whatever
//			// handle sound playback
//			// handle score
//			// set countdown for Destroy
//		}
//		else 
//		{
//			// draw note as a note
//			// if note has passed the target's track (i.e. the player missed it), do score things
//		}
	}
}
