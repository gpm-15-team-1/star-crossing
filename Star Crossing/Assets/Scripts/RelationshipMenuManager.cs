using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RelationshipMenuManager : MonoBehaviour {

	Relationship[] relationships;
	public Text[] toShow;
	public Button[] buttons;
	List<int> shuffled;

	string chosen;

	// Use this for initialization
	void Start () {
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
		GameObject.Find("Save").GetComponent<SaveScript>().saveChosen();
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

		chosen = GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen;

		if(chosen!=null)
		{
			for(int i=0; i<shuffled.Count; i++)
			{
				if(chosen.Equals(GameObject.Find("Save").GetComponent<SaveScript>().relationships[shuffled[i]].getName()))
				{
					toShow[i].material.color = Color.gray;
				}
			}
		}
	}

	public string getChosen()
	{
		return chosen;
	}

	public void disable()
	{
		Debug.Log("Relationships disabled.");
		for(int i=0; i<toShow.Length; i++)
		{
			toShow[i].enabled = false;
			buttons[i].enabled = false;
			buttons[i].gameObject.SetActive(false);
		}
	}

	public void enable()
	{
		for(int i=0; i<toShow.Length; i++)
		{
			toShow[i].enabled = true;
			buttons[i].enabled = true;
			buttons[i].gameObject.SetActive(true);
		}
	}
}
