using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CharMenuAccuracyScript : MonoBehaviour {

	public Character character;
	
	void Start () {
		Debug.Log (this.gameObject.GetComponent<Text> ().text);
		//CalculateAccuracy ();
	}

	// Set this piece of text to show nothing (because this character is selected).
	public void ClearAccuracy () {
		gameObject.GetComponent<Text> ().text = "--";
	}

	// Set this piece of text to show the character's projected accuracy.
	public void CalculateAccuracy () {
		// Calculate accuracy from mood.
		double tempMood = (double)character.mood;
		double accuracy;
		if (tempMood > 0)
			accuracy = 85.0d + (0.2d * (double)tempMood);
		else if (tempMood < 0)
			accuracy = 85.0d + (0.6d * (double)tempMood);
		else
			accuracy = 85.0d;
		accuracy = Math.Round (accuracy, 0);
		// Set this object to display the accuracy.
		gameObject.GetComponent<Text> ().text = accuracy.ToString ("G0") + "%";
	}
}
