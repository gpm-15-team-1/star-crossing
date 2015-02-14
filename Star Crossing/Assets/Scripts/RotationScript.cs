using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {
	private GameObject Sphere1;
	private GameObject Sphere2;
	private GameObject PivotObj;
	private float distance;


	// Use this for initialization
	void Awake()
	{
		Sphere1 = GameObject.FindGameObjectWithTag("Sphere1");
		Sphere2 = GameObject.FindGameObjectWithTag("Sphere2");
		PivotObj = GameObject.FindGameObjectWithTag ("Pivot");
	}

	void Start () {
	
	}


	// Update is called once per frame
	void FixedUpdate () {
		distance = Vector3.Distance (Sphere1.transform.position, PivotObj.transform.position);
		if(Input.GetKey(KeyCode.A)){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			PivotObj.transform.Rotate(0,0, -2);
		}
		if(Input.GetKey(KeyCode.D)){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			PivotObj.transform.Rotate(0,0, 2);
		}
		if(Input.GetKey(KeyCode.W)){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			if(distance > 0.5f)
			{
				Sphere1.transform.position = Vector3.MoveTowards(Sphere1.transform.position, PivotObj.transform.position, 0.05f);
				Sphere2.transform.position = Vector3.MoveTowards(Sphere2.transform.position, PivotObj.transform.position, 0.05f);
			}

		}
		if(Input.GetKey(KeyCode.S)){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			if(distance < 2.5f)
			{
				Sphere1.transform.position = Vector3.MoveTowards(Sphere1.transform.position, PivotObj.transform.position, -0.05f);		
				Sphere2.transform.position = Vector3.MoveTowards(Sphere2.transform.position, PivotObj.transform.position, -0.05f);
			}
		}
	}
}