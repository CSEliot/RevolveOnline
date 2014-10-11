using UnityEngine;
using System.Collections;

public class Bullet_Basic : MonoBehaviour {

	private float timeBorn;


	private Vector3 bulletPos;
	private Ray	whatever;
	private RaycastHit anyVariableName;

	private GameMaster GM;

	private Vector3 speed = Vector3.zero;
	private string owner;

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
	}

	void OnCollisionEnter(Collision hitObject){
		if(hitObject.transform.tag == "Player"){
			Manager.say("This bullet has hit: " + hitObject.gameObject.name, "eliot");
			hitObject.transform.GetComponentInChildren<Healthbar>().takePercentDamage(0.20f, owner);
			Destroy(this.gameObject);
		}
		else{
			Destroy(this.gameObject);
		}
	}

	public void setSpeedandOwner(Vector3 setSpeed, string setOwner){
		speed = setSpeed;
		owner = setOwner;
		rigidbody.AddRelativeForce(speed);
	}
}
