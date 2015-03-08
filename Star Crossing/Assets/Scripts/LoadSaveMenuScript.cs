using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class LoadSaveMenuScript : MonoBehaviour {
	
	public Text panel1Time;
	public Button panel1Button;
	public Text panel2Time;
	public Button panel2Button;
	public Text panel3Time;
	public Button panel3Button;
	public Text panel4Time;
	public Button panel4Button;
	public Text panel5Time;
	public Button panel5Button;
	public Text panel6Time;
	public Button panel6Button;

	// Use this for initialization
	void Start () {
		// Redirect to Load or Save functionality depending on current scene.
		if (Application.loadedLevelName == "MainMenu") 
			SetupLoadMenu();
		else if (Application.loadedLevelName == "SaveMenu")
			SetupSaveMenu();
	}

	void SetupLoadMenu () {
		// panel 1 (File 1)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save1.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save1.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel1Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		else {
			// If not, disable that panel's Load
			panel1Button.interactable = false;
		}

		// panel 2 (File 2)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save2.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save2.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel2Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		else {
			panel2Button.interactable = false;
		}

		// panel 3 (File 3)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save3.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save3.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel3Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		else {
			panel3Button.interactable = false;
		}

		// panel 4 (File 4)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save4.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save4.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel4Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		else {
			panel4Button.interactable = false;
		}

		// panel 5 (File 5)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save5.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save5.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel5Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		else {
			panel5Button.interactable = false;
		}

		// panel 6 (Autosave)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save0.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save0.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel6Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		else {
			panel6Button.interactable = false;
		}
	}

	void SetupSaveMenu () {
		// panel 1 (File 1)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save1.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save1.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel1Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
			// Also, maybe come up with some kind of "Are you sure you want to overwrite?" alert?
		}
		
		// panel 2 (File 2)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save2.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save2.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel2Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		
		// panel 3 (File 3)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save3.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save3.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel3Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		
		// panel 4 (File 4)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save4.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save4.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel4Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		
		// panel 5 (File 5)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save5.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save5.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel5Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
		
		// panel 6 (Autosave)
		if (System.IO.File.Exists(Application.dataPath + "/Resources/Files/Saves/Save0.txt")) {
			StreamReader file = new StreamReader(Application.dataPath + "/Resources/Files/Saves/Save0.txt");
			string state = file.ReadLine ();
			string cWeek = state.Substring(0, state.IndexOf(' '));
			state = state.Remove(0,cWeek.Length+1);
			string cDay = state.Substring(0, state.IndexOf(' '));
			panel6Time.text = "Week " + cWeek + ", Day " + cDay;
			file.Close();
		}
	}
	
	public void LoadOnClick (string filename) {
		// overwrite Temp with given file, then load game
		File.Copy (Application.dataPath + "/Resources/Files/Saves/" + filename + ".txt",
		          Application.dataPath + "/Resources/Files/Saves/Temp.txt", true);
		Application.LoadLevel ("StoryScene01");
	}

	public void SaveOnClick (string filename) {
		// overwrite given file with Temp, then load game
		File.Copy (Application.dataPath + "/Resources/Files/Saves/Temp.txt",
		           Application.dataPath + "/Resources/Files/Saves/" + filename + ".txt", true);
		Application.LoadLevel ("StoryScene01");
	}
}
