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

		if (killer == "Player 2") {
			p2Kills ++;
		}

		if (killer == "Player 3") {
			p3Kills ++;
		}

		if (killer == "Player 4") {
			p4Kills ++;
		}
	}


	public void resetKill(string deceased) {
		
		if (deceased == "Player 1") {
			p1Kills = 0;
		}
		
		if (deceased == "Player 2") {
			p2Kills = 0;
		}
		
		if (deceased == "Player 3") {
			p3Kills = 0;
		}
		
		if (deceased == "Player 4") {
			p4Kills = 0;
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
