using UnityEngine;
using System.Collections;

public class RelationshipButtonScript : MonoBehaviour {

	public GUIText myText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = myText.text;
		Relationship[] tempRelationships = new Relationship[10];

		for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().relationships.Length; i++)
		    tempRelationships[i] = GameObject.Find("Save").GetComponent<SaveScript>().relationships[i];

		for(int i=0; i<tempRelationships.Length; i++)
		{
			if(myText.text.Equals(tempRelationships[i].getName()))
			{
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
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readFreeRelationship();
					break;
				}
			}
		}

		GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
	}
}
