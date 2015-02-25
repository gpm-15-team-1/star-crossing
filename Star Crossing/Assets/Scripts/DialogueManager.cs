using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	//enums to help organise characters, positions, and scene state
	enum Chars {Randall, Julie, Tani, Nikolai, Carol, Rusty};
	enum Scene {Morning, Feedback, Relationship1, Relationship2};

	//characters and positions referenced by enums
	public Character[] characters;
	Vector2[] positions;

	//arrays to hold dialogue and speaker names for each dialogue
	//eventually make dynamic
	ArrayList dialogue = new ArrayList();
	ArrayList speaker = new ArrayList();
	List<List<string>> actions = new List<List<string>>();

	//text objects to hold name of character and spoken dialogue
	public Text name;
	public Text line;

	//current dialogue
	int currentIndex;

	//temp currents
	int currentWeek;
	int currentDay;
	int currentScene;

	//relationship
	public string chosen = null;

	//"pause" space action
	public bool pause = false;

	// Use this for initialization
	void Start () {

		for(int i=0; i<characters.Length; i++)
			characters[i].mood = GameObject.Find("Save").GetComponent<SaveScript>().characters[i].mood;

		positions = new Vector2[4];
		positions[0] = new Vector2(-4, 0);
		positions[1] = new Vector2(-2, 0);
		positions[2] = new Vector2(2, 0);
		positions[3] = new Vector2(4, 0);

		currentWeek = GameObject.Find("Save").GetComponent<SaveScript>().currentWeek;
		currentDay = GameObject.Find("Save").GetComponent<SaveScript>().currentDay;
		currentScene = GameObject.Find("Save").GetComponent<SaveScript>().currentScene;

		//IF MORNING SCENE DO THIS
		if(currentScene == (int)Scene.Morning)
		{
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
			//reading from file:
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Wk" +currentWeek+"_Day"+currentDay+"_Morning.txt");
			int size = int.Parse(file.ReadLine());

			//read dialogues from file
			for(int i=0; i<size; i++)
			{
				//name
				string line1 = file.ReadLine();
				string item1 = line1.Substring(0, line1.IndexOf(' '));
				line1 = line1.Remove(0,item1.Length+1);
				speaker.Add(item1);

				//dialogue
				string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
				dialogue.Add(item2);                             
			}
			file.Close();

			file = new StreamReader(Application.dataPath + "/Resources/Files/Wk" +currentWeek+"_Day"+currentDay+"_Action.txt");
			size = int.Parse(file.ReadLine());
			//skip all irrelevant lines
			for(int i=0; i<size; i++)
			{
				List<string> currentActions = new List<string>();
				string[] actionLine = file.ReadLine().Split();
				for(int j=0; j<actionLine.Length; j++)
				{
					currentActions.Add(actionLine[j]);
				}
				actions.Add(currentActions);
			}

			file.Close();
		}
		//IF FEEDBACK SCENE DO THIS
		else if(currentScene == (int)Scene.Feedback)
		{
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Wk" +currentWeek+"_Day"+currentDay+"_Feedback.txt");
			//int score = int.Parse(file.ReadLine());
			//Debug.Log("Score: "+score);

			int n = characters.Length-2;
			string[] party = new string[n];
			int[] accuracy = new int[n];

			for(int i=0; i<n; i++)
			{
				string line1 = file.ReadLine();

				//party member
				string item1 = line1.Substring(0, line1.IndexOf(' '));
				line1 = line1.Remove(0,item1.Length+1);
				party[i] = item1;
				
				//accuracy
				int item2 = int.Parse(line1);
				accuracy[i] = item2;
			}

			file.Close();

			//for(int j=0; j<n; j++)
			{
				int feedback = Random.Range(1,6);
				StreamReader file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Terrible_Action.txt");
				//terrible
				if(accuracy[0]<64)
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Terrible_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Terrible_Action.txt");
				}
				//bad
				else if(accuracy[0]<73 && accuracy[0]>=64)
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Bad_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Bad_Action.txt");
				}
				//awesome
				else if(accuracy[0]>92)
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Awesome_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Awesome_Action.txt");
				}
				//neutral
				else
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Neutral_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[0]+"_Neutral_Action.txt");
				}

				//skip loop
				for(int i=0; i<feedback-1; i++)
				{
					int tempSize = int.Parse(file.ReadLine());
					//Debug.Log("Skipping "+tempSize +" lines");
					for(int k=0; k<tempSize; k++)
						file.ReadLine();
				}

				int size = int.Parse(file.ReadLine());

				//read dialogues from file
				for(int i=0; i<size; i++)
				{
					//name
					string line1 = file.ReadLine();
					string item1 = line1.Substring(0, line1.IndexOf(' '));
					line1 = line1.Remove(0,item1.Length+1);
					speaker.Add(item1);

					//dialogue
					line1 = line1.Replace("@", "\n");
					Debug.Log(line1);
					string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
					dialogue.Add (item2);	                             
				}

				file.Close();

				//skip loop
				for(int i=0; i<feedback-1; i++)
				{
					int tempSize = int.Parse(file2.ReadLine());
					for(int k=0; k<tempSize; k++)
						file2.ReadLine();
				}

				size = int.Parse(file2.ReadLine());

				//skip all irrelevant lines
				for(int i=0; i<size; i++)
				{
					List<string> currentActions = new List<string>();
					string[] actionLine = file2.ReadLine().Split();
					for(int k=0; k<actionLine.Length; k++)
					{
						currentActions.Add(actionLine[k]);
					}
					//Debug.Log("Actions this line: "+currentActions.Count);
					actions.Add(currentActions);
				}
				
				file2.Close();
			}
					
					/*//skip loop
					for(int i=0; i<feedback-1; i++)
					{
						int tempSize = int.Parse(file.ReadLine());
						Debug.Log("Skipping "+tempSize +" lines");
						for(int k=0; k<tempSize; k++)
							file.ReadLine();
					}

					int size = int.Parse(file.ReadLine());
					//read dialogues from file
					for(int i=0; i<size; i++)
					{
						//name
						string line1 = file.ReadLine();
						string item1 = line1.Substring(0, line1.IndexOf(' '));
						line1 = line1.Remove(0,item1.Length+1);
						speaker.Add(item1);
						
						//dialogue
						line1 = line1.Replace("@", "\n");
						string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
						dialogue.Add (item2);                           
					}

					file.Close();

					StreamReader file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+party[j]+"_Positive_Action.txt");

					//skip loop
					for(int i=0; i<feedback-1; i++)
					{
						int tempSize = int.Parse(file2.ReadLine());
						for(int k=0; k<tempSize; k++)
							file2.ReadLine();
					}

					size = int.Parse(file2.ReadLine());
					//read
					for(int i=0; i<size; i++)
					{
						List<string> currentActions = new List<string>();
						string[] actionLine = file2.ReadLine().Split();
						for(int k=0; k<actionLine.Length; k++)
						{
							currentActions.Add(actionLine[k]);
						}
						actions.Add(currentActions);
					}
					
					file2.Close();*/
			save ();
		}
		else if(currentScene == (int)Scene.Relationship1)
		{
			pause=true;
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().SendMessage("Shuffle");
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().enable();
			speaker.Add("");
			dialogue.Add("Select a relationship to pursue.");

			//dummy values
			List<string> currentActions = new List<string>();
			currentActions.Add("Randall");
			currentActions.Add("MouthClosed");
			actions.Add(currentActions);

		}
		else if(currentScene == (int)Scene.Relationship2)
		{
			pause=true;
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().SendMessage("Display");
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().enable();
			speaker.Add("");
			dialogue.Add("Select a relationship to pursue.");

			//dummy values
			List<string> currentActions = new List<string>();
			currentActions.Add("Randall");
			currentActions.Add("MouthClosed");
			actions.Add(currentActions);
		}
		currentIndex = 0;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		if(currentScene != (int)Scene.Relationship1 && currentScene != (int)Scene.Relationship2)
		{
			changeSprites();
			changeColour((string)speaker[currentIndex]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(currentScene==(int)Scene.Morning || currentScene==(int)Scene.Feedback || 
		   (currentScene==(int)Scene.Relationship1 && chosen!=null) || (currentScene==(int)Scene.Relationship2 && chosen!=null))
		{
			if(Input.GetKeyDown(KeyCode.Space)){
				if(pause==true)
				{
					//do nothing
				}
				else
				{
					if(currentIndex+1 < dialogue.Count)
					{
						currentIndex++;
						changeSprites();
						name.text = (string)speaker[currentIndex];
						changeColour((string)speaker[currentIndex]);
						line.text = (string)dialogue[currentIndex];
						//AudioSettings.dspTime for future reference
					}
					else
					{
						if(currentScene==(int)Scene.Relationship2)
						{
							//end of week
							if(currentDay==5)
							{
								GameObject.Find("Save").GetComponent<SaveScript>().currentWeek++;
								GameObject.Find("Save").GetComponent<SaveScript>().currentDay = 1;
								GameObject.Find("Save").GetComponent<SaveScript>().currentScene = (int)Scene.Morning;
							}
							//end of day
							else
							{
								GameObject.Find("Save").GetComponent<SaveScript>().currentDay++;
								GameObject.Find("Save").GetComponent<SaveScript>().currentScene = (int)Scene.Morning;
							}
						}
						//still same day
						else
						{
							GameObject.Find("Save").GetComponent<SaveScript>().currentScene++;
						}
						DontDestroyOnLoad(GameObject.Find("Save"));
						Application.LoadLevel(Application.loadedLevelName);
					}
				}
			}
		}
	}

	void save()
	{
		for(int i=0; i<characters.Length; i++)
			GameObject.Find("Save").GetComponent<SaveScript>().characters[i] = characters[i];
		GameObject.Find("Save").GetComponent<SaveScript>().saveFile(1);
	}

	void changeColour(string speaker)
	{
		if(speaker.Equals("Randall"))
			name.material = characters[0].colour;
		else if(speaker.Equals("Julie"))
			name.material = characters[1].colour;
		else if(speaker.Equals("Tani"))
			name.material = characters[2].colour;
		else if(speaker.Equals("Nikolai"))
			name.material = characters[3].colour;
		else if(speaker.Equals("Carol"))
			name.material = characters[4].colour;
		else if(speaker.Equals("Rusty"))
			name.material = characters[5].colour;
		else
			name.material.color = Color.white;
	}

	void changeSprites()
	{
		//Debug.Log("Char: " +actions[0][0] +", Action: "+actions[0][1]);
		//Debug.Log("actions[currentIndex][0]: "+actions[currentIndex][0]);
		if(!actions[currentIndex][0].Equals("null"))
		{
			for(int i=0; i<actions[currentIndex].Count; i+=2)
			{
				int character = -1;
				if(actions[currentIndex][i].Equals("Randall"))
					character = (int) Chars.Randall;
				else if(actions[currentIndex][i].Equals("Julie"))
					character = (int) Chars.Julie;
				else if(actions[currentIndex][i].Equals("Tani"))
					character = (int) Chars.Tani;
				else if(actions[currentIndex][i].Equals("Nikolai"))
					character = (int) Chars.Nikolai;
				else if(actions[currentIndex][i].Equals("Carol"))
					character = (int) Chars.Carol;
				else if(actions[currentIndex][i].Equals("Rusty"))
					character = (int) Chars.Rusty;
				//Debug.Log("Char: " +actions[currentIndex][i] +", Action: "+actions[currentIndex][i+1]);
				characters[character].gameObject.SendMessage(actions[currentIndex][i+1]);
			}
		}
	}

	public void readFreeRelationship()
	{
		Relationship current = new Relationship();
		Relationship[] all = GameObject.Find("Save").GetComponent<SaveScript>().relationships;

		//save current relationship
		for(int i=0; i<all.Length; i++)
		{
			if(all[i].getName().Equals(chosen))
				current = all[i];
		}

		string char1 = chosen.Substring(0, chosen.IndexOf("/"));
		chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
		string char2 = chosen;

		StreamReader file;
		StreamReader file2;

		int speak = Random.Range(1,3);
		string other;
		//first character
		if(speak == 1)
		{
			speaker.Add(char1);
			other = char2;
			file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Free.txt");
			file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Free_Action.txt");
		}
		else
		{
			speaker.Add(char2);
			other = char1;
			file = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Free.txt");
			file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Free_Action.txt");
		}

		int feedback = Random.Range(1,8);
		Debug.Log("Topic: "+feedback);

		//skip loop
		for(int i=0; i<feedback-1; i++)
		{
			for(int k=0; k<3; k++)
				file.ReadLine();
		}

		string line1 = file.ReadLine();
		line1 = line1.Replace("@", "\n");
		string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
		dialogue.Add (item2);
		
		file.Close();
		
		//skip loop
		for(int i=0; i<feedback-1; i++)
		{
			for(int k=0; k<3; k++)
				file2.ReadLine();
		}

		List<string> currentActions = new List<string>();
		string[] actionLine = file2.ReadLine().Split();
		currentActions.Add(other);
		currentActions.Add("MouthClosed");
		for(int k=0; k<actionLine.Length; k++)
		{
			currentActions.Add(actionLine[k]);
		}
		actions.Add(currentActions);

		file2.Close();

		//second character
		if(speak == 1)
		{
			speaker.Add(char2);
			other = char1;
			file = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Free.txt");
			file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Free_Action.txt");
		}
		else
		{
			speaker.Add(char1);
			other = char2;
			file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Free.txt");
			file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Free_Action.txt");
		}
		
		//skip loop
		for(int i=0; i<feedback-1; i++)
		{
			for(int k=0; k<3; k++)
				file.ReadLine();
		}

		file.ReadLine();
		line1 = file.ReadLine();
		line1 = line1.Replace("@", "\n");
		item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
		dialogue.Add (item2);
		
		file.Close();
		
		//skip loop
		for(int i=0; i<feedback-1; i++)
		{
			for(int k=0; k<3; k++)
				file2.ReadLine();
		}
		
		currentActions = new List<string>();
		file2.ReadLine();
		actionLine = file2.ReadLine().Split();
		currentActions.Add(other);
		currentActions.Add("MouthClosed");
		for(int k=0; k<actionLine.Length; k++)
		{
			currentActions.Add(actionLine[k]);
		}
		actions.Add(currentActions);
		
		file2.Close();

		Character c1 = characters[0];
		Character c2 = characters[1];

		List<string> nullActions = new List<string>();
		//update characters' mood and relationship progress
		bool hasProgressed = false;
		for(int i=0; i<characters.Length; i++)
		{
			if(characters[i].name.Equals(char1))
			{
				c1 = characters[i];
				freeUpdate(c1, current.getChar1Value(), nullActions);
				actions.Add(nullActions);
				if(hasProgressed==false)
				{
					if(characters[i].mood > 0)
					{
						//progress +1
						current.setProgress(current.getProgress()+1);
					}
					else
					{
						//progress -1
						current.setProgress(current.getProgress()-1);
					}
					hasProgressed = true;
				}
			}
			if(characters[i].name.Equals(char2))
			{
				c2 = characters[i];
				freeUpdate(c2, current.getChar2Value(), nullActions);
				actions.Add(nullActions);
				if(hasProgressed==false)
				{
					if(characters[i].mood > 0)
					{
						//progress +1
						current.setProgress(current.getProgress()+1);
					}
					else
					{
						//progress -1
						current.setProgress(current.getProgress()-1);
					}
					hasProgressed = true;
				}
			}
		}

		//actions.Add(nullActions);
		currentIndex = 1;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeColour((string)speaker[currentIndex]);
		changeSprites();
	}

	public void readScriptedRelationship(int level)
	{
		Relationship current = new Relationship();
		Relationship[] all = GameObject.Find("Save").GetComponent<SaveScript>().relationships;
		
		//save current relationship
		for(int i=0; i<all.Length; i++)
		{
			if(all[i].getName().Equals(chosen))
				current = all[i];
		}
		
		string char1 = chosen.Substring(0, chosen.IndexOf("/"));
		chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
		string char2 = chosen;
		
		StreamReader file;
		StreamReader file2;

		if(level<0)
		{
			file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_"+char2+"_Rivalry_"+(-level)+".txt");
			file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_"+char2+"_Rivalry_"+(-level)+"_Action.txt");
		}
		else
		{
			file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_"+char2+"_Friendship_"+level+".txt");
			file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_"+char2+"_Friendship_"+level+"_Action.txt");
		}

		int size = int.Parse(file.ReadLine());
		
		//read dialogues from file
		for(int i=0; i<size; i++)
		{
			//name
			string line1 = file.ReadLine();
			string item1 = line1.Substring(0, line1.IndexOf(' '));
			line1 = line1.Remove(0,item1.Length+1);
			speaker.Add(item1);
			
			//dialogue
			string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
			item2 = item2.Replace("@", "\n");
			dialogue.Add(item2);                             
		}
		file.Close();

		size = int.Parse(file2.ReadLine());
		//skip all irrelevant lines
		for(int i=0; i<size; i++)
		{
			List<string> currentActions = new List<string>();
			string[] actionLine = file2.ReadLine().Split();
			for(int j=0; j<actionLine.Length; j++)
			{
				currentActions.Add(actionLine[j]);
			}
			actions.Add(currentActions);
		}
		
		file2.Close();

		Character c1 = characters[0];
		Character c2 = characters[1];
		
		List<string> nullActions = new List<string>();
		//find characters
		for(int i=0; i<characters.Length; i++)
		{
			if(characters[i].name.Equals(char1))
			{
				c1 = characters[i];
			}
			if(characters[i].name.Equals(char2))
			{
				c2 = characters[i];
			}
		} 

		scriptedUpdate(c1, c2, current.getChar1Value(), current.getChar2Value(), level, nullActions);
		//if either char is upset, negative, else positive
		if(c1.mood < 0 || c2.mood < 0)
		{
			//progress -1
			current.setProgress(current.getProgress()-1);
		}
		else
		{
			//progress +1
			current.setProgress(current.getProgress()+1);
		}
		
		actions.Add(nullActions);
		currentIndex = 1;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeColour((string)speaker[currentIndex]);
		changeSprites();
	}

	void freeUpdate(Character c, int value, List<string> nullActions)
	{
		//if the character is feeling positive about the interaction
		if(value == 1)
		{
			speaker.Add("");
			dialogue.Add(c.name +"'s mood: +" +2+"%");
			c.mood +=2;
			nullActions.Add("null");
		}
		save ();
	}

	//see if I can change the colour of inc/dec
	void scriptedUpdate(Character c1, Character c2, int v1, int v2, int level, List<string> nullActions)
	{
		//Debug.Log("Scripted mood update");
		//friendship
		if(level > 0)
		{
			speaker.Add("");
			dialogue.Add(c1.name +"'s mood: +" +10 +"%\n" +c2.name +"'s mood: +" +10+"%");
			c1.mood += 10;
			c2.mood += 10;
			nullActions.Add("null");
		}
		//rivalry
		else
		{
			int small = 5;
			int large = 15;
			switch(level)
			{
			case -12:
				large = 35;
				break;
			case -8:
				large = 25;
				break;
			case -4:
				large = 15;
				break;
			}

			//give the "happier" one a smaller mood decrement
			if(v1==1)
			{
				speaker.Add("");
				dialogue.Add(c1.name +"'s mood: -" +small +"%\n" +c2.name +"'s mood: -" +large+"%");
				c1.mood -= small;
				c2.mood -= large;
				nullActions.Add("null");
			}
			else
			{
				speaker.Add("");
				dialogue.Add(c1.name +"'s mood: -" +large +"%\n" +c2.name +"'s mood: -" +small+"%");
				c1.mood -= large;
				c2.mood -= small;
				nullActions.Add("null");
			}
		}
		save ();
	}
}