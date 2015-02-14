using UnityEngine;
using System.Collections;

public class MenuCharacterButtonScript : MonoBehaviour {

	public Character[] characters;
	public GUITexture[] otherButtons;

	public string name;
	public GUIText text;
	// Use this for initialization
	void Start () {
		text.text = name;
		if(name.Equals(characters[0].name))
			this.guiTexture.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		for(int i=0; i<characters.Length; i++)
		{
			if(name.Equals(characters[i].name))
			{
			    GameObject.Find("Menu").GetComponent<MenuManager>().setCurrentCharacter(i);
				this.guiTexture.color = Color.white;
				otherButtons[0].color = Color.gray;
				otherButtons[1].color = Color.gray;
				otherButtons[2].color = Color.gray;
				otherButtons[3].color = Color.gray;
				break;
			}
		}
	}
}
