using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
	
	string[] dialogue;
	string[] speaker;

	public GUIText name;
	public GUIText line;
	int currentIndex;

	public Character[] characters;

	// Use this for initialization
	void Start () {
		dialogue = new string[14];
		speaker = new string[14];

		{
			speaker[0] = "Blake";
			speaker[1] = "Kim";
			speaker[2] = "Blake";
			speaker[3] = "Blake";
			speaker[4] = "Nancy";
			speaker[5] = "???";
			speaker[6] = "Ash";
			speaker[7] = "Ash";
			speaker[8] = "Blake";
			speaker[9] = "Nancy";
			speaker[10] = "Ash";
			speaker[11] = "Ash";
			speaker[12] = "Kim";
			speaker[13] = "Blake";
		}

		{
			dialogue[0] = "So, are we all here?";
			dialogue[1] = "I'm here...but you knew that already.";
			dialogue[2] = "Haha! Alright, raise your hand if you aren't here!";
			dialogue[3] = "Seriously, though. Nancy, did you bring Ash?";
			dialogue[4] = "Oh, I thought he was right behind me...";
			dialogue[5] = "I'm here!";
			dialogue[6] = "Terribly sorry! I wanted to be sure my voice bank was properly calibrated.";
			dialogue[7] = "Reporting for duty, sir!";
			dialogue[8] = "Great to see you too, Ash.";
			dialogue[9] = "I'm glad you're still up for this.";
			dialogue[10] = "Of course! I'm happy to be of assistance!";
			dialogue[11] = "And it's a prime opportunity to test my latest syllable samples.";
			dialogue[12] = "Awesome, so are we all ready? Feeling good? Try pressing ESC to take a look.";
			dialogue[13] = "Alright, let's do this!";
		}

		currentIndex = 0;
		name.text = speaker[currentIndex];
		line.text = dialogue[currentIndex];
		changeSprites();
		//changeColour(speaker[currentIndex]);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(currentIndex+1 < dialogue.Length)
			{
				currentIndex++;
				changeSprites();
				name.text = speaker[currentIndex];
				changeColour(speaker[currentIndex]);
				line.text = dialogue[currentIndex];
				//AudioSettings.dspTime for future reference
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
		if(currentIndex==0)
		{
			characters[0].gameObject.SendMessage("MouthOpen");
		}
		if(currentIndex==1)
		{
			
		}
		if(currentIndex==2)
		{
			
		}
		if(currentIndex==3)
		{
			
		}
		if(currentIndex==4)
		{
			
		}
		if(currentIndex==5)
		{
			
		}
		if(currentIndex==6)
		{
			
		}
		if(currentIndex==7)
		{
			
		}
		if(currentIndex==8)
		{
			
		}
		if(currentIndex==9)
		{
			
		}
		if(currentIndex==10)
		{
			
		}
		if(currentIndex==11)
		{
			
		}
		if(currentIndex==12)
		{
			
		}
		if(currentIndex==13)
		{
			
		}
	}
}
