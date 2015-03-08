using UnityEngine;
using System.Collections;

public class SavePromptScript : MonoBehaviour {

	public Canvas promptCanvas;
	public Canvas loadSaveMenuCanvas;
	
	void Start () {
		promptCanvas.enabled = true;
		loadSaveMenuCanvas.enabled = false;
	}

	public void YesOnClick () {
		promptCanvas.enabled = false;
		loadSaveMenuCanvas.enabled = true;
	}

	public void NoOnClick () {
		Application.LoadLevel ("StoryScene01");
	}
}
