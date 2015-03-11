using UnityEngine;
using System.Collections;

public class MoveScript2 : MonoBehaviour {
	private GameObject MovingEntities2;
	private Vector3 temp;
	// Use this for initialization
	void Awake () {
		MovingEntities2 = GameObject.FindGameObjectWithTag ("MovingEntities2");
		temp.z = 240;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MovingEntities2.transform.Translate (Vector3.back * 30 * Time.deltaTime);
		if (MovingEntities2.transform.position.z <= - 575) {
			MovingEntities2.transform.position= temp;
			temp.z = 575;
			MovingEntities2.transform.position = temp;
		}
	}
}
