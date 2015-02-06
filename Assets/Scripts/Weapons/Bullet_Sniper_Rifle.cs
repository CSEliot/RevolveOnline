using UnityEngine;
using System.Collections;

public class Bullet_Sniper_Rifle : MonoBehaviour {

	private float timeBorn;


	private Vector3 bulletPos;
	private Ray	whatever;
	private RaycastHit anyVariableName;

	private GameMaster GM;

	private Vector3 speed = Vector3.zero;
	private string owner;
	private bool gotSpeed = false;

	// Use this for initialization
	void Start () {
		timeBorn = Time.time;
		GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - timeBorn > GM._M.bulletLife_Basic){
			Destroy(this.gameObject);	
		}

		//bulletPos = transform.position;

		/*whatever = new Ray(bulletPos, Vector3.up*-1);
		if(Physics.Raycast(whatever, out anyVariableName, 0.3f)){
			OnTriggerEnter(anyVariableName.transform);
		}*/


		//Keep trying to get the speed from Bullet.cs until it has it.
		if(!gotSpeed){
			gotSpeed = gameObject.transform.GetComponent<Bullet>().getGotSpeed();
			//ONLY GET THE SPEED FROM BULLET COMPONENT >>IF<< BULLET COMPONENT HAS IT'S SPEED BEEN ASSIGNED
			if(gotSpeed){
				gameObject.transform.GetComponent<Bullet>();
				speed = gameObject.transform.GetComponent<Bullet>().getSpeed();
				owner = gameObject.transform.GetComponent<Bullet>().getOwner();
				rigidbody.AddRelativeForce(speed);
			}
		}
	
	}

	void OnCollisionEnter(Collision hitObject){
		if(hitObject.transform.tag == "Player"){
			Manager.say("This bullet has hit: " + hitObject.gameObject.name, "berto");
			hitObject.transform.GetComponentInChildren<Healthbar>().takePercentDamage(0.50f, owner);
			Destroy(this.gameObject);
		}
		else{
			Destroy(this.gameObject);
		}
	}	

	void OnCollisionStay(Collision hitObject){
		if(hitObject.transform.tag == "Player"){
			Manager.say("This bullet has hit: " + hitObject.gameObject.name, "berto2");
			hitObject.transform.GetComponentInChildren<Healthbar>().takePercentDamage(0.50f, owner);
			Destroy(this.gameObject);
		}
		else{
			Destroy(this.gameObject);
		}
	}
}
