using UnityEngine;
using System.Collections;
using System;


public class FirstPersonController : MonoBehaviour {


	private GameMaster GM;

	private float rotUpDown;// = 0;
	private Vector3 speed;
	private float verticalSpeed;
	private float rotLeftRight;


	//ACTION STRINGS
	//==================================================================
	private string Haim_str = "_Look Rotation";
	private string Vaim_str = "_Look UpDown";
	private string Strf_str = "_Strafe";
	private string FWmv_str = "_Forward";
	public string  Fire_str = "_Fire";
	private string Jump_str = "_Jump";



	CharacterController characterController;
	// Use this for initialization
	void Start () {

		setControlStrings();

		GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();

		rotLeftRight = 0.0f; 
		speed = Vector3.zero;
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}


	
	// Update is called once per frame
	void Update () {
		//player rotation
		//left and right

		rotLeftRight = Input.GetAxis(Haim_str)*GM._M.mouseSensetivity;
		transform.Rotate(0, rotLeftRight, 0);
		//up and down (with camera)
		rotUpDown -= Input.GetAxis(Vaim_str)*GM._M.mouseSensetivity;
		rotUpDown = Mathf.Clamp(rotUpDown, -GM._M.upDownRange, GM._M.upDownRange);
		
		transform.GetChild(0).transform.localRotation = Quaternion.Euler(rotUpDown,0,0);

				
		//Movement
		//Jumping!!
		if(characterController.isGrounded && Input.GetButtonDown(Jump_str)){
			verticalSpeed = GM._M.jumpHeight;
			Debug.Log("Jumping attempted!");
		}
		else if(Input.GetButtonDown(Jump_str)){
			Debug.Log("Jumping attempted! and FAILED");
		}
		if(characterController.isGrounded && verticalSpeed < 0){
			verticalSpeed = 0.0f;
		}
		else{
			verticalSpeed += GM._M.gravity * Time.deltaTime;
		}
		//Running!!
		if(characterController.isGrounded && Input.GetKeyDown(KeyCode.LeftShift)){
			GM._M.movementSpeed = 10.0f;
		}
		else if(GM._M.movementSpeed == 10.0f){
			GM._M.movementSpeed = 6.0f;
		}
		
		
		float forwardSpeed = Input.GetAxis(FWmv_str);
		float sideSpeed = Input.GetAxis(Strf_str);
		
		speed = new Vector3( sideSpeed*GM._M.movementSpeed, verticalSpeed, forwardSpeed*GM._M.movementSpeed);
		
		speed = transform.rotation * speed;
		
		characterController.Move( speed * Time.deltaTime );
	}

	private void setControlStrings(){
		string pName = gameObject.name;

		if(pName == "Player 1"){
			Fire_str = "p1" + Fire_str;
			FWmv_str = "p1" + FWmv_str;
			Strf_str = "p1" + Strf_str;
			Haim_str = "p1" + Haim_str;
			Vaim_str = "p1" + Vaim_str;
			Jump_str = "p1" + Jump_str;
		}else if(pName == "Player 2"){
			Fire_str = "p2" + Fire_str;
			FWmv_str = "p2" + FWmv_str;
			Strf_str = "p2" + Strf_str;
			Haim_str = "p2" + Haim_str;
			Vaim_str = "p2" + Vaim_str;
			Jump_str = "p2" + Jump_str;
		}
	}

}