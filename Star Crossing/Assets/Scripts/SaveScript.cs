using UnityEngine;
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
			//for testing
			relationships[2].setProgress(-4);

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
			loadFile(1);
		}
		catch (FileNotFoundException ex)
		{
			//do nothing as file will always not exist during first run
		}
	}

	//streamwriter to save all these values to file
	public void saveFile(int n)
	{
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Saves/Save"+n+".txt", false);
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

	//streamreader to populate this class once main menu has been created
	public bool loadFile(int n)
	{
		StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save"+n+".txt");
		if(file==null)
				return false;
		for(int i=0; i<characters.Length-1; i++)
		{
			//mood
			string line = file.ReadLine();
			string item1 = line.Substring(0, line.IndexOf(' '));
			characters[i].mood = int.Parse(item1);
			line = line.Remove(0,item1.Length+1);
			Debug.Log("Read mood: "+item1);
			
			//breakout factor
			string item2 = line;
			characters[i].breakoutFactor = item2;
			Debug.Log("Read bf: "+item2);
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
