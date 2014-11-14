using UnityEngine;
using System.Collections;

public class ZoomIn : MonoBehaviour {
	private int zoom = 15;
	private int defaultFoV = 35;
	private int smoothness = 5;
	private bool isZoomed = false;

	void Update() {
		if (Input.GetMouseButtonDown (1)) {
			isZoomed = !isZoomed;
		}
		if (isZoomed == true) {
			camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, zoom, Time.deltaTime * smoothness);
		}
		else {
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, defaultFoV, Time.deltaTime * smoothness);
		}
	}
}
