﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Healthbar : MonoBehaviour
{

	private GameMaster GM;
    private float armor;
	private float maxHealth;
	private float currHealth;
	private float damagePerSecond;
	//private Animator animator;
	private float lifeFrac;
	private float sizeX;
	private float sizeY;
	private float sizeZ;
    private Dictionary<string, float> totalDamageList;

	private string lastDamageDealt;
	private const float sizeX_Max = 0.1f;
	
	private KillStreak kills;	
	public Transform parentObject;

	void Start()
	{		
		GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();
		kills = GameObject.Find("Game Master").GetComponent<KillStreak> ();
		//initialize variables
		sizeX = transform.GetChild(0).localScale.x; //referes to the Green child plane.
		sizeY = transform.GetChild(0).localScale.y;
		sizeZ = transform.GetChild(0).localScale.z;
		//float tempX = transform.GetChild(0).transform.localScale.x;
		//tempX = 100f;
		//size = GameObject.Find("Green").transform.localScale.x;
		//Debug.Log("Size after assigned=" + size);
		//animator = transform.parent.GetComponent<Animator>();
        totalDamageList = new Dictionary<string, float>();
	}

    //Percent of 100. So if you have >100 HP, 100% damage won't kill you.
	public void takePercentDamage(float damagePercent, string owner){
        if (armor >= 0)
        {
            armor -= 100*damagePercent;
        }
        else
        {
		    currHealth -= 100*damagePercent;
        }
		resizeHP_Bars();
        if (totalDamageList.ContainsKey(owner))
        {
            totalDamageList[owner] += damagePercent;
        }
        else
        {
            totalDamageList.Add(owner, damagePercent);
        }
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
			kills.addKill(lastDamageDealt);
			kills.resetKill(parentObject.name);
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

    public void modifyMaxHealth(GameMaster GM, float increaseBy, float givenArmor)
    {
        maxHealth = GM._M.maxHealth + increaseBy;
        currHealth = maxHealth;
        armor = givenArmor;
    }
}
