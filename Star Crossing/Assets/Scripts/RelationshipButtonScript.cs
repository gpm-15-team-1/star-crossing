﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RelationshipButtonScript : MonoBehaviour {

	public Text myText;
	public AudioClip blocked;
	public AudioClip click;
	string char1;
	string char2;

	// Use this for initialization
	void Start () {
		char1=null;
		char2=null;
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

		GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = myText.text;
		GameObject.Find("Save").GetComponent<SaveScript>().saveChosen();
		Relationship[] tempRelationships = new Relationship[10];

		for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().relationships.Length; i++)
		    tempRelationships[i] = GameObject.Find("Save").GetComponent<SaveScript>().relationships[i];

		for(int i=0; i<tempRelationships.Length; i++)
		{
			if(myText.text.Equals(tempRelationships[i].getName()))
			{
				GameObject.Find("DialogueManager").GetComponent<DialogueManager>().pause = false;
				//metrics for relationship progression
				switch(tempRelationships[i].getProgress())
				{
				case -12:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readScriptedRelationship(-3);
					break;
				case -8:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readScriptedRelationship(-2);
					break;
				case -4:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readScriptedRelationship(-1);
					break;
				case 4:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readScriptedRelationship(1);
					break;
				case 8:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readScriptedRelationship(2);
					break;
				case 12:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readScriptedRelationship(3);
					break;
				default:
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readFreeRelationship(1);
					break;
				}
			}
		}

		GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
	}
}