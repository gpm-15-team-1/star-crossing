using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	enum Moods {HappyMouthClosed, HappyMouthOpen, Worried, Laughing};
	public Sprite[] moodSprites;
	public Texture faceSprite;

	public string name;
	public string lastName;
	public Material colour;

	public string page1;
	public string page2;

	bool move = false;
	Vector2 newPos = new Vector2(0,0);

	// Use this for initialization
	void Start () {
		Moods myMoods;
		myMoods = Moods.HappyMouthClosed;
		//GetComponent<SpriteRenderer>().sprite = moodSprites[(int)Moods.HappyMouthOpen];
		//Debug.Log(name + ": " +GetComponent<SpriteRenderer>().sortingOrder);
	}
	
	// Update is called once per frame
	void Update () {
		if(move==true)
		{
			//Lerp is considered smoother than MoveTowards, but both work
			if(!transform.position.Equals(newPos))
				transform.position = Vector2.MoveTowards(transform.position, newPos, 8.0f * Time.deltaTime);
				//transform.position = Quaternion.Lerp(transform.position, newPos, 8.0f * Time.deltaTime);
				else
				move = false;
			//move = false;
		}
	
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
		newPos = args[0];
		move = true;
	}

}
