using UnityEngine;
using System.Collections;

public class Relationship {

	string name;
	//0 OR 1 DOES NOT CHANGE UNLESS REDEMPTION
	/*int char1Value;
	int char2Value;*/

	//-30 - +30 CHANGES
	int status;
	int posProgress;
	int negProgress;

	static Queue topics;
	bool[] spoken = new bool[5];

	public Relationship()
	{
		name = null;
		/*char1Value = 1;
		char2Value = 1;*/
		status = 0;
		topics = new Queue();

		for(int i=0; i<spoken.Length; i++)
			spoken[i] = false;
	}

	public string getName()
	{
		return name;
	}

	public void setName(string n)
	{
		name = n;
	}

	/*public int getChar1Value()
	{
		return char1Value;
	}

	public int getChar2Value()
	{
		return char2Value;
	}

	public void setChar1Value(int value)
	{
		char1Value = value;
	}

	public void setChar2Value(int value)
	{
		char2Value = value;
	}*/

	public int getProgress()
	{
		return status;
	}

	public void setProgress(int p)
	{
		status = p;
	}

	public int getPosProgress()
	{
		return posProgress;
	}

	public void setPosProgress(int p)
	{
		posProgress = p;
	}

	public int getNegProgress()
	{
		return negProgress;
	}

	public void setNegProgress(int p)
	{
		negProgress = p;
	}

	public static void addTopic(string t, Relationship[] r)
	{
		if(topics.Count==5)
		{
			topics.Dequeue();
			for(int i=0; i<r.Length; i++)
			{
				r[i].spoken[0] = r[i].spoken[1];
				r[i].spoken[1] = r[i].spoken[2];
				r[i].spoken[2] = r[i].spoken[3];
				r[i].spoken[3] = r[i].spoken[4];
			}
		}
		topics.Enqueue(t);
		for(int i=0; i<r.Length; i++)
			r[i].spoken[topics.Count-1] = false;
		Debug.Log("Size of topics is now: "+topics.Count);
		Debug.Log("Added topic is: "+t);
	}

	public static Queue getTopics()
	{
		return topics;
	}

	public bool[] getSpoken()
	{
		return spoken;
	}
}
