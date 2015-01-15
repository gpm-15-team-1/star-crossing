using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SongManager : MonoBehaviour {

	public Song sampleSong;
	public Canvas textCanvas;
	//public GUIText accuracyText;
	int possiblePoints;
	int acquiredPoints;
	Object theSong;

	public int PossiblePoints
	{
		get { return possiblePoints; }
		set { possiblePoints = value; }
	}

	public int AcquiredPoints
	{
		get { return acquiredPoints; }
		set { acquiredPoints = value; }
	}

	// Use this for initialization
	void Start () {
		theSong = Instantiate (sampleSong);
	}
	
	// Update is called once per frame
	void Update () {
		if (possiblePoints != 0) {
			double accuracy = (double)acquiredPoints / (double)possiblePoints;
			GameObject.Find ("RunningAccuracy").GetComponent<Text> ().text = accuracy.ToString ("P0");
		}
		else {
			GameObject.Find ("RunningAccuracy").GetComponent<Text> ().text = "HI";
		}
	}
}
