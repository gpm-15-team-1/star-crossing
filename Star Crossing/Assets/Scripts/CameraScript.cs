using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public float turnSpeed = 4.0f;				
	private Vector3 mouseOrigin;
	private bool isRotating;
	private Ray ray;
	private RaycastHit hit;


	void Update () 
	{
				if (Input.GetMouseButtonDown (0)) {
						mouseOrigin = Input.mousePosition;
						isRotating = true;
				}

				if (!Input.GetMouseButton (0))
						isRotating = false;
				if (isRotating) {
						Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);
						transform.Translate (pos*Time.deltaTime * 50);
				}
		//ray = camera.ViewportPointToRay(new Vector3(10.0F, 10.0F, 0));
		//Debug.DrawRay (transform.position, ray);
		Debug.DrawRay(transform.position, transform.forward * 20, Color.green);
		if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
			print("I'm looking at " + hit.transform.name);
		else
			print("I'm looking at nothing!");
	}
}