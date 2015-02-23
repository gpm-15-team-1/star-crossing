using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayOnClick () {
		// load new game (story mode with fresh data)
		Application.LoadLevel ("StoryScene01");
	}

	public void LoadOnClick () {
		// display the load/save menu
	}

	public void OptionsOnClick () {
		// display the options menu
	}

	public void JukeboxOnClick () {
		// load character select screen w/o story info
		Application.LoadLevel ("CharSelectScreen");
	}

	public void GalleryOnClick () {
		// display/load gallery
	}

	public void CinemaOnClick () {
		// display/load cinema
	}

	public void QuitOnClick () {
		// quit the program
		Application.Quit ();
	}
}
