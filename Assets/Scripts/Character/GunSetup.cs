using UnityEngine;
using System.Collections;

public class GunSetup : MonoBehaviour {

    public Transform parentObject;
    public Vector3 startRotation;
    public Vector3 startPosition;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void attachGun(GameObject gun,GameObject player){
        gun.transform.parent = parentObject;
        gun.transform.localRotation = Quaternion.Euler(startRotation);
        gun.transform.localPosition = startPosition;
    }
}
