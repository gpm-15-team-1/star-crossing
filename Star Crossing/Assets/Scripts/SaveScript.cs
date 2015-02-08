using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveScript : MonoBehaviour {

	//current week, day, scene
	public int currentWeek;
	public int currentDay;
	public int currentScene;

	//relationship values
	public Relationship[] relationships;
	enum Chars {Randall, Julie, Tani, Nikolai};
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
			characters[0].mood = 0;
			characters[1].mood = 0;
			characters[2].mood = 0;
			characters[3].mood = 0;

			/*characters[4].name = "Randall";
			characters[4].lastName = "Creed";
			//assign part and breakout factor
			characters[4].mood = 0;*/
		}
	}

	//streamwriter to save all these values to file
	public void saveFile()
	{
	}

	//streamreader to populate this class once main menu has been created
	public void loadFile()
	{
	}
	
	// Update is called once per frame
	void Update () {
	}
}
