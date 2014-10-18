using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Displacement/Twirl")]
public class TwirlEffect : ImageEffectBase{
	public Vector2  radius = new Vector2(0.3F,0.3F);
	private float    angle = 0;
	public Vector2  center = new Vector2 (0.5F, 0.5F);


	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		ImageEffects.RenderDistortion (material, source, destination, angle, center, radius);
	}

    void Update()
    {
        angle += Time.deltaTime * 20.0f;
    }
}
