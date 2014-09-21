using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private float timeBorn;

	// Use this for initialization
	void Start () {
		timeBorn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - timeBorn > 4){
			Destroy(this.gameObject);	
		}

	}

	void OnTriggerStay(Collider hitObject){
		
		if(hitObject.transform.tag == "Player"){
			Manager.say("OHN NOWZ I WAS HIT");
			hitObject.transform.GetComponentInChildren<Healthbar>().takePercentDamage(0.20f);
			Destroy(this.gameObject);
		}
		else{
			Destroy(this.gameObject);
		}
	}
}
