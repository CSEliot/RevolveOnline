using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public GameObject playPanel;
    public GameObject mainMenu;
    public GameObject charSelect;
    public GameObject mapSelect;
    public GameObject load;
    public GameObject lockMapCanvas;

    public GameObject[] charModels;
    public GameObject[] mapModels;
    
    public bool[] enabledMaps;
    public bool[] enabledChars;
    //public GameObject[] selectedChars = new GameObject[4];

    private GameMaster GM;

    public int currentChar = 0;
    public int currentMap = 0;

	// Use this for initialization
	void Start () 
    {
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();

        enabledMaps[0] = true; //basic map cannot be disabled
        enabledMaps[1] = GM._M.GridArena;
        enabledMaps[2] = GM._M.ColumnArena;

        enabledChars[0] = true; //pill character cannot be disabled
        enabledChars[1] = GM._M.robots;
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
            mapModels[currentMap].SetActive(false);
        }
        else if (mapSelect.activeSelf == false)
        {
            mapSelect.gameObject.SetActive(true);
            mapModels[currentMap].SetActive(true);
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

            if (enabledChars[currentChar] == false)
            {
                lockMapCanvas.SetActive(true);
            }
            else
            {
                lockMapCanvas.SetActive(false);
            }

            //disable arrows when you can go left or right any more
        }
    }



    public void PlayGame()
    {
        if (enabledMaps[currentMap])
        {
            Application.LoadLevel(currentMap + 1); 
        }
    }



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

            if (enabledMaps[currentMap] == false)
            {
                lockMapCanvas.SetActive(true);
            }
            else
            {
                lockMapCanvas.SetActive(false);
            }
        }

        
        //set start map to new map
    }
}
