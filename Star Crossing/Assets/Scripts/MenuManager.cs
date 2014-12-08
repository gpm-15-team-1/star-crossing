using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	//enums are used to represent each character in Character array
	enum Chars {Blake, Kim, Nancy, Ash};
	public Character[] characters;

	//UI static components
	public GUITexture background;
	public GUITexture leftArrow;
	public GUITexture rightArrow;
	public GUITexture[] characterButtons;
	public GUIText[] characterButtonsText;

	//UI dynamic components
	public GUITexture face;
	public GUIText name;
	public GUIText lastName;
	public GUIText content;

	//tracks which character and page are active
	//and whether a page has been changed or not
	int currentCharacter;
	int currentPage = 1;
	bool pageChange = false;

	// Use this for initialization
	void Start () {

		//initialize the enum
		/*Chars myChars;
		myChars = Chars.Blake;
		currentCharacter = (int)Chars.Blake;*/

		//starting character details
		getCharacter();
		//showMenu();
	}
	
	// Update is called once per frame
	void Update () {
		//show/hide menu
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			getCharacter();
			showMenu();
		}
		//update content if the page has changed
		else if (pageChange == true)
		{
			getContent();
			pageChange = false;
		}
	}

	//enables/disables menu and menu items
	void showMenu()
	{
		background.enabled = !background.enabled;
		rightArrow.enabled = !rightArrow.enabled;
		leftArrow.enabled = !leftArrow.enabled;

		face.enabled = !face.enabled;
		name.enabled = !name.enabled;
		lastName.enabled = !lastName.enabled;
		content.enabled = !content.enabled;

		for(int i=0; i<characterButtons.Length; i++)
		{
			characterButtons[i].enabled = !characterButtons[i].enabled;
			characterButtonsText[i].enabled = !characterButtonsText[i].enabled;
		}
	}

	//changes character face and name
	void getCharacter()
	{
		face.texture = characters[currentCharacter].faceSprite;
		
		name.text = characters[currentCharacter].name;
		name.material = characters[currentCharacter].colour;
		lastName.text = characters[currentCharacter].lastName;

		getContent();
	}

	public void setCurrentCharacter(int i)
	{
		currentCharacter = i;
		getCharacter();
	}

	//changes content of page (description or statistics)
	void getContent()
	{
		if(currentPage==1)
		{
			content.text = GameObject.Find("CharacterStatManager").GetComponent<CharacterStatManager>().characters[currentCharacter].page1;
		}
		else if(currentPage==2)
		{
			content.text = GameObject.Find("CharacterStatManager").GetComponent<CharacterStatManager>().characters[currentCharacter].page2;
		}
	}

	//called by MenuArrowScript
	//changes page depending on which arrow was pressed and states that the page was changed
	void SetCurrentPage(int n)
	{
		currentPage = n;
		pageChange = true;
	}
}
