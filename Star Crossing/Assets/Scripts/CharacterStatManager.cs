using UnityEngine;
using System.Collections;

public class CharacterStatManager : MonoBehaviour {

	//enums are used to represent each character in Character array
	enum Chars {Blake, Kim, Nancy, Ash};
	public Character[] characters;

	// Use this for initialization
	void Start () {

		characters[(int)Chars.Blake].page1 = "Part: Bass\nRange: Baritone\nBreakout factor: 1" +
			"\n\nDescription: A music major, \n" +
			"vocal emphasis. He can make\n" +
			"anything sound good, and he’s\n" +
			"very friendly and supportive\n" +
			"to boot.";
		characters[(int)Chars.Blake].page2 = "Blake: --\n" +
			"Kim: 2\n" +
			"Nancy: 2\n" +
			"Ash: 1";

		characters[(int)Chars.Kim].page1 = "Kim Page 1";
		characters[(int)Chars.Kim].page2 = "Kim Page 2";

		characters[(int)Chars.Nancy].page1 = "Nancy Page 1";
		characters[(int)Chars.Nancy].page2 = "Nancy Page 2";

		characters[(int)Chars.Ash].page1 = "Ash Page 1";
		characters[(int)Chars.Ash].page2 = "Ash Page 2";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
