using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	enum Moods {HappyMouthClosed, HappyMouthOpen, Upset, Laughing};
	public Sprite[] moodSprites;

	public string name;
	public Material colour;

	// Use this for initialization
	void Start () {
		Moods myMoods;
		myMoods = Moods.HappyMouthClosed;
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.HappyMouthClosed];
		//Debug.Log(name + ": " +GetComponent<SpriteRenderer>().sortingOrder);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MouthOpen()
	{
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.HappyMouthOpen];
	}

	void MouthClosed()
	{
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.HappyMouthClosed];
	}

	void Upset()
	{
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.Upset];
	}

	void Laughing()
	{
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.Laughing];
	}
}
