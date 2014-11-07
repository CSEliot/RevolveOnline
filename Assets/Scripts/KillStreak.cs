using UnityEngine;
using System.Collections;

public class KillStreak : MonoBehaviour {

	private int p1Kills;
	private int p2Kills;
	private int p3Kills;
	private int p4Kills;



	public void addKill(string killer) {

		if (killer == "Player 1") {
			p1Kills ++;
		}
		else if (killer == "Player 2") {
			p2Kills ++;
		}

		else if (killer == "Player 3") {
			p3Kills ++;
		}

        else if (killer == "Player 4")
        {
            p4Kills++;
        }
        else
        {
            Manager.say("AddKill function got bad name: " + killer, "always");
        }
	}


	public void resetKill(string deceased) {
        Manager.say("The player who died is: " + deceased, "eliot");
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
	}
	
	// Update is called once per frame
	void Update () {
        Manager.say("Total kills so far is: " + p4Kills, "eliot");
        if (p4Kills > 1)
        {
            Manager.say("GOT MILION KILLS DO THING", "eliot");
            GameObject.Find("Player 4").GetComponent<FirstPersonController>().increaseSpeed(1090);
        }
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
