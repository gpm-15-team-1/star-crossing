using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	enum Moods {HappyMouthClosed, HappyMouthOpen, Worried, Laughing};
	public Sprite[] moodSprites;

	public string name;
	public Material colour;

	// Use this for initialization
	void Start () {
		Moods myMoods;
		myMoods = Moods.HappyMouthClosed;
		//GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.HappyMouthOpen];
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

	void Worried()
	{
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.Worried];
	}

	void Laughing()
	{
		GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.Laughing];
	}

	void Flip()
	{
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}

	void Move(Vector2[] args)
	{
		Vector2 newPos = args[0];
		while(transform.position.x != newPos.x)
			transform.position = Vector2.MoveTowards(transform.position, newPos, 5.0f * Time.deltaTime);
	}

}
