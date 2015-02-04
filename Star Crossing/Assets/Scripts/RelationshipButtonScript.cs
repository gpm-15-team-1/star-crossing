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
				if(tempRelationships[i].getProgress()==10 || tempRelationships[i].getProgress()==20 || tempRelationships[i].getProgress()==30)
				{
					Debug.Log("scripted");
					//it is a scripted conversation
				}
				else
					GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readRelationship("free");

			}
		}

		GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
	}
}
