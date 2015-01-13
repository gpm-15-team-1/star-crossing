using UnityEngine;
using System.Collections;

public class SaveScript : MonoBehaviour {

	//current week, day, scene
	public int currentWeek;
	public int currentDay;
	public int currentScene;

	public Relationship[] relationships;

	// Use this for initialization
	void Start () {
		currentWeek = 1;
		currentDay = 1;
		currentScene = 0;

		/*relationships = new Relationship[10];
		relationships[0].name = "Randall/Julie";
		relationships[1].name = "Randall/Tani";
		relationships[2].name = "Randall/Rocky";
		relationships[3].name = "Randall/Carol";
		relationships[4].name = "Julie/Tani";
		relationships[5].name = "Julie/Rocky";
		relationships[6].name = "Julie/Carol";
		relationships[7].name = "Tani/Rocky";
		relationships[8].name = "Tani/Carol";
		relationships[9].name = "Rocky/Carol";*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
