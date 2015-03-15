using UnityEngine;
using System.Collections;

public class NoteScript : MonoBehaviour {
	private GameObject Pivot;
	private AudioSource hit;
	private AudioSource miss;
	bool isHit = false;
	bool corot = false;
	StatScript StatScript;
	AudioManager AudiomanagerScript;

	// Use this for initialization
	void Awake () {
		Pivot = GameObject.FindGameObjectWithTag ("Pivot");
		hit = Camera.main.transform.Find("Note_hit").GetComponent<AudioSource>();
		miss = Camera.main.transform.Find("Note_miss").GetComponent<AudioSource>();
		GameObject Main_Camera = GameObject.Find("Main Camera");
		StatScript = Main_Camera.GetComponent<StatScript>();
		AudiomanagerScript = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isHit == false && corot == false && gameObject.transform.position.z < Pivot.transform.position.z) {
			StartCoroutine( DestroyThis() );
		}

	}

	void OnTriggerEnter(Collider other)
	{
		print("Called!!!!!");
		if (other.gameObject.tag == "Sphere1" || other.gameObject.tag == "Sphere2") {
			isHit = true;
			gameObject.GetComponent<Renderer>().material.color = Color.green;		
			//hit.Play();
			StatScript.notes_hit++;
			StatScript.number_of_notes++;
			StatScript.current_run++;
			//print("HIT!");
		}
	}



	IEnumerator DestroyThis()
	{
		corot = true;
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		miss.Play();
		AudiomanagerScript.Kill ();
		StatScript.current_run = 0;
		StatScript.number_of_notes++;
		yield return new WaitForSeconds (5.0f);
		Destroy(gameObject);
	}
}


