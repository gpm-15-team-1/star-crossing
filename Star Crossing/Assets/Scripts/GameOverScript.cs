using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{

		Application.LoadLevel("StoryScene01");
	}
}

