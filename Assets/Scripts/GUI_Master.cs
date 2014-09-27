using UnityEngine;
using System.Collections;

public class GUI_Master : MonoBehaviour {

	public GUIStyle varia;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.Box("Game Variables", varia);
	}
}
