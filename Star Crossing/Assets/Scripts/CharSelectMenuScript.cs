using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CharSelectMenuScript : MonoBehaviour {

	public GameObject[] highlights;
	public GameObject[] accuracies;
	private int selectedChar;

	void Start () {
		selectedChar = -1;
		foreach (GameObject highlight in highlights) {
			highlight.GetComponent<Image>().color = Color.clear;
		}
		GameObject.Find ("GoButton").GetComponent<Button> ().interactable = false;
	}

	public void CharacterOnClick(int whichChar) {
		selectedChar = whichChar;
		// Set highlight of only the clicked character to visible.
		for (int i=0; i<5; i++) {
			// If the current index matches whichChar, highlight and clear accuracy.
			if (i == whichChar){
				highlights[i].GetComponent<Image>().color = Color.white;
				accuracies[i].GetComponent<CharMenuAccuracyScript>().ClearAccuracy();
			}
			// Otherwise, hide highlight and display accuracy normally.
			else {
				highlights[i].GetComponent<Image>().color = Color.clear;
				accuracies[i].GetComponent<CharMenuAccuracyScript>().CalculateAccuracy();
			}
		}
		// Make "Go!" clickable.
		GameObject.Find ("GoButton").GetComponent<Button> ().interactable = true;
	}

	// Proceed to Rhythm Mode!
	public void GoOnClick() {
		// Before we switch scenes, we must save some data.
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Feedback.txt", false);
		// First pass: write line with the selected character and his/her mood
		for (int i=0; i<5; i++) {
			if (i == selectedChar) {
				string name = accuracies [i].GetComponent<CharMenuAccuracyScript> ().character.name;
				string mood = accuracies [i].GetComponent<CharMenuAccuracyScript> ().character.mood.ToString ();
				file.WriteLine (name + " " + mood);
			}
		}
		// Second pass: write other characters and their calculated accuracies
		for (int i=0; i<5; i++) {
			if (i != selectedChar) {
				string name = accuracies [i].GetComponent<CharMenuAccuracyScript> ().character.name;
				string acc = accuracies[i].GetComponent<Text>().text;
				file.WriteLine (name + " " + acc.Substring(0, acc.Length-1));
			}
		}
		file.Close ();
		// Now we can switch scenes!
		Application.LoadLevel ("Rhythm Mode Prototype");
	}
}
