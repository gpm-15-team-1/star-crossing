using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RelationshipMenuManager : MonoBehaviour {

	Relationship[] relationships;
	public Text[] toShow;
	public Button[] buttons;
	List<int> shuffled;

	Color basic = new Color(0.20f, 0.20f, 0.20f);
	Color selected = new Color(0.50f, 0.50f, 0.50f);

	string chosen;

	// Use this for initialization
	void Start () {
		
		setChosen(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shuffle()
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
			toShow[i].text = GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName ();
		}
	}

	public void Display()
	{
		shuffled = new List<int>();
		for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().shuffled.Count; i++)
		{
			Debug.Log("Shuffled: "+GameObject.Find("Save").GetComponent<SaveScript>().shuffled[i]);
			shuffled.Add(GameObject.Find("Save").GetComponent<SaveScript>().shuffled[i]);
		}

		for(int i=0; i<shuffled.Count; i++)
		{
			toShow[i].text = GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName();
		}

		if(chosen!=null)
		{
			for(int i=0; i<shuffled.Count; i++)
			{
				if(chosen.Equals(GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName()))
				{
					//Debug.Log("Chosen disabled");
					buttons[i].enabled = false;
					buttons[i].gameObject.SetActive(false);
				}
			}
		}
	}

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
		Debug.Log("Relationships disabled.");
		for(int i=0; i<toShow.Length; i++)
		{
			toShow[i].enabled = false;
			//buttons[i].enabled = false;
			buttons[i].gameObject.SetActive(false);
			buttons[i].GetComponent<Button>().interactable = false;
		}
	}

	public void enable()
	{
		setChosen(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen);
		for(int i=0; i<toShow.Length; i++)
		{
			toShow[i].enabled = true;
			toShow[i].color = basic;
			//buttons[i].enabled = true;
			buttons[i].gameObject.SetActive(true);
			buttons[i].GetComponent<Button>().interactable = true;
			if(toShow[i].text.Equals(chosen))
			{
				//buttons[i].enabled = false;
				toShow[i].color = selected;
				buttons[i].GetComponent<Button>().interactable = false;
				//buttons[i].gameObject.SetActive(false);
			}
		}
	}
}
