using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public GameObject playPanel;
    public GameObject mainMenu;
    public GameObject charSelect;
    public GameObject mapSelect;
    public GameObject load;
    public GameObject lockedCanvas;
    public GameObject onlinePanel;
    public GameObject offlinePanel;


    public GameObject[] charModels;
    public GameObject[] mapModels;
    public GameObject[] characterObjects;
    public GameObject[] lockedCanvases;
    public GameObject[] mapArrows;
    public GameObject[] charArrows;
    public GameObject[] charConfirms;
    
    public bool[] enabledMaps;
    public bool[] enabledChars;

    private GameMaster GM;

    public int[] currentChars = {0, 0, 0, 0, 0};
    public int currentMap = 0;
    
    //input stuff
    public string currentMenu = "char";
    string winner = "p1";
    bool[] canSwitchChars = { true, true, true, true };
    bool canSwitchMap = true;
    string player = "";
    bool isOnline = true;

    public const int NUM_CHARS = 2;
    public const int NUM_MAPS = 3;




	// Use this for initialization
	void Start () 
    {      
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        
        //Screen.lockCursor = false;

        enabledMaps[0] = true; //basic map cannot be disabled
        enabledMaps[1] = GM._M.GridArena;
        enabledMaps[2] = GM._M.ColumnArena;

        enabledChars[0] = true; //pill character cannot be disabled
        enabledChars[1] = GM._M.robots;

        currentMenu = "char";
	}
	
	// Update is called once per frame
	void Update () {
        //controller inputs
        if (currentMenu == "char") {
            for (int i = 0; i < 4; i++) {
                player = "p" + (i+1);
                if (Input.GetAxis(player + "_Strafe") < -0.1) {
                    if (canSwitchChars[i]) {
                        changeChar("-1" + i);
                        canSwitchChars[i] = false; 
                    }
                }
                else if (Input.GetAxis(player + "_Strafe") > 0.1) {
                    if (canSwitchChars[i]) {
                        changeChar("+1" + i);
                        canSwitchChars[i] = false; 
                    }
                }
                else {
                    canSwitchChars[i] = true;
                }
                
                if (Input.GetButtonDown(player + "_Jump")) {
                    ConfirmChar(i);
                }

                
            }
            if (Input.GetButtonDown("Start")) {
                bool allConfirmed = true;
                foreach (GameObject button in charConfirms) {
                    if (button.activeSelf == true) {
                        allConfirmed = false;
                    }
                }
                if (allConfirmed) {
                    SwitchCharacterSelection();
                    SwitchMapSelection(); 
                }
            }
            else if (Input.GetButtonDown("Cancel")) {
                SwitchCharacterSelection();
                SwitchMainMenu();
            }
        }
        else if (currentMenu == "map") {
            if (Input.GetAxis(winner + "_Strafe") < -0.1) {
                if (canSwitchMap) {
                    changeMap(-1);
                    canSwitchMap = false; 
                }
            }
            else if (Input.GetAxis(winner + "_Strafe") > 0.1) {
                if (canSwitchMap) {
                    changeMap(1);
                    canSwitchMap = false; 
                }
            }
            else {
                canSwitchMap = true;
            }

            if (Input.GetButtonDown("Start")) {
                PlayGame();
            }
            else if (Input.GetButtonDown("Cancel")) {
                SwitchMapSelection();
                SwitchCharacterSelection();
            }
        }
        else if (currentMenu == "main") {
            if (Input.GetButtonDown("Start")) {
                SwitchMainMenu();
                SwitchCharacterSelection();
            }
        }
	}


    public void PlayOptions()
    {
        playPanel.SetActive(true);
    }


    //switches to or from the main menu
    public void SwitchMainMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        currentMenu = "main";
    }


    //switches to or from the character selection menu
    public void SwitchCharacterSelection()
    {
        //Enable or disable the character selection panel
        charSelect.SetActive(!charSelect.activeSelf);

        if (isOnline) {
            charModels[currentChars[0] + 4].SetActive(!charModels[currentChars[0] + 4].activeSelf);
            lockedCanvas.SetActive(!enabledChars[currentChars[0]] && charSelect.activeSelf);
        }
        else {
            //Enable or disable the rotating characters and the "locked" messages
            for (int i = 0; i < 4; i++) {
                charModels[currentChars[i]].SetActive(!charModels[currentChars[i]].activeSelf);
                lockedCanvases[i].SetActive(!enabledChars[currentChars[i]] && charSelect.activeSelf);
            }
        }
        currentMenu = "char";
        GM.SetOrthographic(charSelect.activeSelf); //Enable or disable orthographic camera
    }

    public void SetOnlineOffline(bool isOn) {
        isOnline = isOn;
        onlinePanel.SetActive(isOnline);
        offlinePanel.SetActive(!isOnline);
    }


    //switches to or from the map selection menu
    public void SwitchMapSelection()
    {
        mapSelect.SetActive(!mapSelect.activeSelf);                                 //Show or hide the map selection menu
        mapModels[currentMap].SetActive(mapSelect.activeSelf);                      //Show or hide the current map
        lockedCanvas.SetActive(!enabledMaps[currentMap] && mapSelect.activeSelf);   //Show or hide the "locked" message

        GM.SetOrthographic(!mapSelect.activeSelf);                                  //Make the camera perspective if in the map menu and orthographic if leaving
        currentMenu = "map";
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

        if (currentChars[player] + direction >= 0 && currentChars[player] + direction < NUM_CHARS) {
            charConfirms[player].SetActive(true);
            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");

            //Disable the current character model and enable the new one
            charModels[currentChars[player] * 4 + player].SetActive(false);
            currentChars[player] += direction;
            charModels[currentChars[player] * 4 + player].SetActive(true);

            lockedCanvases[player].SetActive(!enabledChars[currentChars[player]]);

            //Enable or disable left arrow
            charArrows[player].SetActive(currentChars[player] > 0);
            //Enable or disable right arrow
            charArrows[player + charArrows.Length / (NUM_CHARS)].SetActive(currentChars[player] < charArrows.Length / 5 - 1); 
        }
    }

    public void ConfirmChar(int player) {
        if (enabledChars[currentChars[player]]) {
            //if player 2, add 1 to character selection
            //Multiple 4*Selected character, such that character 1 = 0 and character 2 = 4 and character 3 = 8
            // so that if player 3 selects the pill, that's: 4*x + y  where x is 0 for pill (character 0) and y is 2 for player 3 
            // (P.1 = 0, P.2 = 1, etc.)
            GM.selectedCharacters[player] = GM.allPossibleCharacters[currentChars[player] * 4 + player];
            charConfirms[player].SetActive(false);
        }
    }
    //Selects the next or previous map as the map to start the game with
    public void changeMap(int direction)
    {
        if (currentMap + direction >= 0 && currentMap + direction < NUM_MAPS) {
            //disable current map model and enable next
            mapModels[currentMap].SetActive(false);
            currentMap += direction;
            mapModels[currentMap].SetActive(true);

            lockedCanvas.SetActive(!enabledMaps[currentMap]);

            ////Enable or disable arrows
            mapArrows[0].SetActive(currentMap > 0);
            mapArrows[1].SetActive(currentMap < mapArrows.Length); 
        }
    }



    public void PlayGame()
    {
        if (enabledMaps[currentMap])
        {
			Debug.Log(currentMap);
			GM.currentScene = currentMap + 1;
			Application.LoadLevel(currentMap + 1);
        }
    }
}
