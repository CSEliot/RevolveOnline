using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public bool equipped;
    public bool spawnedEquipped;
    public string Fire_str = " ";
    protected string Drop_str = " ";

    public float fireSpd;

    public GameObject bullet_prefab;
    public GameObject Owner;

    public GameMaster GM;


	// Use this for initialization
	void Start () {
        fireSpd = 0;

        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();

        if (!spawnedEquipped) {
            equipped = false;
        }
        else {
            equipped = true;
            SetEquips(transform.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (equipped && Input.GetButtonDown(Drop_str)) {
            Dropped();
        }
        fireSpd -= Time.deltaTime * 60;
	}

    public void FireBullet(float bulletSpeed, float fireInterval) {
        GameObject tempBullet;
        Quaternion tempRot = gameObject.transform.rotation;
        //the gun has a bullet spawn component found via getchild(0).transform.position
        tempBullet = Instantiate(bullet_prefab, transform.GetChild(0).transform.position, tempRot * Quaternion.Euler(new Vector3(90f, 0f, 0f))) as GameObject;
        tempBullet.GetComponent<Bullet>().setSpeedandOwner(Vector3.up * (GM._M.bulletSpeed_LAZAR * 4) * (GM._M.bulletSpeed_LAZAR * 5), Owner.name);
        fireSpd = GM._M.fireInterval_LAZAR;
    }

    protected void SetEquips(GameObject player) {
        if (!equipped && player.transform.tag == "Player" && !player.GetComponent<FirstPersonController>().gunEquipped) {
            player.GetComponent<FirstPersonController>().gunEquipped = true;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            player.GetComponent<GunSetup>().attachGun(gameObject, player);
            equipped = true;
            Fire_str = transform.GetComponentInParent<FirstPersonController>().GetFire_Str();
            Drop_str = transform.GetComponentInParent<FirstPersonController>().GetDrop_Str();
        }
    }

    void OnTriggerEnter(Collider player) {
        if (!equipped && player.transform.tag == "Player" && !player.GetComponent<FirstPersonController>().gunEquipped) {
            player.GetComponent<FirstPersonController>().gunEquipped = true;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            player.GetComponent<GunSetup>().attachGun(gameObject, player.gameObject);
            equipped = true;
            //transform.localScale.Set(startScale.x, startScale.y, startScale.z);
            Fire_str = transform.GetComponentInParent<FirstPersonController>().GetFire_Str();
            Drop_str = transform.GetComponentInParent<FirstPersonController>().GetDrop_Str();
            Debug.Log("fire: " + Fire_str + "drop: " + Drop_str);
            Owner = player.gameObject;
        }
    }

    public void Dropped() {
        equipped = false;
        GameObject newParent = Instantiate(GameObject.Find("Game Master").GetComponent<KillStreak>().Pedestal, transform.position, transform.rotation) as GameObject;
        transform.parent = newParent.transform;
    }
}
