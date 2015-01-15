using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Song : MonoBehaviour {

	public Dictionary<string,Object> parts;
	public string soloRange;
	public Part samplePart;

	// Use this for initialization
	void Start () {
		parts.Add ("melody", Instantiate (samplePart));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
