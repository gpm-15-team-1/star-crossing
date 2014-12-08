using UnityEngine;
using System.Collections;

public class MenuArrowScript : MonoBehaviour {

	public GUITexture otherArrow;


	// Use this for initialization
	void Start () {
		guiTexture.color = Color.white;
		if(name == "Menu_LeftArrow")
			guiTexture.color = Color.gray;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		guiTexture.color = Color.gray;
		otherArrow.color = Color.white;

		if(name == "Menu_RightArrow")
		{
			GameObject.Find("Menu").GetComponent<MenuManager>().SendMessage("SetCurrentPage", 2);
		}
		else if(name == "Menu_LeftArrow")
		{
			GameObject.Find("Menu").GetComponent<MenuManager>().SendMessage("SetCurrentPage", 1);
		}
	}
}
