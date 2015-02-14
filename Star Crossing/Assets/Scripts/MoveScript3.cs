using UnityEngine;
using System.Collections;

public class MoveScript3 : MonoBehaviour {
	private GameObject MovingEntities3;
	private Vector3 temp;
	// Use this for initialization
	void Awake () {
		MovingEntities3 = GameObject.FindGameObjectWithTag ("MovingEntities3");
	}
	
	// Update is called once per frame
	void Update () {
		MovingEntities3.transform.Translate (Vector3.back * 30 * Time.deltaTime);
		if (MovingEntities3.transform.position.z <= - 575) {
			MovingEntities3.transform.position= temp;
			temp.z = 575;
			MovingEntities3.transform.position = temp;	
		}
	}
}
