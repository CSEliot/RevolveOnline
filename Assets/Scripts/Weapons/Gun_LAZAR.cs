using UnityEngine;
using System.Collections;

public class Gun_LAZAR : MonoBehaviour {
	
	
	//private Vector3 startRotation = new Vector3(0f,0f,0f);
	//private Vector3 startScale = new Vector3(0.17989f, .13f, 0.56067f);
	



    //public bool equipped;
    //public bool spawnedEquipped;
    //private string Fire_str = " ";
    //private string Drop_str = " ";
    //private float fireSpd;
    //public GameObject bullet_prefab;
    //private GameMaster GM;	
	//private string owner;


    private float intervalModifier;

    private Weapon weap;
    
    // Use this for initialization
	void Start () {
        intervalModifier = 59;

        weap = gameObject.GetComponent<Weapon>();


        //fireSpd = 0;
        //GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();
        //if( !spawnedEquipped){
        //    equipped = false;
        //}else{
        //    equipped = true;
        //    setEquips(transform.gameObject);
        //}



		//bulletType = bullet_prefab.GetComponent(bulletType);
	}
	
	// Update is called once per frame
	void Update () {
        //if (equipped && Input.GetButtonDown(Drop_str))
        //{
        //    Dropped();
        //}
        //fireSpd -= Time.deltaTime*60;
		//Quaternion tempRot = gameObject.transform.rotation;



		if(weap.equipped && Input.GetButton(weap.Fire_str) && weap.fireSpd < 0){
            FireBullet(weap.GM._M.bulletLife_LAZAR, weap.GM._M.fireInterval_LAZAR);
            
            
            //Manager.say("I FIRED", "Nate");
            //GameObject tempBullet;
			//the gun has a bullet spawn component found via getchild(0).transform.position 
            //Vector3 offset = Vector3.zero;
            //for(int i=0; i < 5; i++)
            //{
            //    tempBullet = Instantiate(bullet_prefab, transform.GetChild(0).transform.position + offset, tempRot * Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject;
            //    tempBullet.GetComponent<Bullet>().setSpeedandOwner(Vector3.up * (GM._M.bulletSpeed_LAZAR * 4) * (GM._M.bulletSpeed_LAZAR * 5), owner);
            //    fireSpd = GM._M.fireInterval_LAZAR + intervalModifier;
            //    offset += new Vector3(Random.Range(-.10f, .10f), Random.Range(-.1f, .1f), 0);
            //}
		}
	}

    private void FireBullet(float bulletSpeed, float fireInterval) {
        Vector3 offset = Vector3.zero;
        for (int i = 0; i < 5; i++) {
            GameObject tempBullet;
            Quaternion tempRot = gameObject.transform.rotation;

            tempBullet = Instantiate(weap.bullet_prefab, transform.GetChild(0).transform.position + offset, tempRot * Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject;
            tempBullet.GetComponent<Bullet>().setSpeedandOwner(Vector3.up * (weap.GM._M.bulletSpeed_LAZAR * 4) * (weap.GM._M.bulletSpeed_LAZAR * 5), weap.Owner.name);
            weap.fireSpd = weap.GM._M.fireInterval_LAZAR + intervalModifier;
            offset += new Vector3(Random.Range(-.10f, .10f), Random.Range(-.1f, .1f), 0);
        }
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
    //        owner = transform.parent.parent.name;
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


