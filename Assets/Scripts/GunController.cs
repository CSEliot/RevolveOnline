using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	private Vector3 startPosition = new Vector3(.3f, -0.09f, 0.66f);
	private Vector3 startRotation = new Vector3(0f,0f,0f);
	private Vector3 startScale = new Vector3(0.17989f, .13f, 0.56067f);

	private bool equipped;
	public bool spawnedEquipped;
	private string Fire_str = " "; 

	public GameObject bullet_prefab;

	private GameMaster GM;

	// Use this for initialization
	void Start () {

		GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();

		if( !spawnedEquipped){
			equipped = false;
		}else{
			equipped = true;
			setEquips(transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(equipped && Input.GetButtonDown(Fire_str)){
			Manager.say("I FIRED");
			GameObject tempBullet;
			tempBullet = Instantiate(bullet_prefab, transform.GetChild(0).transform.position,  Quaternion.Euler(new Vector3(90f,0f,0f))) as GameObject;
			tempBullet.rigidbody.velocity = transform.TransformDirection(Vector3.forward * GM._M.bulletSpeed_Basic);

		}
	}

	private void setEquips(GameObject player){
		if(!equipped && player.transform.tag == "Player"){
			Manager.say("O SHI- I was collided");
			transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			transform.parent = player.transform.GetChild(0).transform;
			equipped = true;
			transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			transform.localPosition = startPosition;
			//transform.localScale.Set(startScale.x, startScale.y, startScale.z);
			Fire_str = transform.parent.parent.GetComponent<FirstPersonController>().Fire_str;
		}
	}
	void OnTriggerEnter(Collider player){
		if(!equipped && player.transform.tag == "Player"){
			Manager.say("O SHI- I was collided");
			transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			transform.parent = player.transform.GetChild(0).transform;
			equipped = true;
			transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			transform.localPosition = startPosition;
			//transform.localScale.Set(startScale.x, startScale.y, startScale.z);
			Fire_str = transform.parent.parent.GetComponent<FirstPersonController>().Fire_str;
		}
	}
}

