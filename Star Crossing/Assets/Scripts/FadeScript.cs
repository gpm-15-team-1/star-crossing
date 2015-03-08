using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		//anim.Play ("FadeIn");
		//this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void fadeIn()
	{
		Debug.Log("FadeIn");
		anim.Play("FadeIn");;

		//this.enabled = false;
		/*this.enabled = true;
		this.enabled = false;*/
	}

	public void fadeOut()
	{
		Debug.Log("FadeOut");
		anim.Play("FadeOut");
		/*this.enabled = true;*/
	}
}
