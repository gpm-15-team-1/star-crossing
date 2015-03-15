using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RelationshipMenuManager : MonoBehaviour {

	public Image bg;
	Relationship[] relationships;
	public Text[] toShow;
	public Button[] buttons;
	public Button[] relationshipButtons;
	public Text[] relationshipText;
	List<int> shuffled;

	enum Scene {Morning, MorningTalks, Feedback, Relationship1, Relationship2};

	Color basic = new Color(1.0f, 1.0f, 1.0f);
	Color selected = new Color(0.75f, 0.75f, 0.25f);
	Color disabled = new Color(0.50f, 0.50f, 0.50f);

	string chosen;
	string topic;
	string char1;
	int pair1=-1;
	string char2;
	int pair2=-1;

	// Use this for initialization
	void Start () {

		char1 = null;
		char2 = null;

		//only do this if on Relationship2
		if(GameObject.Find("Save").GetComponent<SaveScript>().currentScene == (int)Scene.Relationship2)
		{
			setChosen(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen);
			string temp = chosen;
			char1 = chosen.Substring(0, chosen.IndexOf("/"));
			chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
			char2 = chosen;
			chosen = temp;
		}
	}

	public void SelectConversation(int index)
	{
		if(index==1)
		{
			string tchar1 = null;
			string tchar2 = null;
			if(GameObject.Find("Save").GetComponent<SaveScript>().currentScene == (int)Scene.Relationship2)
			{
				Debug.Log("chosen: "+chosen);
				string temp = chosen;
				tchar1 = chosen.Substring(0, chosen.IndexOf("/"));
				chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
				tchar2 = chosen;
				chosen = temp;
			}
			
			//only do this if both char 1 and char 2 have been set and the relationship was not already choen
			if((char1 != null && char2 != null) && (!char1.Equals(tchar1) && !char2.Equals(tchar2)))
			{
				string myText1 = char1 +"/" +char2;
				string myText2 = char2 +"/" +char1;
				Relationship[] tempRelationships = new Relationship[10];
				
				for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().relationships.Length; i++)
					tempRelationships[i] = GameObject.Find("Save").GetComponent<SaveScript>().relationships[i];
				
				int c = -1;
				for(int i=0; i<tempRelationships.Length; i++)
				{
					if(myText1.Equals(tempRelationships[i].getName()))
					{
						c=i;
						Debug.Log(myText1);
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = myText1;
						GameObject.Find("Save").GetComponent<SaveScript>().saveChosen();
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().pause = false;

						//metrics for relationship progression
						switch(tempRelationships[c].getProgress())
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
					else if(myText2.Equals(tempRelationships[i].getName()))
					{
						c=i;
						Debug.Log(myText2);
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = myText2;
						GameObject.Find("Save").GetComponent<SaveScript>().saveChosen();
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().pause = false;

						//metrics for relationship progression
						switch(tempRelationships[c].getProgress())
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
		//topic conversation
		else if (index==2)
		{
			string tchar1 = null;
			string tchar2 = null;
			if(GameObject.Find("Save").GetComponent<SaveScript>().currentScene == (int)Scene.Relationship2)
			{
				Debug.Log("topic: "+topic);
				string temp = chosen;
				tchar1 = chosen.Substring(0, chosen.IndexOf("/"));
				chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
				tchar2 = chosen;
				chosen = temp;
			}
			
			//only do this if both char 1 and char 2 have been set and the relationship was not already choen
			if((char1 != null && char2 != null) && (!char1.Equals(tchar1) && !char2.Equals(tchar2)))
			{
				string myText1 = char1 +"/" +char2;
				string myText2 = char2 +"/" +char1;
				Relationship[] tempRelationships = new Relationship[10];
				
				for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().relationships.Length; i++)
					tempRelationships[i] = GameObject.Find("Save").GetComponent<SaveScript>().relationships[i];
				
				int c = -1;
				for(int i=0; i<tempRelationships.Length; i++)
				{
					if(myText1.Equals(tempRelationships[i].getName()))
					{
						c=i;
						Debug.Log("Topic: "+relationshipText[index-1].text);
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = myText1;
						GameObject.Find("Save").GetComponent<SaveScript>().saveChosen();
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().pause = false;

						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readTopicConversation1(relationshipText[index-1].text);
					}
					else if(myText2.Equals(tempRelationships[i].getName()))
					{
						c=i;
						Debug.Log("Topic: "+relationshipText[index-1].text);
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = myText1;
						GameObject.Find("Save").GetComponent<SaveScript>().saveChosen();
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().pause = false;
						
						GameObject.Find("DialogueManager").GetComponent<DialogueManager>().readTopicConversation1(relationshipText[index-1].text);
					}
				}
			}
			
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();

		}
	}

	public void CharacterOnClick(int index)
	{
		//Debug.Log("<<<<<>>>>>");
		//first 5 buttons
		if(index <5)
		{
			for(int i=0; i<5; i++)
			{
				//if i is the current button
				if(i == index)
				{
					char1 = toShow[i].text;
					string text = "";
					text = char1 +"/"+char2;
					
					relationshipText[0].text = text;
					//check the index of the disabled button
					if(pair2!=(i+5) && pair2!=-1)
					{
						//Debug.Log("To enable: "+pair2);
						//if it is no longer my counterpart, enable it again
						//also reset its index
						buttons[pair2].enabled = true;
						buttons[pair2].GetComponent<Image>().color = basic;
						pair2=-1;
					}

					//Debug.Log("Selected: "+i);
					//Debug.Log("To disable: "+(i+5));
					//disable my counterpart
					buttons[i+5].enabled = false;
					buttons[i+5].GetComponent<Image>().color = disabled;
					//buttons[i+5].gameObject.SetActive(false);
					//save the index of the button I just disabled
					//save my index
					pair1=i;
					pair2=i+5;

					//buttons[pair1].GetComponent<Image>().color = selected;
				}
				else
				{
					buttons[i].GetComponent<Image>().color = basic;
				}
			}
		}
		else
		{
			//last 5 buttons
			for(int i=5; i<10; i++)
			{
				//if i is the current button
				if(i == index)
				{
					char2 = toShow[i].text;
					string text = "";
					text = char1 +"/"+char2;
					
					relationshipText[0].text = text;
					//check the index of the disabled button
					if(pair1!=(i-5) && pair1!=-1)
					{
						//Debug.Log("To enable: "+pair1);
						//if it is no longer my counterpart, enable it again
						//also reset its index
						buttons[pair1].enabled = true;
						buttons[pair1].GetComponent<Image>().color = basic;
						//buttons[pair1].gameObject.SetActive(true);
						pair1=-1;
					}
					
					//Debug.Log("Selected: "+i);
					//Debug.Log("To disable: "+(i-5));
					//disable my counterpart
					buttons[i-5].enabled = false;
					buttons[i-5].GetComponent<Image>().color = disabled;
					//save the index of the button I just disabled
					//save my index
					pair2=i;
					pair1=i-5;

					//buttons[pair2].GetComponent<Image>().color = selected;
				}
				else
				{
					buttons[i].GetComponent<Image>().color = basic;
				}
			}
		}
	}

	/*public void Shuffle()
	{
		shuffled = new List<int>();
		while(shuffled.Count<5)
		{
			int temp = Random.Range(0,9);
			if(!shuffled.Contains(temp))
				shuffled.Add(temp);
		}

		for(int i=0; i<shuffled.Count; i++)
		{
			GameObject.Find("Save").GetComponent<SaveScript>().shuffled.Add(shuffled[i]);
			//toShow[i].text = GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName ();
		}
	}*/

	/*public void Display()
	{
		/*shuffled = new List<int>();
		for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().shuffled.Count; i++)
		{
			Debug.Log("Shuffled: "+GameObject.Find("Save").GetComponent<SaveScript>().shuffled[i]);
			shuffled.Add(GameObject.Find("Save").GetComponent<SaveScript>().shuffled[i]);
		}*/

		/*for(int i=0; i<shuffled.Count; i++)
		{
			toShow[i].text = GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName();
		}*/

		/*if(chosen!=null)
		{
			string tchar1 = chosen.Substring(0, chosen.IndexOf("/"));
			chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
			string tchar2 = chosen;

			for(int i=0; i<buttons.Length; i++)
			{
				if(chosen.Equals(GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName()))
				{
					//Debug.Log("Chosen disabled");
					buttons[i].enabled = false;
					buttons[i].gameObject.SetActive(false);
				}
			}
		}*//*
	}*/

	public string getChosen()
	{
		return chosen;
	}

	public void setChosen(string c)
	{
		chosen = c;
	}

	public void disable()
	{
		bg.gameObject.SetActive(false);
		Debug.Log("Relationships disabled.");
		for(int i=0; i<relationshipButtons.Length; i++)
		{
			relationshipButtons[i].gameObject.SetActive(false);
			relationshipButtons[i].GetComponent<Button>().interactable = false;
			relationshipText[i].enabled = false;
		}
		for(int i=0; i<buttons.Length; i++)
		{
			//toShow[i].enabled = false;
			//buttons[i].enabled = false;
			buttons[i].gameObject.SetActive(false);
			buttons[i].GetComponent<Button>().interactable = false;
		}
	}

	public void enable()
	{
		bg.gameObject.SetActive(true);
		for(int i=0; i<relationshipButtons.Length; i++)
		{
			relationshipButtons[i].gameObject.SetActive(true);
			relationshipButtons[i].GetComponent<Button>().interactable = true;
			relationshipText[i].enabled = true;
		}
		
		//setChosen(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen);
		for(int i=0; i<buttons.Length; i++)
		{
			//toShow[i].enabled = true;
			//toShow[i].color = basic;
			//buttons[i].enabled = true;
			buttons[i].gameObject.SetActive(true);
			buttons[i].GetComponent<Button>().interactable = true;
			/*if(toShow[i].text.Equals(chosen))
			{
				//buttons[i].enabled = false;
				//toShow[i].color = selected;
				buttons[i].GetComponent<Button>().interactable = false;
				//buttons[i].gameObject.SetActive(false);
			}*/
		}

		if(GameObject.Find("Save").GetComponent<SaveScript>().currentDay!=2)
		{
			relationshipButtons[1].gameObject.SetActive(false);
			relationshipButtons[1].GetComponent<Button>().interactable = false;
			relationshipText[1].enabled = false;
		}
	}
}
