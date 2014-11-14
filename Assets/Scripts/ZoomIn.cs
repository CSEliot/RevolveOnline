using UnityEngine;
using System.Collections;

public class ZoomIn : MonoBehaviour {
	//Variables for parameters of Lerp
	private int zoom = 15;
	private int defaultFoV = 35;
	private int smoothness = 5;

	private GameMaster GM;


	void Start() {
		GM  = GameObject.Find("Game Master").GetComponent<GameMaster>();
	}

	//Change FoV to the zoom value if isZoomed is true, otherwise change FoV to the default FoV
	void Update() {
		if (GM._M.isZoomed) {
			camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, zoom, Time.deltaTime * smoothness);
		}
		else {
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, defaultFoV, Time.deltaTime * smoothness);
		}
	}
}