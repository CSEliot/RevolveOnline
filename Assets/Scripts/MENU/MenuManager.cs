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
    public GameObject[] lockedCanvases;
    public GameObject[] mapArrows;
    public GameObject[] charArrows;
    
    public bool[] enabledMaps;
    public bool[] enabledChars;

    private GameMaster GM;

    public int[] currentChars = {0, 0, 0, 0};
    public int currentMap = 0;
    
    public const int NUM_CHARS = 2;




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
        mainMenu.SetActive(!mainMenu.activeSelf);   
    }


    //switches to or from the character selection menu
    public void SwitchCharacterSelection()
    {
        //Enable or disable the character selection panel
        charSelect.SetActive(!charSelect.activeSelf);

        //Enable or disable the rotating characters and the "locked" messages
        for (int i = 0; i < 4; i++)
        {
            charModels[currentChars[i]].SetActive(!charModels[currentChars[i]].activeSelf);
            lockedCanvases[i].SetActive(!enabledChars[currentChars[i]] && charSelect.activeSelf);
        }

        GM.SetOrthographic(charSelect.activeSelf); //Enable or disable orthographic camera
    }


    //switches to or from the map selection menu
    public void SwitchMapSelection()
    {
        mapSelect.SetActive(!mapSelect.activeSelf);                                 //Show or hide the map selection menu
        mapModels[currentMap].SetActive(mapSelect.activeSelf);                      //Show or hide the current map
        LockedCanvas.SetActive(!enabledMaps[currentMap] && mapSelect.activeSelf);   //Show or hide the "locked" message

        GM.SetOrthographic(!mapSelect.activeSelf);                                  //Make the camera perspective if in the map menu and orthographic if leaving
    }



    public void LoadButton()
    {
        load.SetActive(true);
    }



    //Selects the next or previous character as the character for that player to start the game with
    public void changeChar(string twoInts)
    {
        //Process parameter
        string temp = twoInts.Substring(0, 2);
        int direction = int.Parse(temp);
        temp = twoInts.Substring(2, 1);
        int player = int.Parse(temp);

        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        
        //Disable the current character model and enable the new one
        charModels[currentChars[player] * 4 + player].SetActive(false);
        currentChars[player] += direction;
        charModels[currentChars[player] * 4 + player].SetActive(true);

        if (enabledChars[currentChars[player]])
        {
            //if player 2, add 1 to character selection
            //Multiple 4*Selected character, such that character 1 = 0 and character 2 = 4 and character 3 = 8
            // so that if player 3 selects the pill, that's: 4*x + y  where x is 0 for pill (character 0) and y is 2 for player 3 
            // (P.1 = 0, P.2 = 1, etc.)
            lockedCanvases[player].SetActive(false);
            GM.selectedCharacters[player] = GM.allPossibleCharacters[currentChars[player] * 4 + player];
        }

        lockedCanvases[player].SetActive(!enabledChars[currentChars[player]]);

        charArrows[player].SetActive(currentChars[player] > 0);
        charArrows[player + charArrows.Length / NUM_CHARS].SetActive(currentChars[player] < charArrows.Length / 5);
    }


    //Selects the next or previous map as the map to start the game with
    public void changeMap(int direction)
    {
        //disable current map model and enable next
        mapModels[currentMap].SetActive(false);
        currentMap += direction;
        mapModels[currentMap].SetActive(true);

        LockedCanvas.SetActive(!enabledMaps[currentMap]);

        ////Enable or disable arrows
        mapArrows[0].SetActive(currentMap > 0);
        mapArrows[1].SetActive(currentMap < mapArrows.Length);
    }



    public void PlayGame()
    {
        if (enabledMaps[currentMap])
        {
            Application.LoadLevel(currentMap + 1);
        }
    }
}
