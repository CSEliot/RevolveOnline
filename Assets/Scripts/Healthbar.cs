using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Healthbar : MonoBehaviour
{

	private GameMaster GM;

	private float maxHealth;
	private float currHealth;
	private float damagePerSecond;
	private float damageDamper;
	//private Animator animator;
	private float lifeFrac;
	private float sizeX;
	private float sizeY;
	private float sizeZ;


	private string lastDamageDealt;
	private const float sizeX_Max = 0.1f;
	
	void Start()
	{		
		GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();

		//initialize variables
		sizeX = transform.GetChild(0).localScale.x; //referes to the Green child plane.
		sizeY = transform.GetChild(0).localScale.y;
		sizeZ = transform.GetChild(0).localScale.z;
		maxHealth = GM._M.maxHealth; // bar size, this never changes
		currHealth = maxHealth; //transform bar size
		float tempX = transform.GetChild(0).transform.localScale.x;
		tempX = 100f;
		//size = GameObject.Find("Green").transform.localScale.x;
		//Debug.Log("Size after assigned=" + size);
		//animator = transform.parent.GetComponent<Animator>();
	}

	public void takePercentDamage(float damagePercent, string owner){
		currHealth -= maxHealth*damagePercent;
		resizeHP_Bars();

		lastDamageDealt = owner;
	}
	private void resizeHP_Bars(){
		sizeX = sizeX_Max * (currHealth/maxHealth);
		Manager.say("SizeX is now: " + sizeX, "eliot");
		transform.GetChild(0).transform.localScale = new Vector3(sizeX, sizeY, sizeZ);// 10f, 10f ,10f);//
		transform.GetChild(2).transform.localScale = new Vector3(sizeX, sizeY, sizeZ);//10f, 10f ,10f);//
	}

	void FixedUpdate()
	{

		if(isDead()){
			Destroy(this.transform.parent.gameObject);
			Manager.say(transform.parent.name + " was murderized by: " + lastDamageDealt, "eliot");
		}

	}

	public bool isDead()
	{
		if (currHealth <= 0.001)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	void loseScene()
	{
		Destroy(this.gameObject);
		
	}
	
	
}
