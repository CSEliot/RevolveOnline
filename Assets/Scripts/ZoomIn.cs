using UnityEngine;
using System.Collections;

public class ZoomIn : MonoBehaviour {
	//Variables for parameters of Lerp
	private int zoom = 15;
	private int defaultFoV = 35;
	private int smoothness = 5;
	

	public void dozoom() {
		camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoom, Time.deltaTime * smoothness);
	}

	public void undozoom() {
		camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, defaultFoV, Time.deltaTime * smoothness);
	}
}