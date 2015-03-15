using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour{

	//visualisation
	//enum Moods {HappyMouthClosed, HappyMouthOpen, Worried, Laughing, Sad, Angry, Curious};
	enum Moods {Neutral, Happy, Sad, Angry, Confused, Thinking};
	enum Variations {MouthClosed, MouthOpen};
	int currentMood;
	//default
	public Sprite[] moodSpritesClosed;
	public Sprite[] moodSpritesOpen;
	public Texture faceSprite;

	//static facts
	public string name;
	public string lastName;
	public string part;
	public string breakoutFactor;
	public Material colour;
	
	public string page1;
	public string page2;
	
	//-50 to +50 CHANGES
	public int mood;

	bool move = false;
	public string facing = null;
	string currentPos = null;
	Vector2 newPos = new Vector2(0,0);


	// Use this for initialization
	void Start () {
		Moods myMoods;
		myMoods = Moods.Neutral;
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
		GetComponent<SpriteRenderer>().sprite = moodSpritesOpen[currentMood];
	}

	void MouthClosed()
	{
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Neutral()
	{
		currentMood = (int)Moods.Neutral;
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Happy()
	{
		currentMood = (int)Moods.Happy;
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Sad()
	{
		currentMood = (int)Moods.Sad;
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Angry()
	{
		currentMood = (int)Moods.Angry;
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Confused()
	{
		currentMood = (int)Moods.Confused;
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Thinking()
	{
		currentMood = (int)Moods.Thinking;
		GetComponent<SpriteRenderer>().sprite = moodSpritesClosed[currentMood];
	}

	void Flip()
	{
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}

	void Move1Left()
	{
		newPos = new Vector2(transform.position.x-2.5f, transform.position.y);
		if(currentPos.Equals("RightBack"))
		   newPos = new Vector2(-2f, transform.position.y);
		move = true;

		if (newPos.x == -7f)
		{
			currentPos = "LeftFront";
			this.renderer.sortingOrder = 0;
		}
		else if (newPos.x == -4.5f)
		{
			currentPos = "LeftMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == -2f)
		{
			currentPos = "LeftBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 2f)
		{
			currentPos = "RightBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 4.5f)
		{
			currentPos = "RightMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == 7f)
		{
			currentPos = "RightFront";
			this.renderer.sortingOrder = 0;
		}
	}

	void Move1Right()
	{
		newPos = new Vector2(transform.position.x+2.5f, transform.position.y);
		if(currentPos.Equals("LeftBack"))
			newPos = new Vector2(2f, transform.position.y);
		move = true;

		if (newPos.x == -7f)
		{
			currentPos = "LeftFront";
			this.renderer.sortingOrder = 0;
		}
		else if (newPos.x == -4.5f)
		{
			currentPos = "LeftMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == -2f)
		{
			currentPos = "LeftBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 2f)
		{
			currentPos = "RightBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 4.5f)
		{
			currentPos = "RightMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == 7f)
		{
			currentPos = "RightFront";
			this.renderer.sortingOrder = 0;
		}
	}

	void Move2Left()
	{
		newPos = new Vector2(transform.position.x-5, transform.position.y);
		if(currentPos.Equals("RightMid"))
			newPos = new Vector2(-2f, transform.position.y);
		else if(currentPos.Equals("RightBack"))
			newPos = new Vector2(-4.5f, transform.position.y);

		move = true;

		if (newPos.x == -7f)
		{
			currentPos = "LeftFront";
			this.renderer.sortingOrder = 0;
		}
		else if (newPos.x == -4.5f)
		{
			currentPos = "LeftMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == -2f)
		{
			currentPos = "LeftBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 2f)
		{
			currentPos = "RightBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 4.5f)
		{
			currentPos = "RightMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == 7f)
		{
			currentPos = "RightFront";
			this.renderer.sortingOrder = 0;
		}
	}

	void Move2Right()
	{
		newPos = new Vector2(transform.position.x+5, transform.position.y);
		if(currentPos.Equals("LeftMid"))
			newPos = new Vector2(2f, transform.position.y);
		else if(currentPos.Equals("LeftBack"))
			newPos = new Vector2(4.5f, transform.position.y);
		move = true;

		if (newPos.x == -7f)
		{
			currentPos = "LeftFront";
			this.renderer.sortingOrder = 0;
		}
		else if (newPos.x == -4.5f)
		{
			currentPos = "LeftMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == -2f)
		{
			currentPos = "LeftBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 2f)
		{
			currentPos = "RightBack";
			this.renderer.sortingOrder = -2;
		}
		else if (newPos.x == 4.5f)
		{
			currentPos = "RightMid";
			this.renderer.sortingOrder = -1;
		}
		else if (newPos.x == 7f)
		{
			currentPos = "RightFront";
			this.renderer.sortingOrder = 0;
		}
	}

	//move on screen, from the right side
	void MoveOnScreenRight()
	{
		if(facing == "right")
		{
			Flip();
			facing = "left";
		}
		transform.position = new Vector2(15.0f, transform.position.y);
		newPos = new Vector2(7f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = 0;
		currentPos = "RightFront";
	}

	//move to LeftMid
	void MoveLeftMid()
	{
		if(facing == "left")
		{
			Flip();
			facing = "right";
		}
		transform.position = new Vector2(-15.0f, transform.position.y);
		newPos = new Vector2(-4.5f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = -1;
		currentPos = "LeftMid";
	}
	
	//move to LeftBack
	void MoveLeftBack()
	{
		if(facing == "left")
		{
			Flip();
			facing = "right";
		}
		transform.position = new Vector2(-15.0f, transform.position.y);
		newPos = new Vector2(-2f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = -2;
		currentPos = "LeftBack";
	}

	//move on screen, from the left side
	void MoveOnScreenLeft()
	{
		if(facing == "left")
		{
			Flip();
			facing = "right";
		}
		transform.position = new Vector2(-15.0f, transform.position.y);
		newPos = new Vector2(-7f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = 0;

		currentPos = "LeftFront";
	}

	//move to RightMid
	void MoveRightMid()
	{
		if(facing == "right")
		{
			Flip();
			facing = "left";
		}
		transform.position = new Vector2(15.0f, transform.position.y);
		newPos = new Vector2(4.5f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = -1;
		currentPos = "RightMid";
	}
	
	//move to RightBack
	void MoveRightBack()
	{
		if(facing == "right")
		{
			Flip();
			facing = "left";
		}
		transform.position = new Vector2(15.0f, transform.position.y);
		newPos = new Vector2(2f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = -2;
		currentPos = "RightBack";
	}

	void MoveOffScreenLeft()
	{
		newPos = new Vector2(-15f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = 0;

		currentPos = null;
	}

	void MoveOffScreenRight()
	{
		newPos = new Vector2(15f, transform.position.y);
		move = true;
		this.renderer.sortingOrder = 0;

		currentPos = null;
	}

	void RightFront()
	{
		if(facing == "right")
		{
			Flip();
			facing = "left";
		}
		
		currentPos = "RightFront";
		transform.position = new Vector2(7f, transform.position.y);
		this.renderer.sortingOrder = 0;
	}

	void RightMid()
	{
		if(facing == "right")
		{
			Flip();
			facing = "left";
		}
		
		currentPos = "RightMid";
		transform.position = new Vector2(4.5f, transform.position.y);
		this.renderer.sortingOrder = -1;
	}

	void RightBack()
	{
		if(facing == "right")
		{
			Flip();
			facing = "left";
		}
		
		currentPos = "RightBack";
		transform.position = new Vector2(2f, transform.position.y);
		this.renderer.sortingOrder = -2;
	}

	void LeftFront()
	{
		if(facing == "left")
		{
			Flip();
			facing = "right";
		}
		
		currentPos = "LeftFront";
		transform.position = new Vector2(-7f, transform.position.y);
		this.renderer.sortingOrder = 0;
	}

	void LeftMid()
	{
		if(facing == "left")
		{
			Flip();
			facing = "right";
		}
		currentPos = "LeftMid";
		transform.position = new Vector2(-4.5f, transform.position.y);
		this.renderer.sortingOrder = -1;
	}

	void LeftBack()
	{
		if(facing == "left")
		{
			Flip();
			facing = "right";
		}
		currentPos = "LeftBack";
		transform.position = new Vector2(-2f, transform.position.y);
		this.renderer.sortingOrder = -2;
	}
}
