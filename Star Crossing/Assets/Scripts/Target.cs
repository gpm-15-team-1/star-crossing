using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public float mouseTargetScalar;

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		float yPos = (Input.mousePosition.y - Screen.height / 2) * mouseTargetScalar;
		this.transform.position = new Vector3 (this.transform.position.x, yPos, this.transform.position.z);
	}
}
