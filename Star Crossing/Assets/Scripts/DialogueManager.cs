using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DialogueManager : MonoBehaviour {

	//enums to help organise characters, positions, and scene state
	enum Chars {Blake, Kim, Nancy, Ash};
	enum Pos {LeftFront, LeftBack, RightFront, RightBack};
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
	public GUIText name;
	public GUIText line;

	//current dialogue
	int currentIndex;

	//temp currents
	int currentWeek;
	int currentDay;
	int currentScene;

	// Use this for initialization
	void Start () {

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
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Wk" +currentWeek+"_Day"+currentDay+"_Feedback.txt");
			Debug.Log("Score: "+int.Parse(file.ReadLine()));

			string[] party = new string[4];
			int[] accuracy = new int[4];

			for(int i=0; i<4; i++)
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

			for(int j=0; j<4; j++)
			{
				if(accuracy[j]<=80)
				{
					//randomise later? or make internal (within file)
					int feedback = Random.Range(1,5);
					file = new StreamReader(Application.dataPath + "/Resources/Files/"+party[j]+"_Negative_Feedback.txt");

					//skip loop
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
						string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
						dialogue.Add (item2);	                             
					}

					file.Close();

					StreamReader file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+party[j]+"_Negative_Action.txt");
					
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
						Debug.Log("Actions this line: "+currentActions.Count);
						actions.Add(currentActions);
					}
					
					file2.Close();
				}
				else if(accuracy[j] >=90)
				{
					int feedback = Random.Range(1,5);
					file = new StreamReader(Application.dataPath + "/Resources/Files/"+party[j]+"_Positive_Feedback.txt");
					
					//skip loop
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
					
					file2.Close();
				}
			}
		}
		currentIndex = 0;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeSprites();
		//changeColour(speaker[currentIndex]);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
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

	void changeColour(string speaker)
	{
		if(speaker.Equals("Blake"))
			name.material = characters[0].colour;
		else if(speaker.Equals("Kim"))
			name.material = characters[1].colour;
		else if(speaker.Equals("Nancy"))
			name.material = characters[2].colour;
		else if(speaker.Equals("Ash"))
			name.material = characters[3].colour;
		else
			name.material.color = Color.white;
	}

	void changeSprites()
	{
		//Debug.Log("actions[currentIndex][0]: "+actions[currentIndex][0]);
		if(!actions[currentIndex][0].Equals("null"))
		{
			for(int i=0; i<actions[currentIndex].Count; i+=2)
			{
				int character = -1;
				if(actions[currentIndex][i].Equals("Blake"))
					character = (int) Chars.Blake;
				else if(actions[currentIndex][i].Equals("Kim"))
					character = (int) Chars.Kim;
				else if(actions[currentIndex][i].Equals("Nancy"))
					character = (int) Chars.Nancy;
				else if(actions[currentIndex][i].Equals("Ash"))
					character = (int) Chars.Ash;
				//Debug.Log("Char: " +actions[currentIndex][i] +", Action: "+actions[currentIndex][i+1]);
				characters[character].gameObject.SendMessage(actions[currentIndex][i+1]);
			}
		}
	}
	/*void changeSprites()
	{
		if(currentIndex==0)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("MouthOpen");
			characters[(int)Chars.Kim].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Nancy].gameObject.SendMessage("MouthClosed");
		}
		if(currentIndex==1)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Kim].gameObject.SendMessage("MouthOpen");
		}
		if(currentIndex==2)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("Laughing");
			characters[(int)Chars.Kim].gameObject.SendMessage("MouthClosed");
		}
		if(currentIndex==3)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("MouthOpen");
		}
		if(currentIndex==4)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Nancy].gameObject.SendMessage("Worried");
		}
		if(currentIndex==5)
		{
		}
		if(currentIndex==6)
		{
			Vector2[] args = new Vector2[1];

			Vector2 move = new Vector2(-2, 0);
			args[0] = move;
			characters[(int)Chars.Nancy].gameObject.SendMessage("Move", args);
			characters[(int)Chars.Nancy].gameObject.SendMessage("Flip");
			characters[(int)Chars.Nancy].gameObject.SendMessage("MouthClosed");

			move = new Vector2(2, 0);
			args[0] = move;
			characters[(int)Chars.Ash].gameObject.SendMessage("Move", args);
			characters[(int)Chars.Ash].gameObject.SendMessage("MouthOpen");
		}
		if(currentIndex==7)
		{
			
		}
		if(currentIndex==8)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("Laughing");
			characters[(int)Chars.Ash].gameObject.SendMessage("MouthClosed");
		}
		if(currentIndex==9)
		{
			characters[(int)Chars.Blake].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Nancy].gameObject.SendMessage("MouthOpen");
			characters[(int)Chars.Ash].gameObject.SendMessage("MouthClosed");
		}
		if(currentIndex==10)
		{
			characters[(int)Chars.Nancy].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Ash].gameObject.SendMessage("MouthOpen");
		}
		if(currentIndex==11)
		{
			
		}
		if(currentIndex==12)
		{
			characters[(int)Chars.Ash].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Kim].gameObject.SendMessage("MouthOpen");
		}
		if(currentIndex==13)
		{
			characters[(int)Chars.Kim].gameObject.SendMessage("MouthClosed");
			characters[(int)Chars.Blake].gameObject.SendMessage("MouthOpen");
		}
	}*/
}