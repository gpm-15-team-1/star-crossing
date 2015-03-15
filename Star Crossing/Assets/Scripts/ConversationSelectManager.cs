using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConversationSelectManager : MonoBehaviour {
	
	Relationship[] relationships;
	public Text[] toShow;
	public Button[] buttons;

	enum Scene {Morning, MorningTalks, Feedback, Relationship1, Relationship2};

	Color basic = new Color(0.20f, 0.20f, 0.20f);
	Color selected = new Color(0.50f, 0.50f, 0.50f);

	string chosen;

	// Use this for initialization
	void Start () {
		buttons[0].GetComponent<ConversationButtonScript>().index = 1;
		buttons[1].GetComponent<ConversationButtonScript>().index = 2;
		buttons[2].GetComponent<ConversationButtonScript>().index = 3;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shuffle(string[] args)
	{
		List <int> availableEntries = new List<int>();
		availableEntries = Enumerable.Range(1, 3).ToList();
		
		int firstVal = availableEntries[Random.Range(0, availableEntries.Count)];
		availableEntries.Remove(firstVal);
		
		int secondVal = availableEntries[Random.Range(0, availableEntries.Count)];
		availableEntries.Remove(secondVal);

		int thirdVal = availableEntries[Random.Range(0, availableEntries.Count)];
		availableEntries.Remove(thirdVal);

		availableEntries.Add(firstVal);
		availableEntries.Add(secondVal);
		availableEntries.Add(thirdVal);

		Debug.Log("shuffled size: "+availableEntries.Count);

		for(int i=0; i<3; i++)
		{
			int toSet = availableEntries[i]-1; //i-1's value -1
			toShow[i].text = args[toSet]; 
			Debug.Log(":" +toSet +": "+ args[toSet]);
			buttons[i].GetComponent<ConversationButtonScript>().index = toSet+1;
		}
	}

	public void Display(string[] args)
	{
		for(int i=0; i<args.Length; i++)
		{
			toShow[i].text = args[i];
		}
	}

	public void disable()
	{
		Debug.Log("Conversations disabled.");
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
		Debug.Log("Conversations enabled.");
		for(int i=0; i<toShow.Length; i++)
		{
			toShow[i].enabled = true;
			//buttons[i].enabled = true;
			buttons[i].gameObject.SetActive(true);
			buttons[i].GetComponent<Button>().interactable = true;
		}
	}
}
