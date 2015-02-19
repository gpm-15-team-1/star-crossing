using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CharSelectMenuScript : MonoBehaviour {

	private GameObject[] highlights;
	private GameObject[] accuracies;
	//private string selectedChar;

	void Start () {
		// Initialize references.
		//selectedChar = "";
		highlights = GameObject.FindGameObjectsWithTag ("MenuHighlights");
		foreach (GameObject highlight in highlights) {
			highlight.GetComponent<Image>().color = Color.clear;
		}
		accuracies = GameObject.FindGameObjectsWithTag ("MenuAccuracies");
		GameObject.Find ("GoButton").GetComponent<Button> ().interactable = false;
	}

	public void CharacterOnClick(string charName) {
		// Set the clicked character as selected!
		//selectedChar = charName;
		string keyLetter = charName.Substring (0, 1);
		// Set highlight of only the clicked character to visible.
		foreach (GameObject highlight in highlights) {
			if (highlight.name.EndsWith(keyLetter)) {
				highlight.GetComponent<Image>().color = Color.white;
			} else
				highlight.GetComponent<Image>().color = Color.clear;
		}
		// Set accuracy of only the clicked character to null.
		foreach (GameObject accuracy in accuracies) {
			if (accuracy.name.EndsWith(keyLetter)) {
				accuracy.GetComponent<CharMenuAccuracyScript>().ClearAccuracy();
			} else
				accuracy.GetComponent<CharMenuAccuracyScript>().CalculateAccuracy();
		}
		// Make "Go!" clickable.
		GameObject.Find ("GoButton").GetComponent<Button> ().interactable = true;
	}

	// Proceed to Rhythm Mode!
	public void GoOnClick() {
		// Before we switch scenes, we must save some data.
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Feedback.txt", false);
		foreach (GameObject accuracy in accuracies) {
			string name = accuracy.GetComponent<CharMenuAccuracyScript>().character.name;
			string acc = accuracy.GetComponent<Text>().text;
			// If acc is blank, that character is being played. Provide mood.
			if (acc == "--") {
				string mood = accuracy.GetComponent<CharMenuAccuracyScript>().character.mood.ToString();
				file.WriteLine("PLAY " + name + " " + mood);
			}
			// If acc is a percent, that character is on autopilot. Provide accuracy.
			else if (acc.EndsWith("%")) {
				file.WriteLine("NPC " + name + " " + acc);
			}
		}
		file.Close ();
		Application.LoadLevel ("Rhythm Mode Prototype");
	}
}
