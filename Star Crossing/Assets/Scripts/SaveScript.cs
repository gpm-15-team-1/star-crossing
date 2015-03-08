﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SaveScript : MonoBehaviour {

	//current week, day, scene
	public int currentWeek;
	public int currentDay;
	public int currentScene;

	//relationship values
	public Relationship[] relationships;
	enum Chars {Randall, Julie, Tani, Nikolai, Carol, Rusty};
	enum Scene {Morning, Feedback, Relationship1, Relationship2};
	public Character[] characters;
	public List<int> shuffled;

	// Use this for initialization
	void Start () {
		currentWeek = 1;
		currentDay = 1;
		currentScene = 0;

		declare ();
	}

	void declare()
	{
		relationships = new Relationship[10];
		for(int i=0; i<10; i++)
			relationships[i] = new Relationship();
		{
			relationships[0].setName("Randall/Julie");
			relationships[0].setChar1Value(1);
			relationships[0].setChar2Value(1);

			relationships[1].setName("Randall/Tani");
			relationships[1].setChar1Value(1);
			relationships[1].setChar2Value(1);

			relationships[2].setName("Randall/Nikolai");
			relationships[2].setChar1Value(1);
			relationships[2].setChar2Value(0);

			relationships[3].setName("Randall/Carol");
			relationships[3].setChar1Value(1);
			relationships[3].setChar2Value(1);

			relationships[4].setName("Julie/Tani");
			relationships[4].setChar1Value(0);
			relationships[4].setChar2Value(1);

			relationships[5].setName("Julie/Nikolai");
			relationships[5].setChar1Value(1);
			relationships[5].setChar2Value(1);

			relationships[6].setName("Julie/Carol");
			relationships[6].setChar1Value(1);
			relationships[6].setChar2Value(1);

			relationships[7].setName("Nikolai/Tani");
			relationships[7].setChar1Value(1);
			relationships[7].setChar2Value(1);

			relationships[8].setName("Nikolai/Carol");
			relationships[8].setChar1Value(1);
			relationships[8].setChar2Value(1);

			relationships[9].setName("Tani/Carol");
			relationships[9].setChar1Value(1);
			relationships[9].setChar2Value(1);
		}

		{
			characters[(int)Chars.Randall].mood = 0;
			characters[(int)Chars.Julie].mood = 0;
			characters[(int)Chars.Tani].mood = 0;
			characters[(int)Chars.Nikolai].mood = 0;
			characters[(int)Chars.Carol].mood = 0;
			characters[(int)Chars.Rusty].mood = 0;

			characters[(int)Chars.Randall].breakoutFactor = "0";
			characters[(int)Chars.Julie].breakoutFactor = "0";
			characters[(int)Chars.Tani].breakoutFactor = "0";
			characters[(int)Chars.Nikolai].breakoutFactor = "0";
			characters[(int)Chars.Carol].breakoutFactor = "0";
			characters[(int)Chars.Rusty].breakoutFactor = "0";
			/*characters[4].name = "Randall";
			characters[4].lastName = "Creed";
			//assign part and breakout factor
			characters[4].mood = 0;*/
		}

		try
		{
			//argument can be replaced with player's chosen save file
			loadFile ();
		}
		catch (FileNotFoundException ex)
		{
			//do nothing as file will always not exist during first run
		}
		//relationships[2].setProgress(-4);

		if(currentScene==(int)Scene.Relationship2)
		{
			loadChosen();
			//Debug.Log(GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().getChosen());
		}
	}

	//streamwriter to save all these values to file
	public void saveFile(int n)
	{
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Saves/Save"+n+".txt", false);
		file.WriteLine (currentWeek + " " + currentDay + " " + currentScene);
		for(int i=0; i<characters.Length-1; i++)
		{
			//mood, breakout factor
			file.WriteLine(characters[i].mood +" "+characters[i].breakoutFactor);
		}
		for(int i=0; i<relationships.Length; i++)
		{
			//char1value, char2value, progress
			file.WriteLine(relationships[i].getChar1Value() +" "+relationships[i].getChar2Value() +" "+relationships[i].getProgress());
		}
		file.Close();
	}

	//streamwriter for temp file
	public void saveFile()
	{
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Saves/Temp.txt", false);
		file.WriteLine (currentWeek + " " + currentDay + " " + currentScene);
		for(int i=0; i<characters.Length-1; i++)
		{
			//mood, breakout factor
			file.WriteLine(characters[i].mood +" "+characters[i].breakoutFactor);
		}
		for(int i=0; i<relationships.Length; i++)
		{
			//char1value, char2value, progress
			file.WriteLine(relationships[i].getChar1Value() +" "+relationships[i].getChar2Value() +" "+relationships[i].getProgress());
		}
		file.Close();
	}

	//streamreader to populate temp file
	public bool loadFile()
	{
		StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Temp.txt");
		if(file==null)
			return false;
		string state = file.ReadLine ();
		
		string cWeek = state.Substring(0, state.IndexOf(' '));
		currentWeek = int.Parse (cWeek);
		state = state.Remove(0,cWeek.Length+1);
		
		string cDay = state.Substring(0, state.IndexOf(' '));
		currentDay = int.Parse (cDay);
		state = state.Remove(0,cDay.Length+1);
		
		string cScene = state;
		currentScene = int.Parse (cScene);
		
		Debug.Log (currentWeek + "+" + currentDay + "+" + currentScene);
		for(int i=0; i<characters.Length-1; i++)
		{
			//mood
			string line = file.ReadLine();
			string item1 = line.Substring(0, line.IndexOf(' '));
			characters[i].mood = int.Parse(item1);
			line = line.Remove(0,item1.Length+1);
			//Debug.Log("Read mood: "+item1);
			
			//breakout factor
			string item2 = line;
			characters[i].breakoutFactor = item2;
			//Debug.Log("Read bf: "+item2);
		}
		for(int i=0; i<relationships.Length; i++)
		{
			//char1value
			string line = file.ReadLine();
			string item1 = line.Substring(0, line.IndexOf(' '));
			relationships[i].setChar1Value(int.Parse(item1));
			line = line.Remove(0,item1.Length+1);
			
			//char2value
			string item2 = line.Substring(0, line.IndexOf(' '));
			relationships[i].setChar2Value(int.Parse(item2));
			line = line.Remove(0,item2.Length+1);
			
			string item3 = line;
			relationships[i].setProgress(int.Parse(item3));
		}
		file.Close();
		
		// If the current scene is the char select scene, tell the accuracies that it's okay to calculate!
		if (Application.loadedLevelName == "CharSelectScreen") {
			GameObject[] accuracies = GameObject.FindGameObjectsWithTag("MenuAccuracies");
			foreach (GameObject accuracy in accuracies) {
				accuracy.GetComponent<CharMenuAccuracyScript>().CalculateAccuracy();
			}
		}
		return true;
	}

	//streamreader to populate this class once main menu has been created
	public bool loadFile(int n)
	{
		StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save"+n+".txt");
		if(file==null)
				return false;
		string state = file.ReadLine ();

		string cWeek = state.Substring(0, state.IndexOf(' '));
		currentWeek = int.Parse (cWeek);
		state = state.Remove(0,cWeek.Length+1);

		string cDay = state.Substring(0, state.IndexOf(' '));
		currentDay = int.Parse (cDay);
		state = state.Remove(0,cDay.Length+1);

		string cScene = state;
		currentScene = int.Parse (cScene);

		Debug.Log (currentWeek + "+" + currentDay + "+" + currentScene);
		for(int i=0; i<characters.Length-1; i++)
		{
			//mood
			string line = file.ReadLine();
			string item1 = line.Substring(0, line.IndexOf(' '));
			characters[i].mood = int.Parse(item1);
			line = line.Remove(0,item1.Length+1);
			//Debug.Log("Read mood: "+item1);
			
			//breakout factor
			string item2 = line;
			characters[i].breakoutFactor = item2;
			//Debug.Log("Read bf: "+item2);
		}
		for(int i=0; i<relationships.Length; i++)
		{
			//char1value
			string line = file.ReadLine();
			string item1 = line.Substring(0, line.IndexOf(' '));
			relationships[i].setChar1Value(int.Parse(item1));
			line = line.Remove(0,item1.Length+1);
			
			//char2value
			string item2 = line.Substring(0, line.IndexOf(' '));
			relationships[i].setChar2Value(int.Parse(item2));
			line = line.Remove(0,item2.Length+1);

			string item3 = line;
			relationships[i].setProgress(int.Parse(item3));
		}
		file.Close();
		
		// If the current scene is the char select scene, tell the accuracies that it's okay to calculate!
		if (Application.loadedLevelName == "CharSelectScreen") {
			GameObject[] accuracies = GameObject.FindGameObjectsWithTag("MenuAccuracies");
			foreach (GameObject accuracy in accuracies) {
				accuracy.GetComponent<CharMenuAccuracyScript>().CalculateAccuracy();
			}
		}
		return true;
	}

	public void saveChosen()
	{
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Saves/Chosen.txt", false);
		file.WriteLine(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen);
		Debug.Log(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen +" saved.");
		for(int i=0; i<shuffled.Count; i++)
		{
			file.WriteLine(shuffled[i]);
		}
		file.Close();
	}

	public void loadChosen()
	{
		StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Chosen.txt");
		GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen = file.ReadLine();
		Debug.Log(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().chosen +" loaded.");
		for(int i=0; i<5; i++)
		{
			shuffled.Add(int.Parse(file.ReadLine()));
		}
		file.Close();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public int getIndexByName(string name)
	{
		for(int i=0; i<relationships.Length; i++)
		{
			if(relationships[i].getName().Equals(name))
			   return i;
		}
		return -1;
	}

	public Relationship getRelationshipByName(string name)
	{
		for(int i=0; i<relationships.Length; i++)
		{
			if(relationships[i].getName().Equals(name))
				return relationships[i];
		}
		return null;
	}
}
