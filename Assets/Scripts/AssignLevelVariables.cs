using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssignLevelVariables : MonoBehaviour {

    private bool nameEnabled;
    private bool killsEnabled;
    private bool healthBarEnabled;
    private bool crossHair;
    private GameMaster.GAME_VALUES GM;
    private bool playerSpawned;
    private bool GUISetup;
    private bool healthbarSetup;


    public GameObject winnerText;
    public GameObject winnerPanel;

    private bool skyEnabled;
    
    

	// Use this for initialization
	void Start () {
        playerSpawned = false;
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>()._M;
        nameEnabled = GM.playername;
        killsEnabled = GM.kills_tracking;
        healthBarEnabled = GM.healthbar;
        crossHair = GM.crosshair;
        skyEnabled = GM.sky;
        if (skyEnabled == false)
        {
            GameObject.Find("Sky").SetActive(false);
        }
        GUISetup = true;
        healthbarSetup = true;
        
        
        bool SniperEnabled = GM.Sniper;
        bool ShotgunEnabled = GM.Shotgun;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Pedestal")){
			if(!GM.Sniper && g.transform.GetChild(0).name == "Gun_Sniper_Rifle"){
				g.transform.GetChild(0).gameObject.SetActive(false);
			}else if( !GM.Shotgun && g.transform.GetChild(0).name == "LAZAR"){
				g.transform.GetChild(0).gameObject.SetActive(false);
			}
        }
	}
	
    public void SetWinnerGUI(string winner){
        winnerPanel.SetActive(true);
        winnerText.SetActive(true);
        Debug.Log("Winner is: " + winner);
        winnerText.GetComponent<Text>().text = winner + " Wins!";
    }

	// Update is called once per frame
	void Update () {
        if (playerSpawned == false && GameObject.FindGameObjectsWithTag("Player").Length > 0)
        {
            //used for functions that want to make sure player has spawned.
            playerSpawned = true;
        }

        if (playerSpawned && healthbarSetup)
        {
            healthbarSetup = false;
            if (healthBarEnabled == false)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
                {
                    g.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    g.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                    g.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    g.transform.GetChild(2).GetChild(3).gameObject.SetActive(false);
                }
            }
        }

        if (playerSpawned && GUISetup)
        {
            GUISetup = false;
            if (crossHair == false){
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
                {
                    g.transform.GetChild(4).GetChild(0).GetChild(4).gameObject.SetActive(false);
                }
            }
            if (killsEnabled == false)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Debug.Log("Disabling kill gui");
                    g.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.SetActive(false);
                    g.transform.GetChild(4).GetChild(0).GetChild(1).gameObject.SetActive(false);
                }
            }
            if (nameEnabled == false)
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Debug.Log("Disabling name gui");
                    g.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
                    g.transform.GetChild(4).GetChild(0).GetChild(3).gameObject.SetActive(false);
                }
            }
        }
	}
}
