using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
	GameObject MovingEntities;
	// Use this for initialization
	void Awake () {
		MovingEntities = GameObject.FindGameObjectWithTag ("MovingEntities");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MovingEntities.transform.Translate (Vector3.back * 8.0f * Time.deltaTime);
	}
}
