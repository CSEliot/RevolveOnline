using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillStreak : MonoBehaviour {

	private int p1Kills;
	private int p2Kills;
	private int p3Kills;
	private int p4Kills;

    public GameObject Pedestal; //saving here cuz i'm lazy

    private string winner;
    private int maxKills;
    private GameMaster.GAME_VALUES GM;


	public void addKill(string killer) {

		if (killer == "Player 1") {
			p1Kills ++;
            if (GM.kills_tracking)
            {
                GameObject.Find("GUI Camera 1").transform.GetChild(0).GetChild(1).
                    GetComponent<Text>().text = "Kills: " + p1Kills;
            }
		}
		else if (killer == "Player 2") {
			p2Kills ++;
            if (GM.kills_tracking)
            {
                GameObject.Find("GUI Camera 2").transform.GetChild(0).GetChild(1).
                    GetComponent<Text>().text = "Kills: " + p2Kills;
            }
		}

		else if (killer == "Player 3") {
			p3Kills ++;
            if (GM.kills_tracking)
            {
                GameObject.Find("GUI Camera 3").transform.GetChild(0).GetChild(1).
                    GetComponent<Text>().text = "Kills: " + p3Kills;
            }
		}

        else if (killer == "Player 4")
        {
            p4Kills++;
            if (GM.kills_tracking)
            {
                GameObject.Find("GUI Camera 4").transform.GetChild(0).GetChild(1).
                    GetComponent<Text>().text = "Kills: " + p4Kills;
            }
        }
        else
        {
            Manager.say("AddKill function got bad name: " + killer, "always");
        }
	}

    public string GetWinner()
    {
        if (p1Kills > maxKills)
        {
            maxKills = p1Kills;
            winner = "Player 1";
        }
        if (p2Kills > maxKills)
        {
            maxKills = p2Kills;
            winner = "Player 2";
        }
        if (p3Kills > maxKills)
        {
            maxKills = p3Kills;
            winner = "Player 3";
        }
        if (p4Kills > maxKills)
        {
            maxKills = p4Kills;
            winner = "Player 4";
        }
        if (maxKills == 0)
        {
            return "No One";
        }
        return winner;
    }

    public int GetWinningKills()
    {
        if (p1Kills > maxKills)
        {
            maxKills = p1Kills;
            winner = "Player 1";
        }
        if (p2Kills > maxKills)
        {
            maxKills = p2Kills;
            winner = "Player 2";
        }
        if (p3Kills > maxKills)
        {
            maxKills = p3Kills;
            winner = "Player 3";
        }
        if (p4Kills > maxKills)
        {
            maxKills = p4Kills;
            winner = "Player 4";
        }
        if (maxKills <= 0)
        {
            return 1;
        }
        return maxKills;
    }


	public void resetKill(string deceased) {
        Debug.Log("The player who died is: " + deceased);
		if (deceased == "Player 1") {
			p1Kills = 0;
		}
        else if (deceased == "Player 2")
        {
            p2Kills = 0;
        }
		else if (deceased == "Player 3") {
			p3Kills = 0;
		}
        else if (deceased == "Player 4")
        {
            p4Kills = 0;
        }
        else
        {
            Manager.say("ResetKill got invalid dead name", "always");
        }
	}


	// Use this for initialization
	void Start () {
		p1Kills = 0;
		p2Kills = 0;
		p3Kills = 0;
		p4Kills = 0;
        maxKills = -1;
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>()._M;
	}
	
	// Update is called once per frame
	void Update () {
        Manager.say("Total kills so far is: " + p4Kills, "eliot");
        if (p4Kills > 1)
        {
            Manager.say("GOT MILION KILLS DO THING", "eliot");
            //GameObject.Find("Player 4").GetComponent<FirstPersonController>().increaseSpeed(1090);
        }
	}

    public void NewGame(){
        p1Kills = 0;
        p2Kills = 0;
        p3Kills = 0;
        p4Kills = 0;
        maxKills = -1;
    }

	public int getPlayer1Kills(){
		return p1Kills;
	}

	public int getPlayer2Kills(){
		return p2Kills;
	}

	public int getPlayer3Kills(){
		return p3Kills;
	}

	public int getPlayer4Kills(){
		return p4Kills;
	}
}
