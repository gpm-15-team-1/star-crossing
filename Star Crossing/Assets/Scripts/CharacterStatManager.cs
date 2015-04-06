using UnityEngine;
using System.Collections;

public class CharacterStatManager : MonoBehaviour {

	//enums are used to represent each character in Character array
	enum Chars {Randall, Julie, Tani, Nikolai, Carol};
	public Character[] characters;
	public Relationship[] relationships;

	// Use this for initialization
	void Start () {

		characters = new Character[6];
		relationships = new Relationship[10];
		for(int i=0; i<10; i++)
			relationships[i] = new Relationship();
		//make version that reads from temp file

		//reading from save object
		for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().characters.Length; i++)
		{
			characters[i] = GameObject.Find("Save").GetComponent<SaveScript>().characters[i];
		}

		Debug.Log("Number of relationships: "+GameObject.Find("Save").GetComponent<SaveScript>().relationships.Length);
		//reading from save object
		for(int i=0; i<GameObject.Find("Save").GetComponent<SaveScript>().relationships.Length; i++)
		{
			relationships[i] = GameObject.Find("Save").GetComponent<SaveScript>().relationships[i];
		}

		//incorporate mood bar
		/*characters[(int)Chars.Randall].page1 = "Part: Bass\nRange: Baritone\nBreakout factor: 1" +
			"\n\nDescription: A music major, \n" +
			"vocal emphasis. He can make\n" +
			"anything sound good, and he’s\n" +
			"very friendly and supportive\n" +
			"to boot.";
		characters[(int)Chars.Randall].page2 = 
		"Randall: --\n" +
		"Julie: "+getRelationshipByName("Randall/Julie").getChar1Value()+"\n" +
		"Tani: "+getRelationshipByName("Randall/Tani").getChar1Value()+"\n" +
		"Nikolai: "+getRelationshipByName("Randall/Nikolai").getChar1Value()+"\n" +
		"Carol: "+getRelationshipByName("Randall/Carol").getChar1Value() +"\n\n" +
		"Mood: "+characters[(int)Chars.Randall].mood;

		characters[(int)Chars.Julie].page1 = "Julie Page 1";
		characters[(int)Chars.Julie].page2 =
		"Randall: "+getRelationshipByName("Randall/Julie").getChar2Value()+"\n" +
		"Julie: --\n" +
		"Tani: "+getRelationshipByName("Julie/Tani").getChar1Value()+"\n" +
		"Nikolai: "+getRelationshipByName("Julie/Nikolai").getChar1Value()+"\n" +
		"Carol: "+getRelationshipByName("Julie/Carol").getChar1Value()+"\n\n" +
		"Mood: "+characters[(int)Chars.Julie].mood;

		characters[(int)Chars.Tani].page1 = "Tani Page 1";
		characters[(int)Chars.Tani].page2 =
		"Randall: "+getRelationshipByName("Randall/Tani").getChar2Value()+"\n" +
		"Julie: "+getRelationshipByName("Julie/Tani").getChar2Value()+"\n" +
		"Tani: --\n" +
		"Nikolai: "+getRelationshipByName("Nikolai/Tani").getChar2Value()+"\n" +
		"Carol: "+getRelationshipByName("Tani/Carol").getChar1Value()+"\n\n" +
		"Mood: "+characters[(int)Chars.Tani].mood;

		characters[(int)Chars.Nikolai].page1 = "Nikolai Page 1";
		characters[(int)Chars.Nikolai].page2 = 
		"Randall: "+getRelationshipByName("Randall/Nikolai").getChar2Value()+"\n" +
		"Julie: "+getRelationshipByName("Julie/Nikolai").getChar2Value()+"\n" +
		"Tani: "+getRelationshipByName("Nikolai/Tani").getChar1Value()+"\n" +
		"Nikolai: --\n" +
		"Carol: "+getRelationshipByName("Nikolai/Carol").getChar1Value()+"\n\n" +
		"Mood: "+characters[(int)Chars.Nikolai].mood;

		characters[(int)Chars.Carol].page1 = "Carol Page 1";
		characters[(int)Chars.Carol].page2 = 
		"Randall: "+getRelationshipByName("Randall/Carol").getChar2Value()+"\n" +
		"Julie: "+getRelationshipByName("Julie/Carol").getChar2Value()+"\n" +
		"Tani: "+getRelationshipByName("Tani/Carol").getChar2Value()+"\n" +
		"Nikolai: "+getRelationshipByName("Nikolai/Carol").getChar2Value()+"\n" +
		"Carol: --"+"\n\n" +
		"Mood: "+characters[(int)Chars.Carol].mood;
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
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
