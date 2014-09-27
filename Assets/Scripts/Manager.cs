using UnityEngine;
using System.Collections;

public static class Manager {

	public const bool DEBUG = true;
	private const string myName = "eliot";
	//change to your name above, then whenever you want to see your own errors, call the function with your name.


	// BUT, if you think the error is relevant to everyone, make the second string say 'always' instead.
	public static void say(string msg, string speaker){
		if(DEBUG && (speaker == myName || speaker == "always")){
			Debug.Log(msg);
		}

	}
}
