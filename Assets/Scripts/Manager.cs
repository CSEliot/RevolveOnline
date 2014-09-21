using UnityEngine;
using System.Collections;

public static class Manager {

	public const bool DEBUG = true;

	public static void say(string msg){
		if(DEBUG){
			Debug.Log(msg);
		}
	}
}
