using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public GameObject playPanel;
    public GameObject mainMenu;
    public GameObject charSelect;
    public GameObject mapSelect;
    public GameObject load;
    public GameObject LockedCanvas;


    public GameObject[] charModels;
    public GameObject[] mapModels;
    public GameObject[] characterObjects;
    
    public bool[] enabledMaps;
    public bool[] enabledChars;
    //public GameObject[] selectedChars = new GameObject[4];

    private GameMaster GM;

    public int[] currentChars = {0, 0, 0, 0};
    public int currentMap = 0;


	// Use this for initialization
	void Start () 
    {      
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        
        Screen.lockCursor = false;

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
        for (int i = 0; i < 4; i++)
        {
            if (charModels[currentChars[i]].activeSelf)
            {
                charModels[currentChars[i]].gameObject.SetActive(false);
            }
            else if (charModels[currentChars[i]].activeSelf == false)
            {
                charModels[currentChars[i]].gameObject.SetActive(true);
            } 
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
    public void changeChar(string twoInts)
    {
        string temp = twoInts.Substring(0, 2);
        int direction = int.Parse(temp);
        temp = twoInts.Substring(2, 1);
        int player = int.Parse(temp);

        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        
        //Disable the current character model and enable the new one
        if (currentChars[player] + direction >= 0 && currentChars[player] + direction < charModels.Length / 5)
        {
            charModels[currentChars[player] * 4 + player].SetActive(false);
            currentChars[player] += direction;
            charModels[currentChars[player] * 4 + player].SetActive(true);
            //selectedChars[0] = playerList[currentChar];

            if (enabledChars[currentChars[player]])
            {
                //if player 2, add 1 to character selection
                //Multiple 4*Selected character, such that character 1 = 0 and character 2 = 4 and character 3 = 8
                // so that if player 3 selects the pill, that's: 4*x + y  where x is 0 for pill (character 0) and y is 2 for player 3 
                // (P.1 = 0, P.2 = 1, etc.)
                LockedCanvas.SetActive(false);
                GM.selectedCharacters[player] = GM.allPossibleCharacters[currentChars[player] * 4 + player];
            }
            else
            {
                LockedCanvas.SetActive(true);
            }

            //disable arrows when you can't go left or right any more
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
                LockedCanvas.SetActive(true);
            }
            else
            {
                LockedCanvas.SetActive(false);
            }
        }

            //disable arrows
    }



    public void PlayGame()
    {
        if (enabledMaps[currentMap])
        {
            Application.LoadLevel(currentMap + 1);
        }
    }
}
