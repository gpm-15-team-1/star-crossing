using UnityEngine;
using System.Collections;

public class Relationship {

	string name;
	//-5 to +5
	int char1Value;
	int char2Value;

	//-30 - +30
	int progress;

	public Relationship()
	{
		name = null;
		char1Value = 0;
		char2Value = 0;
		progress = 0;
	}

	public string getName()
	{
		return name;
	}

	public void setName(string n)
	{
		name = n;
	}

	public int getChar1Value()
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
	}

	public int getProgress()
	{
		return progress;
	}

	public void setProgress(int p)
	{
		progress = p;
	}
}
