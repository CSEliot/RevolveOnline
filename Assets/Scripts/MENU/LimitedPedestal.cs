using UnityEngine;
using System.Collections;

public class LimitedPedestal : MonoBehaviour {

    private float currentRotation;
    public float rotationSpeed;
    private float limitAmount = 0.45f;

    // Use this for initialization
    void Start()
    {
        currentRotation = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.rotation.x > limitAmount || transform.rotation.x > -limitAmount)
        {
            rotationSpeed *= -1;
        }

        currentRotation = ((rotationSpeed));
        Debug.Log(transform.rotation.x);

        //Manager.say("Current Rotation: " + currentRotation + "Actual Rotation: " + transform.rotation.y);

        //transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, currentRotation, this.transform.eulerAngles.z);
        transform.Rotate(0, currentRotation, 0);
        //if (transform.childCount == 0)
        //{
        //    Manager.say("Pedestal Destroyed!", "eliot");
        //    Destroy(gameObject);
        //}
    }
}
