using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public float turnSpeed = 4.0f;				
	private Vector3 mouseOrigin;
	private bool isRotating;
	private Ray ray;
	private RaycastHit hit;
	private GameObject temporaryObject;
	public Transform[] aud_positions;
	private int positions_counter;
	private float cam_smooth_var;
	StatScript StatScript;
	private int temp_current_run;
	private Light[] Aud_members_arr;

	void Start()
	{
		StatScript = GameObject.Find("Main Camera").GetComponent<StatScript>();
		Aud_members_arr = GameObject.Find ("Audience member positions").GetComponentsInChildren<Light>();
		positions_counter = 0;
		cam_smooth_var = 2.0f;
		temp_current_run = 0;
		gameObject.transform.position = aud_positions[0].position;
	}

	void Update () 
	{
//		if(positions_counter == (aud_positions.Length - 1))
//		{
//			positions_counter = 0;
//			// transform.position = Vector3.Lerp(this.transform.position, aud_positions[positions_counter].position, 0.1f * Time.deltaTime);
//		}
//		else{
//			print ("Pos Counter : " + positions_counter);
//			iTween.MoveTo(gameObject,iTween.Hash("x",aud_positions[positions_counter].position.x,"y",aud_positions[positions_counter].position.y, "Time", 5.0f));
//			if(this.transform.position.x == aud_positions[positions_counter].position.x)
//			{
//				positions_counter++;
//			}
//		}
	
		if (Input.GetMouseButtonDown (1)) {
			temp_current_run = StatScript.current_run + 5;
			iTween.ValueTo(gameObject, iTween.Hash("from", cam_smooth_var, "to", 1.5f, "time", 0.5f, "onupdate", "changecamOsize"));
		}

		if (Input.GetMouseButton (1)) {
			print ("Positions counter : " +  positions_counter);
			if(Aud_members_arr[positions_counter].intensity < 2.0f)
			{
				Aud_members_arr[positions_counter].intensity = StatScript.current_run - temp_current_run + 2;
			}
		}

		if (Input.GetMouseButtonUp (1)) {
			if(Aud_members_arr[positions_counter].intensity <= 2.0f)
			{
				Aud_members_arr[positions_counter].intensity = 0;
			}
			iTween.ValueTo(gameObject, iTween.Hash("from", cam_smooth_var, "to", 2.0f, "time", 0.5f, "onupdate", "changecamOsize"));
		}

		if (Input.GetMouseButton (0)) {
			print ("Pos Counter : " + positions_counter);
			if(positions_counter == (aud_positions.Length))
			{
				positions_counter = 0;
			}
			iTween.MoveTo(gameObject,iTween.Hash("x",aud_positions[positions_counter].position.x,"y",aud_positions[positions_counter].position.y, "Time", 1.5f, "onComplete", "moveCamAud"));
		}

//				if (Input.GetMouseButtonDown (0)) {
//						mouseOrigin = Input.mousePosition;
//						isRotating = true;
//				}
//
//				if (!Input.GetMouseButton (0))
//						isRotating = false;
//				if (isRotating) {
//						Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);
//						transform.Translate (pos*Time.deltaTime * 50);
//				}
//		//ray = camera.ViewportPointToRay(new Vector3(10.0F, 10.0F, 0));
//		//Debug.DrawRay (transform.position, ray);
//		Debug.DrawRay(transform.position, transform.forward * 20, Color.green);
//		if (Physics.Raycast (transform.position, transform.forward, out hit, 20)) {
//						if(hit.collider.gameObject.tag == "Audience")
//			{
//						temporaryObject = hit.collider.gameObject;
//						temporaryObject.light.enabled = true;
//						print ("I'm looking at " + hit.collider.gameObject.name);
//			}
//				} else if (temporaryObject != null) {
//				temporaryObject.light.enabled = false;
//				temporaryObject = null;
//				print("I'm looking at nothing!");
//				
//		}
			
	}

	void changecamOsize(float newValue){
		//apply the value of newValue:
		cam_smooth_var = newValue;
		gameObject.camera.orthographicSize = newValue;
	}

	void moveCamAud() {
		positions_counter++;
	}
}