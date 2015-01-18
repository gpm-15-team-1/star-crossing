using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Note : MonoBehaviour {

	//public double height;
	//public double position;
	public double duration;
	public bool sustain;
	public int pointValue;
	public Sprite[] sprites;
	private bool mouseClicked;
	private bool isActive;
	private bool hit;
	private Stopwatch destroyTimer;

	// Use this for initialization
	void Start () {
		//height = 1;
		//position = 1;
		duration = 1;
		sustain = false;
		pointValue = 5;
		mouseClicked = false;
		isActive = true;
		hit = false;
		destroyTimer = new Stopwatch ();
	}

	// Update is called once per frame
	void Update () {
		// determine click state for available notes
		if (Input.GetMouseButtonDown (0)) {
			mouseClicked = true;
		}
		else {
			mouseClicked = false;
		}
		// animate hit notes
		if (hit) {
			double elapsedTime = destroyTimer.Elapsed.TotalSeconds;
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>() as SpriteRenderer;
			if (elapsedTime > 0 && elapsedTime <= 0.1) 
				spriteRenderer.sprite = sprites[0];
			else if (elapsedTime > 0.1 && elapsedTime <= 0.2)
				spriteRenderer.sprite = sprites[1];
			else if (elapsedTime > 0.2)
				spriteRenderer.sprite = sprites[2];
		}
	}
	
	// Alternate update that happens after normal update
	void LateUpdate () {
		GameObject target = GameObject.Find ("Target");
		GameObject songManager = GameObject.Find ("SongManager");
		if (mouseClicked && isActive && 
		    (target.transform.position.x < this.transform.position.x + .5) && 
		    (target.transform.position.x > this.transform.position.x - .5) &&
		    (target.transform.position.y < this.transform.position.y + .5) &&
		    (target.transform.position.y > this.transform.position.y - .5)) {
			songManager.GetComponent<SongManager>().PossiblePoints += 5;
			songManager.GetComponent<SongManager>().AcquiredPoints += 5;
			hit = true;
			isActive = false;
			Destroy(this.gameObject,0.3f);
			destroyTimer.Start();
		}
		else if (this.transform.position.x < -4 && isActive) {
			songManager.GetComponent<SongManager>().PossiblePoints += 5;
			isActive = false;
			//Destroy(this.gameObject);
		}
	}
}
