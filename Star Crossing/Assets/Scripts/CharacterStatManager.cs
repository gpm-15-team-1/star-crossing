using UnityEngine;
using System.Collections;

public class CharacterStatManager : MonoBehaviour {

	//enums are used to represent each character in Character array
	enum Chars {Randall, Julie, Tani, Nikolai};
	public Character[] characters;

	// Use this for initialization
	void Start () {

		characters[(int)Chars.Randall].page1 = "Part: Bass\nRange: Baritone\nBreakout factor: 1" +
			"\n\nDescription: A music major, \n" +
			"vocal emphasis. He can make\n" +
			"anything sound good, and he’s\n" +
			"very friendly and supportive\n" +
			"to boot.";
		characters[(int)Chars.Randall].page2 = "Randall: --\n" +
			"Julie: 2\n" +
			"Tani: 2\n" +
			"Nikolai: 1";

		characters[(int)Chars.Julie].page1 = "Julie Page 1";
		characters[(int)Chars.Julie].page2 = "Julie Page 2";

		characters[(int)Chars.Tani].page1 = "Tani Page 1";
		characters[(int)Chars.Tani].page2 = "Tani Page 2";

		characters[(int)Chars.Nikolai].page1 = "Nikolai Page 1";
		characters[(int)Chars.Nikolai].page2 = "Nikolai Page 2";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
