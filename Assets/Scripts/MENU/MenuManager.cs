using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public GameObject playPanel;
    public GameObject mainMenu;
    public GameObject charSelect;
    public GameObject mapSelect;
    public GameObject load;

    public GameObject[] charModels;
    public GameObject[] mapModels;
    //public GameObject[] selectedChars = new GameObject[4];


    public int currentChar = 0;
    public int currentMap = 0;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void PlayOptions()
    {
        playPanel.SetActive(true);
    }

    //switches to or from the main menu
    public void SwitchMainMenu()
    {
        if (mainMenu.activeSelf)
        {
            mainMenu.gameObject.SetActive(false);
        }
        else if (mainMenu.activeSelf == false)
        {
            mainMenu.gameObject.SetActive(true);
        }
    }


    //switches to or from the character selection menu
    public void SwitchCharacterSelection()
    {
        //Enable or disable the character selection panel
        if (charSelect.activeSelf)
        {
            charSelect.gameObject.SetActive(false);
        }
        else if (charSelect.activeSelf == false)
        {
            charSelect.gameObject.SetActive(true);
        }

        //Enable or disable the rotating character
        if (charModels[currentChar].activeSelf)
        {
            charModels[currentChar].gameObject.SetActive(false);
        }
        else if (charModels[currentChar].activeSelf == false)
        {
            charModels[currentChar].gameObject.SetActive(true);
        }
    }



    //switches to or from the map selection menu
    public void SwitchMapSelection()
    {
        if (mapSelect.activeSelf)
        {
            mapSelect.gameObject.SetActive(false);
        }
        else if (mapSelect.activeSelf == false)
        {
            mapSelect.gameObject.SetActive(true);
        }
    }

    public void LoadButton()
    {
        load.SetActive(true);
    }



    //Selects the next or previous character as the character for that player to start the game with
    public void changeChar(int direction)
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        
        //Enable disable the current character model and enable the new one
        if (currentChar + direction >= 0 && currentChar + direction < charModels.Length)
        {
            charModels[currentChar].SetActive(false);
            currentChar += direction;
            charModels[currentChar].SetActive(true);
            //selectedChars[0] = playerList[currentChar];
        }
    }

    //public void PlayGame()
    //{

    //}

    //Selects the next or previous map as the map to start the game with
    public void changeMap(int direction)
    {
        //disable current map model and enable next
        if (currentMap + direction >= 0 && currentMap + direction < mapModels.Length)
        {
            mapModels[currentMap].SetActive(false);
            currentMap += direction;
            mapModels[currentMap].SetActive(true);
            //selectedChars[0] = playerList[currentChar];
        }

        //set start map to new map
    }
}
