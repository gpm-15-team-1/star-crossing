using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	enum Chars {Blake, Kim, Nancy, Ash};
	enum Pos {LeftFront, LeftBack, RightFront, RightBack};

	string[] dialogue;
	string[] speaker;

	public GUIText name;
	public GUIText line;
	int currentIndex;

	public Character[] characters;
	Vector2[] positions;

	// Use this for initialization
	void Start () {

		Chars myChars;
		myChars = Chars.Blake;

		Pos myPos;
		myPos = Pos.LeftFront;

		positions = new Vector2[4];
		positions[0] = new Vector2(-4, 0);
		positions[1] = new Vector2(-2, 0);
		positions[2] = new Vector2(2, 0);
		positions[3] = new Vector2(4, 0);

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
	}
}
