using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationButtonScript : MonoBehaviour {

	public Text myText;
	public AudioClip blocked;
	public AudioClip click;

	public int index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseUp()
	{
		if(!this.GetComponent<Button>().enabled)
		{
			audio.clip = blocked;
			audio.PlayOneShot(blocked);
		}
		else
		{
			audio.clip = click;
			audio.PlayOneShot(click);
		}

		GameObject.Find("DialogueManager").GetComponent<DialogueManager>().pause = false;

		if(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().topic!=null)
			GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readTopicConversation2(index);
		else
			GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readConversation(index);

		GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();
	}
}