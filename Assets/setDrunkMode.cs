using UnityEngine;
using System.Collections;

public class setDrunkMode : MonoBehaviour {

    private GameMaster GM;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GM._M.drunkMode)
        {
            transform.GetComponent<TwirlEffect>().enabled = true;
            transform.GetComponent<MotionBlur>().enabled = true;
        }
	}
}
