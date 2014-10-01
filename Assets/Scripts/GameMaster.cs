using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Reflection;

public class GameMaster : MonoBehaviour {

	[Serializable]
	public struct GAME_VALUES{

		//###################################
		//PLAYER VARIABLES
		//###################################
		public float movementSpeed; 
		public float runningSpeed;
		public float mouseSensetivity;// = 10.0f;
		public float upDownRange;// = 70.0f;
		public float jumpHeight;// = 10.0f;
		private float jumpCount;
		public float maxHealth;

		public bool doYouEvenLift;

		//###################################
		//ENVIRONMENT VARIABLES
		//###################################
		public float gravity;
		public bool jumpingAllowed;
		public bool runningAllowed;
		public bool miniMapEnabled;

		//###################################
		//MOD CONTENT ENABLED
		//###################################
		//GUNS/WEAPONS-----------------------
		//basic gun mods
		public float bulletSpeed_Basic;
		public float gunWeight_Basic;


		//Characters-------------------------

	}

	public GAME_VALUES _M;
	private int sizeX = 150;
	private int sizeY = 150;
	private bool gameOver = false;

	void Start(){
		Save_Values();
		Load_Values();
		gameObject.camera.pixelRect = new Rect(Screen.width/2 - sizeX/2, Screen.height/2 - sizeY/2, sizeX, sizeY);
		if(_M.miniMapEnabled){
			gameObject.camera.enabled = true;
		}
	}

	void Update(){
		if(Input.GetKeyDown("escape")){
			Save_Values();
			Manager.say("Attempting to quit game now, goodbye!", "always");
			Application.Quit();

		}
		if(GameObject.FindGameObjectsWithTag("Player").Length == 1){
		        gameOver = true;
		}   
	}


	private void Save_Values(){
		GameMaster masterScript;
		masterScript = GameObject.Find("Game Master").GetComponent<GameMaster>();
		Manager.say("Attempting to save GM", "always");
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream("GM.bin", FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize(stream, masterScript._M);
		stream.Close();
		Manager.say("Saving GM likely successful", "always");
	}

	private void Load_Values(){
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new FileStream("GM.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		_M = (GAME_VALUES) formatter.Deserialize(stream);
		stream.Close();
		Manager.say("Loading GM likely successful", "always");
	}
	
	private bool isGameOver(){
		return gameOver;
	}
}
