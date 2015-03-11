using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

	Animator anim;
	float temp = 1.0f;
	float fadeTime = 2.0f;
	CanvasGroup cv;
	// Use this for initialization
	void Start () {
		//anim = this.GetComponent<Animator>();
		//anim.Play ("FadeIn");
		//this.gameObject.SetActive(false);
		cv = this.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void updateFromValue (float newValue)
	{
		cv.alpha = newValue;
	}

	public IEnumerator fadeIn()
	{
		Debug.Log("FadeIn");
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1, "to", 0, "onupdatetarget", gameObject, "onupdate", "updateFromValue", "time", fadeTime, "easetype", iTween.EaseType.easeOutExpo));
		yield return new WaitForSeconds(0.5f);
		//anim.Play("FadeIn");
		//this.enabled = false;
		/*this.enabled = true;
		this.enabled = false;*/
	}

	public IEnumerator fadeOut()
	{
		Debug.Log("FadeOut");
		iTween.ValueTo (gameObject, iTween.Hash ("from", 0, "to", 1, "onupdatetarget", gameObject, "onupdate", "updateFromValue", "time", fadeTime, "easetype", iTween.EaseType.easeInExpo));
		yield return new WaitForSeconds(0.5f);
		//anim.Play("FadeOut");
		/*this.enabled = true;*/
	}
}
