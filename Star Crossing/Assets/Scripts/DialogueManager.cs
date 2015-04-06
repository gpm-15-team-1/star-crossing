using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	//enums to help organise characters, positions, and scene state
	enum Chars {Randall, Julie, Tani, Nikolai, Carol, Rusty};
	enum Scene {Morning, MorningTalks, Feedback, Relationship1, Evening};
	enum End {Morning, Feedback, Evening};

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

	//clocl
	public Text clock;

	//background
	public GameObject background;

	bool skip = false;

	//current dialogue
	int currentIndex;

	//temp currents
	int currentWeek;
	int currentDay;
	int currentScene;

	//mood updates
	public Image updatePanel;
	public Text updateText;
	public Material goodMaterial;
	public Material badMaterial;
	public AudioClip goodSound;
	public AudioClip badSound;

	public AudioClip bgMusic_Cutscene;
	public AudioClip bgMusic_Talks;
	public AudioClip bgMusic_Relationship;

	//relationship
	public string chosen = "";

	//"pause" space action
	public bool pause = false;
	int pauseIndex=-1;
	public string topic = null;

	//fading transition
	public GameObject fade;
	CanvasGroup cg;
	float fromValue = 0.0f;
	Animator anim;

	int fadeTime = 0;
	int fadeCounter = 1;

	// Use this for initialization
	void Start () {

		//fade.SetActive(false);
		skip = false;

		line.supportRichText = true;

		//time
		clock.text = GameObject.Find("Save").GetComponent<SaveScript>().clock.GetComponent<ClockScript>().GetTimeAsString();

		//audio
		audio.volume = 1.0f;

		chosen = "";

		fade.GetComponent<FadeScript>().SendMessage("fadeIn");
		//iTween.MoveTo(cg.gameObject,new Vector3(2,0,0),2);
		//iTween.ValueTo(fade.gameObject, iTween.Hash("from", fromValue, "to", 1, "onupdatetarget", fade.gameObject, "onupdate", "updateFromValue", "time", 1.0f, "easetype", iTween.EaseType.easeInExpo));

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

		GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
		GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();

		Debug.Log("<<<CURRENT SCENE: "+currentScene+">>>");

		hideUpdate();

		//IF MORNING OR EVENING SCENE DO THIS
		if(currentScene == (int)Scene.Morning || (currentScene == (int)Scene.Evening && currentDay != 4) || (currentScene == (int)End.Evening && currentDay == 4))
		{
			//set clock
			clock.GetComponent<ClockScript>().SetTime(9, 30);
			clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();

			//change music
			audio.clip = bgMusic_Cutscene;
			audio.volume = 0.25f;
			audio.Play();
			audio.loop = true;
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
			GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();

			//reading from file:
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_Morning.txt");
			if((currentScene == (int)End.Evening && currentDay == 4) || (currentScene == (int)Scene.Evening && currentDay != 4))
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_Evening.txt");
			}

			//change background
			Sprite temp = Resources.Load<Sprite>("Sprites/Environment/"+file.ReadLine());
			background.GetComponent<SpriteRenderer>().sprite = temp;

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
				Debug.Log(item2);
				dialogue.Add(item2);                             
			}
			file.Close();

			file = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_Morning_Action.txt");
			if(currentScene == (int)End.Evening && currentDay == 4)
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_Evening_Action.txt");
			}
				
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
		else if((currentScene == (int)Scene.Feedback && currentDay != 4)||
		        (currentScene == (int)End.Feedback && currentDay == 4))
		{
			//set clock
			clock.GetComponent<ClockScript>().SetTime(12, 0);
			clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();

			//player feedback
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
			GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();

			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback.txt");
			//int score = int.Parse(file.ReadLine());
			//Debug.Log("Score: "+score);

			//change background
			Sprite temp = Resources.Load<Sprite>("Sprites/Environment/practice-room");
			background.GetComponent<SpriteRenderer>().sprite = temp;

			int n = characters.Length-1;
			Debug.Log("Number of characters: "+n);
			string[] party = new string[n];
			int[] accuracy = new int[n];

			for(int i=0; i<n; i++)
			{
				string line1 = file.ReadLine();

				//party member
				string item1 = line1.Substring(0, line1.IndexOf(' '));
				line1 = line1.Remove(0,item1.Length+1);
				party[i] = item1;
				//Debug.Log(party[i]);

				//accuracy
				//Debug.Log(line1);
				float f = float.Parse(line1);
				int item2 = (int)f;
				accuracy[i] = item2;
			}

			file.Close();


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

			readRandomDialogue(file, file2, feedback);

			//status of any one other character who may be a concern
			bool concern = false;
			for(int i=1; i<n-1; i++)
			{
				feedback = Random.Range(1,6);
				//terrible
				if(accuracy[i]<64)
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[i]+"_Terrible_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[i]+"_Terrible_Action.txt");
					readRandomDialogue(file, file2, feedback);
					concern = true;
					break;
				}
				//bad
				else if(accuracy[i]<73 && accuracy[i]>=64)
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[i]+"_Bad_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[i]+"_Bad_Action.txt");
					readRandomDialogue(file, file2, feedback);
					concern = true;
					break;
				}
				//awesome
				else if(accuracy[i]>92)
				{
					file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[i]+"_Awesome_Feedback.txt");
					file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/"+party[i]+"_Awesome_Action.txt");
					readRandomDialogue(file, file2, feedback);
					concern = true;
					break;
				}
			}

			if(concern==false)
			{
				feedback = Random.Range(1,8);
				//provide generic feedback
				file = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/Generic_Feedback.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/Feedback/Generic_Action.txt");
				readRandomDialogue(file, file2, feedback);
			}

			Debug.Log("Now saving.");save ();
		}
		else if(currentScene == (int)Scene.Relationship1 && currentDay != 4)
		{
			//set clock
			//Debug.Log("<<<TEMP TIME: "+tempTime+">>>");
			//tempTime = tempTime.Substring(tempTime.IndexOf('0'), tempTime.Length-1);
			//int temp = int.Parse(tempTime);

			if(clock.GetComponent<ClockScript>().getHour()<13)
			{
				clock.GetComponent<ClockScript>().SetTime(13, 0);
				clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();
			}

			Debug.Log("<<<CURRENT TIME: "+clock.GetComponent<ClockScript>().GetTimeAsString()+">>>");

			audio.clip = bgMusic_Relationship;
			audio.volume = 0.25f;
			audio.Play();
			audio.loop = true;

			pause=true;
			//GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().SendMessage("Display");
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().enable();
			GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();

			speaker.Add("");
			dialogue.Add("Select an action.");

			//dummy values
			List<string> currentActions = new List<string>();
			currentActions.Add("Randall");
			currentActions.Add("MouthClosed");
			actions.Add(currentActions);

			Debug.Log("Now saving.");save ();

		}
		//morning conversations
		else if(currentScene == (int)Scene.MorningTalks && currentDay !=4)
		{
			//set clock
			clock.GetComponent<ClockScript>().SetTime(9, 45);
			clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();

			topic = null;
			audio.clip = bgMusic_Talks;
			audio.volume = 0.25f;
			audio.Play();
			audio.loop = true;

			pause=true;

			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_MorningTalks.txt");

			//change background
			Sprite temp = Resources.Load<Sprite>("Sprites/Environment/"+file.ReadLine());
			background.GetComponent<SpriteRenderer>().sprite = temp;

			file.ReadLine();

			string[] args = new string[3];

			for(int i=0; i<3; i++)
			{
				args[i] = file.ReadLine();
			}

			file.Close ();

			GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().SendMessage("Display", args);
			GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().enable();
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();

			speaker.Add("");
			dialogue.Add("Select a conversation to pursue.");
			
			//dummy values
			List<string> currentActions = new List<string>();
			currentActions.Add("Randall");
			currentActions.Add("MouthClosed");
			actions.Add(currentActions);

			Debug.Log("Now saving.");save ();
		}
		else if(currentScene == (int)Scene.Evening && currentDay != 4)
		{
			audio.clip = bgMusic_Relationship;
			audio.volume = 0.25f;
			audio.Play();
			audio.loop = true;

			pause=true;
			//GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().SendMessage("Display");
			GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().enable();
			GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();

			speaker.Add("");
			dialogue.Add("Select an action.");

			//dummy values
			List<string> currentActions = new List<string>();
			currentActions.Add("Randall");
			currentActions.Add("MouthClosed");
			actions.Add(currentActions);
		}
		currentIndex = 0;

		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];

		//if(((currentScene != (int)Scene.Relationship1 && currentDay !=4) || (currentScene != (int)Scene.Evening && currentDay !=4)) && (currentScene != (int)Scene.MorningTalks && currentDay !=4) && ((currentScene==(int)End.Evening || currentScene==(int)End.Feedback) && currentDay==4))
		if((currentScene==(int)Scene.Morning) || //if it's morning
		   (currentScene==(int)Scene.Feedback && currentDay != 4) || //feedback not on Day 4
		   (currentScene==(int)End.Feedback && currentDay == 4) || //feedback on Day 4
		   (currentScene==(int)End.Evening && currentDay == 4)) //evening on Day 4
		{
			Debug.Log("Change sprites");
			changeSprites();
			changeColour((string)speaker[currentIndex]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(currentScene==(int)Scene.Morning || currentScene==(int)Scene.Feedback || currentScene==(int)Scene.MorningTalks ||
		   (currentScene==(int)Scene.Relationship1 && chosen!=null) || (currentScene==(int)Scene.Evening && chosen!=null))
		{
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
				if(pause==true)
				{
					if((currentScene==(int)Scene.Relationship1 && currentIndex!=0) || (currentScene==(int)Scene.Evening && currentIndex!=0))
					{
						GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().enable();
					}
					//do nothing
				}
				//if space was pressed for the first time
				else if(skip==true)
				{
					StopAllCoroutines();
					if(!line.text.Equals((string)dialogue[currentIndex]))
						line.text = (string)dialogue[currentIndex];
					skip=false;
				}
				else
				{
					if(currentIndex+1 < actions.Count)
					{
						//not the "select relationship" statement
						if(((string)speaker[currentIndex+1]).Equals("") && currentIndex!=0)
						{
							currentIndex++;
							updateText.text = (string)dialogue[currentIndex];
							showUpdate();
							if(updateText.material == goodMaterial)
							{
								audio.clip = goodSound;
								audio.loop = false;
								audio.Play();
							}
							else if(updateText.material == badMaterial)
							{
								audio.clip = badSound;
								audio.loop = false;
								audio.Play();
							}
						}
						else if(skip==false)
						{
							skip = true;
							StopAllCoroutines();
							line.text = "";
							line.audio.Play();

							hideUpdate();

							currentIndex++;
							if(currentIndex==pauseIndex)
							{
								pause = true;
								line.text = (string)dialogue[currentIndex];
							}
							else
							{
								pause = false;
							}

							if(pause==false)
							{
								changeSprites();
								name.text = (string)speaker[currentIndex];
								changeColour((string)speaker[currentIndex]);
								//line.text = (string)dialogue[currentIndex];
								string[] args = new string[1];
								args[0] = (string)dialogue[currentIndex];
								StartCoroutine("TypeText", args);
							}
						}
						if(currentScene==(int)Scene.Feedback)
						{
							Debug.Log("Fade counter:" +fadeCounter);
							Debug.Log("Fade time:" +fadeTime);
							if(fadeCounter!=fadeTime)
							{
								fadeCounter++;
							}
							else
							{
								//fade out and in
								fade.GetComponent<FadeScript>().SendMessage("fadeOut");

								fade.GetComponent<FadeScript>().SendMessage("fadeIn");
								fadeCounter = 1;
								fadeTime = 0;
							}
						}
					}
					else
					{
						string toLoad = Application.loadedLevelName;
						//if end of either day type
						if((currentScene==(int)Scene.Evening && currentDay!=4) ||
						   (currentScene==(int)End.Evening && currentDay==4))
						{
							//end of week (4 days in a week!)
							if(currentDay==4)
							{
								GameObject.Find("Save").GetComponent<SaveScript>().currentWeek++;
								GameObject.Find("Save").GetComponent<SaveScript>().currentDay = 1;
								GameObject.Find("Save").GetComponent<SaveScript>().currentScene = (int)Scene.Morning;
							}
							//end of day
							else
							{
								Debug.Log("<<<DELETE>>>");
								File.Delete(Application.dataPath +"/Resources/Files/Saves/Chosen.txt");
								File.Delete(Application.dataPath +"/Resources/Files/Saves/Chosen.txt.meta");

								GameObject.Find("Save").GetComponent<SaveScript>().currentDay++;
								GameObject.Find("Save").GetComponent<SaveScript>().currentScene = (int)Scene.Morning;
								toLoad = "SaveMenu";
							}
						}
						//go to rhythm mode select menu
						else if((currentScene==(int)Scene.MorningTalks && currentDay!=4) || (currentScene==(int)End.Morning && currentDay==4))
						{
							GameObject.Find("Save").GetComponent<SaveScript>().currentScene++;
							toLoad = "CharSelectScreen";
						}
						else if((currentScene==(int)Scene.Relationship1 && currentDay!=4) && !clock.text.Equals("5:00 P.M."))
						{
							Debug.Log("Refreshing Relationship1");
							//reload the same level with the same scene
							toLoad = "StoryScene01";
						}
						//still same day
						else
						{
							GameObject.Find("Save").GetComponent<SaveScript>().currentScene++;
						}
						
						Debug.Log("Now saving.");save ();
						fade.GetComponent<FadeScript>().SendMessage("fadeOut");
						//DontDestroyOnLoad(GameObject.Find("Save"));
						//insert fading transition here
						Application.LoadLevel(toLoad);
					}
				}
			}
			//skip
			else if (Input.GetKeyDown(KeyCode.Tab))
			{
				string toLoad = Application.loadedLevelName;
				//if end of either day type
				if((currentScene==(int)Scene.Evening && currentDay!=4) ||
				   (currentScene==(int)End.Evening && currentDay==4))
				{
					//end of week (4 days in a week!)
					if(currentDay==4)
					{
						GameObject.Find("Save").GetComponent<SaveScript>().currentWeek++;
						GameObject.Find("Save").GetComponent<SaveScript>().currentDay = 1;
						GameObject.Find("Save").GetComponent<SaveScript>().currentScene = (int)Scene.Morning;
					}
					//end of day
					else
					{
						Debug.Log("<<<DELETE>>>");
						File.Delete(Application.dataPath +"/Resources/Files/Saves/Chosen.txt");
						File.Delete(Application.dataPath +"/Resources/Files/Saves/Chosen.txt.meta");

						GameObject.Find("Save").GetComponent<SaveScript>().currentDay++;
						GameObject.Find("Save").GetComponent<SaveScript>().currentScene = (int)Scene.Morning;
						toLoad = "SaveMenu";
					}
				}
				//go to rhythm mode select menu
				else if((currentScene==(int)Scene.MorningTalks && currentDay!=4) ||
				        (currentScene==(int)End.Morning && currentDay==4))
				{
					GameObject.Find("Save").GetComponent<SaveScript>().currentScene++;
					toLoad = "CharSelectScreen";
				}
				else if((currentScene==(int)Scene.Relationship1 && currentDay!=4) && !clock.text.Equals("5:00 P.M."))
				{
					Debug.Log("Refreshing Relationship1");
					//reload the same level with the same scene
					toLoad = "StoryScene01";
				}
				//still same day
				else
				{
					GameObject.Find("Save").GetComponent<SaveScript>().currentScene++;
				}
				
				Debug.Log("Now saving.");save ();
				fade.GetComponent<FadeScript>().SendMessage("fadeOut");
				//DontDestroyOnLoad(GameObject.Find("Save"));
				//insert fading transition here
				Application.LoadLevel(toLoad);
			}
		}
	}

	IEnumerator TypeText (string[] args) {
		//char letter;
		char[] array = args[0].ToCharArray();
		int length = array.Length;
		bool inTag = false;
		int i=0;

		while(i<length) {

			//identify tags in dialogue
			if(array[i].Equals('<'))
			{
				inTag = true;
			}
			//regular dialogue
			if(inTag==false)
			{
				line.text += array[i];
				i++;
				yield return 0;
				yield return new WaitForSeconds (0.00025f);
			}
			else
			{
				string tag = null;
				string endTag = null;
				
				//get colour tag
				while(!array[i].Equals('>'))
				{
					tag+=array[i];
					i++;
				}

				//get that last one
				tag+=array[i];
				i++;

				//append until closing tag
				while(!array[i].Equals('<'))
				{
					if(tag.Equals("<color>"))
					   endTag = "</color>";
					else if(tag.Equals("<i>"))
						endTag = "</i>";
					else if(tag.Equals("<b>"))
						endTag = "</b>";
					
					line.text+=tag+array[i]+endTag;
					i++;
					yield return 0;
					yield return new WaitForSeconds (0.00025f);
				}

				//skip closing tag
				while(!array[i].Equals('>'))
				{
					i++;
				}

				//get that last one
				i++;

				inTag = false;
			}
		}      
		skip = false;
	}

	void save()
	{
		for(int i=0; i<characters.Length; i++)
			GameObject.Find("Save").GetComponent<SaveScript>().characters[i] = characters[i];
		GameObject.Find("Save").GetComponent<SaveScript>().clock.GetComponent<ClockScript>().SetTime(this.clock.GetComponent<ClockScript>().getHour(), this.clock.GetComponent<ClockScript>().getMinute());
		GameObject.Find("Save").GetComponent<SaveScript>().saveFile();
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
				Debug.Log("Char: " +actions[currentIndex][i] +", Action: "+actions[currentIndex][i+1]);
				characters[character].gameObject.SendMessage(actions[currentIndex][i+1]);
			}
		}
	}

	public void readConversation(int index)
	{
		fade.GetComponent<FadeScript>().SendMessage("fadeOut");
		fade.GetComponent<FadeScript>().SendMessage("fadeIn");
		
		StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_MorningTalks.txt");
		StreamReader file2 = new StreamReader(Application.dataPath + "/Resources/Files/Weeks/Week"+currentWeek+"/Wk" +currentWeek+"_Day"+currentDay+"_MorningTalks_Action.txt");

		//skip background file
		file.ReadLine();
		
		//add topic
		string t = file.ReadLine();
		Debug.Log("Topic: "+t);
		if(!t.Equals("null"))
		{
			Relationship.addTopic(t, GameObject.Find("Save").GetComponent<SaveScript>().relationships);
		}
		//skip button strings
		for(int i=0; i<4; i++)
		{
			file.ReadLine();
		}

		//skip loop
		for(int i=0; i<index-1; i++)
		{
			file.ReadLine();
			int skip = int.Parse(file.ReadLine());
			for(int k=0; k<=skip; k++)
				file.ReadLine();
		}

		//change background
		Sprite temp = Resources.Load<Sprite>("Sprites/Environment/"+file.ReadLine());
		background.GetComponent<SpriteRenderer>().sprite = temp;

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

		//skip loop
		for(int i=0; i<index-1; i++)
		{
			int skip = int.Parse(file2.ReadLine());
			for(int k=0; k<=skip; k++)
				Debug.Log(file2.ReadLine());
		}
		
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

		currentIndex = 1;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeColour((string)speaker[currentIndex]);
		changeSprites();
	}

	public void readFreeRelationship(int progress)
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
		string speaking;
		string other;
		//first character
		if(speak == 1)
		{
			speaker.Add(char1);
			speaking = char1;
			other = char2;
			if(current.getProgress() < 0)
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Negative_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Negative_Free_Action.txt");
			}
			else
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Positive_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Positive_Free_Action.txt");
			}
		}
		else
		{
			speaker.Add(char2);
			speaking = char2;
			other = char1;

			if(current.getProgress() < 0)
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Negative_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Negative_Free_Action.txt");
			}
			else
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Positive_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Positive_Free_Action.txt");
			}
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
		currentActions.Add(other);
		currentActions.Add("RightMid");
		currentActions.Add(speaking);
		currentActions.Add("LeftMid");
		for(int k=0; k<actionLine.Length; k++)
		{
			currentActions.Add(actionLine[k]);
		}
		actions.Add(currentActions);
		Debug.Log(actions.Count);

		file2.Close();

		//second character
		if(speak == 1)
		{
			speaker.Add(char2);
			other = char1;

			if(current.getProgress() < 0)
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Negative_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Negative_Free_Action.txt");
			}
			else
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Positive_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char2+"_Positive_Free_Action.txt");
			}
		}
		else
		{
			speaker.Add(char1);
			other = char2;

			if(current.getProgress() < 0)
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Negative_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Negative_Free_Action.txt");
			}
			else
			{
				file = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Positive_Free.txt");
				file2 = new StreamReader(Application.dataPath + "/Resources/Files/"+char1+"_Positive_Free_Action.txt");
			}
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
		Debug.Log(actions.Count);
		
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
				freeUpdate(c1, progress, nullActions);
				//Debug.Log("c1 free Actions added");

				if(hasProgressed==false)
				{
					if(characters[i].mood > 0)
					{
						//progress +1
						current.setProgress(current.getProgress()+1);
						current.setPosProgress(current.getPosProgress()+1);
					}
					else
					{
						//progress -1
						current.setProgress(current.getProgress()-1);
						current.setNegProgress(current.getNegProgress()-1);
					}
					hasProgressed = true;
				}
			}
			if(characters[i].name.Equals(char2))
			{
				c2 = characters[i];
				freeUpdate(c2, progress, nullActions);
				//Debug.Log("c2 free Actions added");

				if(hasProgressed==false)
				{
					if(characters[i].mood > 0)
					{
						//progress +1
						current.setProgress(current.getProgress()+1);
						current.setPosProgress(current.getPosProgress()+1);
					}
					else
					{
						//progress -1
						current.setProgress(current.getProgress()-1);
						current.setNegProgress(current.getNegProgress()-1);
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

		Debug.Log("<<<CURRENT TIME: "+clock.GetComponent<ClockScript>().GetTimeAsString()+">>>");

		//increment time by 30 mins
		clock.GetComponent<ClockScript>().IncrementTime(0, 30);
		clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();
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

		scriptedUpdate(c1, c2, level, nullActions);
		//if either char is upset, negative, else positive
		if(c1.mood < 0 || c2.mood < 0)
		{
			//progress -1
			current.setProgress(current.getProgress()-4);
			current.setNegProgress(current.getNegProgress()-4);
		}
		else
		{
			//progress +1
			current.setProgress(current.getProgress()+4);
			current.setPosProgress(current.getPosProgress()+4);
		}
		
		actions.Add(nullActions);
		currentIndex = 1;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeColour((string)speaker[currentIndex]);
		changeSprites();

		//increment time by 1.5 hrs
		clock.GetComponent<ClockScript>().IncrementTime(1, 30);
		clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();
	}

	//talk about a specific topic
	public void readTopicConversation1(string t)
	{
		/*Relationship current = new Relationship();
		Relationship[] all = GameObject.Find("Save").GetComponent<SaveScript>().relationships;
		
		//save current relationship
		for(int i=0; i<all.Length; i++)
		{
			if(all[i].getName().Equals(chosen))
				current = all[i];
		}*/

		topic = t;

		string tempC = chosen;
		string char1 = chosen.Substring(0, chosen.IndexOf("/"));
		chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
		string char2 = chosen;
		chosen = tempC;
		
		StreamReader file;
		StreamReader file2;
		
		file = new StreamReader(Application.dataPath + "/Resources/Files/Topics/"+char1+"_"+char2+"_"+topic+".txt");
		file2 = new StreamReader(Application.dataPath + "/Resources/Files/Topics/"+char1+"_"+char2+"_"+topic+"_Action.txt");
		
		int size = int.Parse(file.ReadLine());

		string tempSpeaker = "";
		string tempDialogue = "";

		//read dialogues from file
		for(int i=0; i<size; i++)
		{
			//name
			string line1 = file.ReadLine();
			string item1 = line1.Substring(0, line1.IndexOf(' '));
			line1 = line1.Remove(0,item1.Length+1);
			speaker.Add(item1);
			tempSpeaker = item1;
			
			//dialogue
			string item2 = line1.Substring(1,line1.LastIndexOf('"')-1);
			item2 = item2.Replace("@", "\n");
			dialogue.Add(item2); 
			tempDialogue = item2;
		}

		//push extras
		speaker.Add(tempSpeaker);
		dialogue.Add(tempDialogue);

		//read blank line
		file.ReadLine();

		List<string> nullActions = new List<string>();
		nullActions.Add("null");
		
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

		pauseIndex = size+1;
		Debug.Log("Pause at: "+pauseIndex);

		string[] args = new string[3];
		//args[0]=topic;

		for(int i=0; i<3; i++)
		{
			args[i] = file.ReadLine();
			Debug.Log("This is being read: " +args[i]);
		}
		
		file.Close ();

		//pause when pauseIndex is hit
		//pauseIndex = size;
		//display ConversationSelect in update function

		actions.Add(nullActions);
		currentIndex = 1;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeColour((string)speaker[currentIndex]);
		changeSprites();

		GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().SendMessage("Shuffle", args);
		//GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().SendMessage("Display", args);
		//GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().enable();
		//GameObject.Find("Relationship_Menu_Background").GetComponent<RelationshipMenuManager>().disable();
	}

	//talk about a specific topic
	public void readTopicConversation2(int choice)
	{
		pauseIndex = -1;
		GameObject.Find("ConversationSelect").GetComponent<ConversationSelectManager>().disable();

		Relationship current = new Relationship();
		Relationship[] all = GameObject.Find("Save").GetComponent<SaveScript>().relationships;

		string char1 = chosen.Substring(0, chosen.IndexOf("/"));
		chosen = chosen.Remove(0,chosen.IndexOf("/")+1);
		string char2 = chosen;

		string myText1 = char1 +"/" +char2;
		string myText2 = char2 +"/" +char1;

		//save current relationship
		for(int i=0; i<all.Length; i++)
		{
			if(all[i].getName().Equals(myText1))
				current = all[i];
			else if(all[i].getName().Equals(myText2))
				current = all[i];
		}
		
		StreamReader file;
		StreamReader file2;

		file = new StreamReader(Application.dataPath + "/Resources/Files/Topics/"+char1+"_"+char2+"_"+topic+".txt");
		file2 = new StreamReader(Application.dataPath + "/Resources/Files/Topics/"+char1+"_"+char2+"_"+topic+"_Action.txt");
		
		int size = int.Parse(file.ReadLine());
		int initSize = size;

		//skip loop 1 for init conversation
		for(int i=0; i<size; i++)
			file.ReadLine();

		//skip loop 2 for choices and lines
		for(int i=0; i<5; i++)
			file.ReadLine();

		//all irrelevant blocks
		for(int i=0; i<choice-1; i++)
		{
			int size2 = int.Parse (file.ReadLine());
			for(int j=0; j<size2; j++)
				file.ReadLine();
			file.ReadLine();
		}

		size = int.Parse(file.ReadLine());
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

		//all irrelevant blocks
		for(int i=0; i<choice; i++)
		{
			int size2 = int.Parse(file2.ReadLine());
			for(int j=0; j<size2; j++)
				file2.ReadLine();
			file2.ReadLine();
		}

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

		//relationship progress
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
				if(choice==1)
					freeUpdate(c1, 2, nullActions);
				else
					freeUpdate(c1, -2, nullActions);
				if(hasProgressed==false)
				{
					if(characters[i].mood > 0)
					{
						//progress +1
						current.setProgress(current.getProgress()+2);
						current.setPosProgress(current.getPosProgress()+2);
					}
					else
					{
						//progress -1
						current.setProgress(current.getProgress()-2);
						current.setNegProgress(current.getNegProgress()-2);
					}
					hasProgressed = true;
				}
			}
			if(characters[i].name.Equals(char2))
			{
				c2 = characters[i];
				if(choice==1)
					freeUpdate(c2, 2, nullActions);
				else
					freeUpdate(c2, -2, nullActions);
				if(hasProgressed==false)
				{
					if(characters[i].mood > 0)
					{
						//progress +1
						current.setProgress(current.getProgress()+2);
						current.setPosProgress(current.getPosProgress()+2);
					}
					else
					{
						//progress -1
						current.setProgress(current.getProgress()-2);
						current.setNegProgress(current.getNegProgress()-2);
					}
					hasProgressed = true;
				}
			}
		}

		//actions.Add(nullActions);
		currentIndex = initSize+1;
		name.text = (string)speaker[currentIndex];
		line.text = (string)dialogue[currentIndex];
		changeColour((string)speaker[currentIndex]);
		changeSprites();

		//increment time by 1 hr
		clock.GetComponent<ClockScript>().IncrementTime(1, 0);
		clock.text = clock.GetComponent<ClockScript>().GetTimeAsString();
	}
	
	void freeUpdate(Character c, int value, List<string> nullActions)
	{
		if(value == 1)
		{
			speaker.Add("");
			dialogue.Add(c.name +"'s mood: +" +2+"%");
			c.mood +=2;
			nullActions.Add("null");
			updateText.material = goodMaterial;
			
			actions.Add(nullActions);
		}
		else if(value == -1)
		{
			speaker.Add("");
			dialogue.Add(c.name +"'s mood: -" +2+"%");
			c.mood -=2;
			nullActions.Add("null");
			updateText.material = badMaterial;
			
			actions.Add(nullActions);
		}
		else if(value == 2)
		{
			speaker.Add("");
			dialogue.Add(c.name +"'s mood: +" +4+"%");
			c.mood +=4;
			nullActions.Add("null");
			updateText.material = goodMaterial;
			
			actions.Add(nullActions);
		}
		else if(value == -2)
		{
			speaker.Add("");
			dialogue.Add(c.name +"'s mood: -" +4+"%");
			c.mood -=4;
			nullActions.Add("null");
			updateText.material = badMaterial;
			
			actions.Add(nullActions);
		}
		
		Debug.Log("Now saving.");save ();
	}

	//consider changing values depending on tier
	void scriptedUpdate(Character c1, Character c2, int level, List<string> nullActions)
	{
		//Debug.Log("Scripted mood update");
		//friendship
		if(level > 0)
		{
			speaker.Add("");
			speaker.Add("");
			dialogue.Add(c1.name +"'s mood: +" +10 +"%");
			dialogue.Add(c2.name +"'s mood: +" +10+"%");
			c1.mood += 10;
			c2.mood += 10;
			nullActions.Add("null");
			nullActions.Add("null");
			updateText.material = goodMaterial;
		}
		//rivalry
		else
		{
			speaker.Add("");
			speaker.Add("");
			dialogue.Add(c1.name +"'s mood: -" +10 +"%");
			dialogue.Add (c2.name +"'s mood: -" +10+"%");
			c1.mood -= 10;
			c2.mood -= 10;
			nullActions.Add("null");
			nullActions.Add("null");
			updateText.material = badMaterial;
		}
		
		Debug.Log("Now saving.");save ();
	}

	void showUpdate()
	{
		updatePanel.enabled = true;
		updateText.enabled = true;
	}

	
	void hideUpdate()
	{
		updatePanel.enabled = false;
		updateText.enabled = false;
	}

	//reads from file where a random section of dialogue must be chosen
	void readRandomDialogue(StreamReader file, StreamReader file2, int feedback)
	{
		//skip loop
		for(int i=0; i<feedback-1; i++)
		{
			int tempSize = int.Parse(file.ReadLine());
			//Debug.Log("Skipping "+tempSize +" lines");
			for(int k=0; k<tempSize; k++)
				file.ReadLine();
		}
		
		int size = int.Parse(file.ReadLine());
		if(currentScene==(int)Scene.Feedback && fadeTime==0)
			fadeTime+=size;
		
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
}