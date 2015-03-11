using UnityEngine;
using System.Collections;

public class NoteScript : MonoBehaviour {
	private GameObject Pivot;
	private AudioSource hit;
	private AudioSource miss;
	bool isHit = false;
	bool corot = false;
	StatScript StatScript;

	// Use this for initialization
	void Awake () {
		Pivot = GameObject.FindGameObjectWithTag ("Pivot");
		hit = Camera.main.transform.Find("Note_hit").GetComponent<AudioSource>();
		miss = Camera.main.transform.Find("Note_miss").GetComponent<AudioSource>();
		GameObject Main_Camera = GameObject.Find("Main Camera");
		StatScript = Main_Camera.GetComponent<StatScript>();
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

	IEnumerator StopSound()
	{
		Debug.Log("Muting");
		GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0.0f;
		yield return new WaitForSeconds (0.5f);
		GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 1.0f;
	}

	IEnumerator DestroyThis()
	{
		corot = true;
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		miss.Play();
		SendMessage("StopSound");
		StatScript.current_run = 0;
		StatScript.number_of_notes++;
		yield return new WaitForSeconds (5.0f);
		Destroy(gameObject);
	}
}


