using UnityEngine;
using System.Collections;
using System.IO;

public class GameOverScript : MonoBehaviour {
	public GameObject Cam;
	StreamWriter filew;
	StreamReader filer;

	void Awake() {
		Cam = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	void OnTriggerEnter(Collider other)
	{
		filer = new StreamReader (Application.dataPath + "/Resources/Files/Feedback.txt");
		string line = filer.ReadLine();
		string tempLine = line.Substring (0, line.IndexOf(' '));
		string[] others = new string[4];
		for (int i=0; i<4; i++) {
			others[i] = filer.ReadLine();
				}
		filer.Close ();
		filew = new StreamWriter (Application.dataPath + "/Resources/Files/Feedback.txt", false);
		filew.WriteLine (tempLine + " " + Cam.GetComponent<StatScript>().running_accuracy);
		for (int i=0; i<4; i++) {
			filew.WriteLine(others[i]);
		}
		filew.Close ();
		//GameObject.Find ("Save").GetComponent<SaveScript>().currentScene++;
		Application.LoadLevel("StoryScene01");
	}
}
