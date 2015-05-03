using UnityEngine;
using System.Collections;

public class Gun_Basic : MonoBehaviour {

	
	//private Vector3 startRotation = new Vector3(0f,0f,0f);
	//private Vector3 startScale = new Vector3(0.17989f, .13f, 0.56067f);

    //public bool equipped;
    //public bool spawnedEquipped;
    //private string Fire_str = " ";
    //private string Drop_str = " ";

    //private float fireSpd;

    //public GameObject bullet_prefab;
    //public GameObject Owner;


    //private GameMaster GM;
    private Weapon weap;

    //// Use this for initialization
    void Start () {

        weap = gameObject.GetComponent<Weapon>();
    //    fireSpd = 0;

    //    GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();

    //    if( !spawnedEquipped){
    //        equipped = false;
    //    }else{
    //        equipped = true;
    //        setEquips(transform.gameObject);
    //    }
    //    //bulletType = bullet_prefab.GetComponent(bulletType);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Equipped: " + weap.equipped + " Fire String: " + weap.Fire_str + " Fire Speed: " + weap.fireSpd);
        if (weap.equipped && Input.GetButton(weap.Fire_str) && weap.fireSpd < 0) {
            Debug.Log("dis hapnin");
            weap.FireBullet(weap.GM._M.bulletSpeed_Basic, weap.GM._M.fireInterval_Basic);
        }
        
        
        
        //if (equipped && Input.GetButtonDown(Drop_str))
        //{
        //    Dropped();
        //}

        //fireSpd -= Time.deltaTime*60;

        //Quaternion tempRot = gameObject.transform.rotation;
        //if(equipped && Input.GetButton(Fire_str) && fireSpd < 0){
        //    Manager.say("I FIRED", "eliot");
        //    GameObject tempBullet;
        //    //the gun has a bullet spawn component found via getchild(0).transform.position 
        //    tempBullet = Instantiate(bullet_prefab, transform.GetChild(0).transform.position,  tempRot*Quaternion.Euler(new Vector3(90f,0f,0f))) as GameObject;
        //    tempBullet.GetComponent<Bullet>().setSpeedandOwner(Vector3.up * (GM._M.bulletSpeed_Basic)*(GM._M.bulletSpeed_Basic), Owner.name);
        //    fireSpd = GM._M.fireInterval_Basic;
        //}
	}


	//METHOD USED IF PLAYER SPAWNS WITH THIS GUN
    //private void setEquips(GameObject player){
    //    if(!equipped && player.transform.tag == "Player" && !player.GetComponent<FirstPersonController>().gunEquipped){
    //        player.GetComponent<FirstPersonController>().gunEquipped = true;
    //        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    //        player.GetComponent<GunSetup>().attachGun(gameObject, player);
    //        equipped = true;
    //        //transform.localScale.Set(startScale.x, startScale.y, startScale.z);
    //        Fire_str = transform.GetComponentInParent<FirstPersonController>().GetFire_Str();
    //        Drop_str = transform.GetComponentInParent<FirstPersonController>().GetDrop_Str();
    //    }
    //}

	//METHOD USED IF NO SPAWNING WITH GUN
    //void OnTriggerEnter(Collider player){
    //    if(!equipped && player.transform.tag == "Player" && !player.GetComponent<FirstPersonController>().gunEquipped){
            
    //        player.GetComponent<FirstPersonController>().gunEquipped = true;
    //        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    //        player.GetComponent<GunSetup>().attachGun(gameObject, player.gameObject);
    //        equipped = true;
    //        //transform.localScale.Set(startScale.x, startScale.y, startScale.z);
    //        Fire_str = transform.GetComponentInParent<FirstPersonController>().GetFire_Str();
    //        Drop_str = transform.GetComponentInParent<FirstPersonController>().GetDrop_Str();
    //        Owner = player.gameObject;
    //    }
    //}

    //public void Dropped()
    //{
    //    equipped = false;
    //    GameObject newParent = Instantiate(GameObject.Find("Game Master").GetComponent<KillStreak>().Pedestal, transform.position, transform.rotation) as GameObject;
    //    transform.parent = newParent.transform;
    //}
}

