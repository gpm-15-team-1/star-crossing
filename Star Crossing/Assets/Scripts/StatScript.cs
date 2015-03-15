using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatScript : MonoBehaviour {
	public int notes_hit;
	public int number_of_notes;
	public float running_accuracy;
	private Text Score;
	private Text RunningAccuracy;
	public int current_run;

	void Start () {
		notes_hit = 1;
		number_of_notes = 1;
		running_accuracy = 0;
		current_run = 0;
		Score = GameObject.FindWithTag("NotesHitUI").GetComponent<Text>();
		RunningAccuracy = GameObject.FindWithTag("RunningAccuracyUI").GetComponent<Text> (); 
	}

	void Update () {
		running_accuracy = (notes_hit/(float)number_of_notes)*100;
		Score.text = "Notes Hit : " + (notes_hit - 1);
		RunningAccuracy.text = "Accuracy : " + running_accuracy.ToString("f1") + "%"; 
		print ("Current run : " +  current_run);
		//print ("Total notes hit : " + notes_hit);
		//print ("Total notes  : " + number_of_notes);
		//print ("Accuracy : " + running_accuracy);
	}
}
