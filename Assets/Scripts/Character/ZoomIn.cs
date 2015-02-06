using UnityEngine;
using System.Collections;

public class ZoomIn : MonoBehaviour {
	//Variables for parameters of Lerp
	private int zoom = 15;
	private int defaultFoV = 35;
	private int smoothness = 5;
	

	private Camera myCamera;

	void Start(){
		myCamera = this.GetComponent<Camera> ();
	}

	public void dozoom() {
		myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, zoom, Time.deltaTime * smoothness);
	}

	public void undozoom() {
		myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, defaultFoV, Time.deltaTime * smoothness);
	}
}