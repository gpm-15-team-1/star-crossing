using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class NoteReader : MonoBehaviour {

	public GameObject note;
	public GameObject sphere;
	List<GameObject>notes;
	List<GameObject>extraNotes;
	StreamReader noteFile;
	string tempLine;

	// Use this for initialization
	void Start () {
		notes = new List<GameObject>();
		extraNotes = new List<GameObject>();
		noteFile = new StreamReader (Application.dataPath + "/Resources/Files/Feedback.txt");
		string line = noteFile.ReadLine();
		tempLine = line.Substring (0, line.IndexOf(' '));
		noteFile.Close();
		noteFile = new StreamReader(Application.dataPath + "/Resources/Files/Songs/Song1_" + tempLine + ".txt");
		int size = int.Parse(noteFile.ReadLine());

		Vector3 position;
		float startPos = 0.0f;
		//float z = 12.275f;

		for(int i=0; i<size; i++)
		{
			string line1 = noteFile.ReadLine();
			//x
			string item1 = line1.Substring(0, line1.IndexOf(' '));
			line1 = line1.Remove(0,item1.Length+1);
			float x = float.Parse(item1);

			//y
			string item2 = line1.Substring(0, line1.IndexOf(' '));
			line1 = line1.Remove(0,item2.Length+1);
			float y = float.Parse(item2);

			//z
			string item3 = line1;
			float z = float.Parse(item3);


			//speed at which notes move
			z +=(12.5f);
			//gap factor
			z*=2.7f;
			//create position for note
			position = new Vector3(x, y, z);

			//instantiate note at position, parent to this object
			notes.Add(Instantiate(note, position, note.transform.rotation) as GameObject);
			notes[i].name = "Note "+i;
			notes[i].transform.parent = this.transform;
		}

		sphere.transform.position = new Vector3(notes[size-1].transform.position.x, notes[size-1].transform.position.y, ((notes[size-1].transform.position.z)+10.0f));
		noteFile.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
