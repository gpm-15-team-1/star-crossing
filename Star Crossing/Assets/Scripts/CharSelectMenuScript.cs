using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CharSelectMenuScript : MonoBehaviour {

	private GameObject[] highlights;
	private string selectedChar;

	void Start () {
		selectedChar = "";
		highlights = GameObject.FindGameObjectsWithTag ("MenuHighlights");
		foreach (GameObject highlight in highlights) {
			highlight.GetComponent<Image>().color = Color.clear;
		}
		GameObject.Find ("GoButton").GetComponent<Button> ().interactable = false;
	}

	public void CharacterOnClick(string charName) {
		selectedChar = charName;
		string keyLetter = charName.Substring (0, 1);
		Debug.Log ("Key letter is " + keyLetter);
		foreach (GameObject highlight in highlights) {
			if (highlight.name.EndsWith(keyLetter)) {
				highlight.GetComponent<Image>().color = Color.white;
			} else
				highlight.GetComponent<Image>().color = Color.clear;
		}
		GameObject.Find ("GoButton").GetComponent<Button> ().interactable = true;
	}

	public void GoOnClick() {
		StreamWriter file = new StreamWriter(Application.dataPath + "/Resources/Files/Feedback.txt", false);
		file.WriteLine (selectedChar);
		file.Close ();
		Application.LoadLevel ("Rhythm Mode Prototype");
	}
}
