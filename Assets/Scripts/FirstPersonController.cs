using UnityEngine;
using System.Collections;
using System;


public class FirstPersonController : MonoBehaviour {
	
	private float movementSpeed = 6.001f; 
	private float mouseSensetivity = 10.0f;
	//private float rotYSpeed = 1.01f;
	private float upDownRange = 70.0f;
	private float rotUpDown = 0;
	private float verticalSpeed = 0.0f;
	private float jumpHeight = 10.0f;
	Vector3 speed;



	CharacterController characterController;
	// Use this for initialization
	void Start () {
		speed = Vector3.zero;
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//player rotation
		//left and right
		float rotLeftRight = Input.GetAxis("Mouse X")*mouseSensetivity;
		transform.Rotate(0, rotLeftRight, 0);
		//up and down (with camera)
		rotUpDown -= Input.GetAxis("Mouse Y")*mouseSensetivity;
		rotUpDown = Mathf.Clamp(rotUpDown, -upDownRange, upDownRange);
		
		Camera.main.transform.localRotation = Quaternion.Euler( ,0,0);

				
		//Movement
		//Jumping!!
		if(characterController.isGrounded && Input.GetButtonDown("Jump")){
			verticalSpeed = jumpHeight;
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
			movementSpeed = 10.0f;
		}
		else if(movementSpeed == 10.0f){
			movementSpeed = 6.0f;
		}
		
		
		float forwardSpeed = Input.GetAxis("Vertical");
		float sideSpeed = Input.GetAxis("Horizontal");
		
		speed = new Vector3( sideSpeed*movementSpeed, verticalSpeed, forwardSpeed*movementSpeed);
		
		speed = transform.rotation * speed;
		
		characterController.Move( speed * Time.deltaTime );
	}
}
