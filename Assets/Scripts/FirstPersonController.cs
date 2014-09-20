using UnityEngine;
using System.Collections;
using System;


public class FirstPersonController : MonoBehaviour {


	private GameMaster GM;

	private float rotUpDown;// = 0;
	private Vector3 speed;
	private float verticalSpeed;
	private float rotLeftRight;

	CharacterController characterController;
	// Use this for initialization
	void Start () {

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

		rotLeftRight = Input.GetAxis("Mouse X")*GM.mouseSensetivity;
		transform.Rotate(0, rotLeftRight, 0);
		//up and down (with camera)
		rotUpDown -= Input.GetAxis("Mouse Y")*GM.mouseSensetivity;
		rotUpDown = Mathf.Clamp(rotUpDown, -GM.upDownRange, GM.upDownRange);
		
		Camera.main.transform.localRotation = Quaternion.Euler(rotUpDown,0,0);

				
		//Movement
		//Jumping!!
		if(characterController.isGrounded && Input.GetButtonDown("Jump")){
			verticalSpeed = GM.jumpHeight;
			Debug.Log("Jumping attempted!");
		}
		else if(Input.GetButtonDown("Jump")){
			Debug.Log("Jumping attempted! and FAILED");
		}
		if(characterController.isGrounded && verticalSpeed < 0){
			verticalSpeed = 0.0f;
		}
		else{
			verticalSpeed += Physics.gravity.y * Time.deltaTime;
		}
		//Running!!
		if(characterController.isGrounded && Input.GetKeyDown(KeyCode.LeftShift)){
			GM.movementSpeed = 10.0f;
		}
		else if(GM.movementSpeed == 10.0f){
			GM.movementSpeed = 6.0f;
		}
		
		
		float forwardSpeed = Input.GetAxis("Vertical");
		float sideSpeed = Input.GetAxis("Horizontal");
		
		speed = new Vector3( sideSpeed*GM.movementSpeed, verticalSpeed, forwardSpeed*GM.movementSpeed);
		
		speed = transform.rotation * speed;
		
		characterController.Move( speed * Time.deltaTime );
	}
}
