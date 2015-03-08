﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class MainMenuScript : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas loadSaveMenu;

	// Use this for initialization
	void Start () {
		mainMenu.enabled = true;
		loadSaveMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayOnClick () {
		// load new game (story mode with fresh data)
		File.Delete (Application.dataPath + "/Resources/Files/Saves/Temp.txt");
		Application.LoadLevel ("StoryScene01");
	}

	public void LoadOnClick () {
		// display the load/save menu
		mainMenu.enabled = false;
		loadSaveMenu.enabled = true;
	}

	public void BackOnClick () {
		// returns from load/save menu to main menu
		mainMenu.enabled = true;
		loadSaveMenu.enabled = false;
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
