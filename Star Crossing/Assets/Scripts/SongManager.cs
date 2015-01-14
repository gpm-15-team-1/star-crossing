using UnityEngine;
using System.Collections;

public class SongManager : MonoBehaviour {

	public Song sampleSong;
	public Canvas textCanvas;
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
		double accuracy = (double)acquiredPoints / (double)possiblePoints;
	}
}
