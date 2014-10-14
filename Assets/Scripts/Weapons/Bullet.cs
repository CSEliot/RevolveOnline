using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	//ALL BULLETS MUST HAVE THIS SCRIPT!!!!!!!!!

	private Vector3 speed;
	private string owner;


	private bool gotSpeed = false;

	public void setSpeedandOwner(Vector3 setSpeed, string setOwner){
		speed = setSpeed;
		owner = setOwner;
		gotSpeed = true;
	}
	
	public bool getGotSpeed(){
		return gotSpeed;
	}

	public string getOwner(){
		return owner;
	}
	public Vector3 getSpeed(){
		return speed;
	}

}
