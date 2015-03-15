using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {
	private GameObject Sphere1;
	private GameObject Sphere2;
	private GameObject PivotObj;
	private float distance;
	public bool canPress;


	// Use this for initialization
	void Awake()
	{
		Sphere1 = GameObject.FindGameObjectWithTag("Sphere1");
		Sphere2 = GameObject.FindGameObjectWithTag("Sphere2");
		PivotObj = GameObject.FindGameObjectWithTag ("Pivot");
	}

	void Start () {
		canPress = true;
	}


	// Update is called once per frame
	void FixedUpdate () {
		distance = Vector3.Distance (Sphere1.transform.position, PivotObj.transform.position);
		if(Input.GetKey(KeyCode.A) && canPress == true){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			iTween.RotateBy(gameObject, iTween.Hash("z", -0.125f, "time", 0.35f, "easetype", "spring", "onUpdate", "disable", "onComplete", "enable"));
			//PivotObj.transform.Rotate(0,0, -2);
		}
		if(Input.GetKey(KeyCode.D) && canPress == true){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			//iTween.RotateBy(gameObject, new Vector3(0.0f, 0.0f, 0.0416666666666667f), 0.5f);
			iTween.RotateBy(gameObject, iTween.Hash("z", 0.125f, "time", 0.35f, "easetype", "spring", "onUpdate", "disable", "onComplete", "enable"));
			//PivotObj.transform.Rotate(0,0, 2);
		}
		if(Input.GetKey(KeyCode.S)){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * mindeltaRotation);
			if(distance > 0.5f)
			{
				//iTween.MoveTo(Sphere1, iTween.Hash("speed", 0.5f, "position", PivotObj.transform.position, "easetype", "spring"));
				//iTween.MoveTo(Sphere2, iTween.Hash("x", 0.5f, "y", 0.5f, "position", PivotObj.transform.position, "time", 0.5f, "easetype", "spring"));
				Sphere1.transform.position = Vector3.MoveTowards(Sphere1.transform.position, PivotObj.transform.position, 0.05f);
				Sphere2.transform.position = Vector3.MoveTowards(Sphere2.transform.position, PivotObj.transform.position, 0.05f);
			}

		}
		if(Input.GetKey(KeyCode.W)){
			//Sphere1.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			//Sphere2.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
			if(distance < 2.75f)
			{
				//iTween.MoveTo(Sphere1, iTween.Hash("x", -0.5f, "y", -0.5f, "position", PivotObj.transform.position, "time", 0.5f, "easetype", "spring"));
				//iTween.MoveTo(Sphere2, iTween.Hash("x", -0.5f, "y", -0.5f, "position", PivotObj.transform.position, "time", 0.5f, "easetype", "spring"));
				Sphere1.transform.position = Vector3.MoveTowards(Sphere1.transform.position, PivotObj.transform.position, -0.05f);		
				Sphere2.transform.position = Vector3.MoveTowards(Sphere2.transform.position, PivotObj.transform.position, -0.05f);
			}
		}
	}

	void disable(){
		canPress = false;
	}

	void enable() {
		canPress = true;
	}
}