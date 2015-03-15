using UnityEngine;
using System.Collections;
using System.IO;

public class AudioManager : MonoBehaviour {
	public AudioSource[] Stems;
	StreamReader filer;
	private int i;
	public int char_select;
	// Use this for initialization
	void Start () {
		Stems = gameObject.GetComponents<AudioSource> ();
		for (i = 0; i<Stems.Length; i++) {
			Stems[i].volume = 0.3f;
			Stems[i].PlayDelayed(4.25f);}
		i = 0;
		filer = new StreamReader (Application.dataPath + "/Resources/Files/Feedback.txt");
		string line = filer.ReadLine();
		string tempLine = line.Substring (0, line.IndexOf(' '));

		switch (tempLine) {
			case "Carol" : char_select = 1; break;
			case "Julie" : char_select = 2; break;
			case "Nikolai" : char_select = 3; break;	
			case "Randall" : char_select = 4; break;	
			case "Tani" : char_select = 5; break;
		}
		Stems [char_select].volume = 1.0f;
		filer.Close ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Kill()
	{
		SendMessage("StopSound");
	}

	IEnumerator StopSound()
	{
		Stems[char_select].volume = 0.0f;
		yield return new WaitForSeconds (0.75f);
		Stems[char_select].volume = 1.0f;
		Debug.Log("Muting");
	}
}
