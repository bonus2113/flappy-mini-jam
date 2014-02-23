using UnityEngine;
using System.Collections;

public class Suppressing : MonoBehaviour {
	public static float CurrentPercent { get; private set; }

	AudioLowPassFilter lowPass;
	MotionBlur blur;
	AudioDistortionFilter distort;
	ColorCorrectionCurves colorCorrect;
	void Start() {
		blur = GetComponent<MotionBlur> ();
		CurrentPercent = 0.00f;
		distort = GetComponent<AudioDistortionFilter> ();
		colorCorrect = GetComponent<ColorCorrectionCurves> ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)) {
			CurrentPercent = Input.mousePosition.y / Screen.height;
			distort.distortionLevel = Input.mousePosition.x/Screen.width;
		}
		blur.blurAmount = (1.0f - CurrentPercent);
		colorCorrect.saturation = 0.5f + CurrentPercent * 0.75f;
		

		if (lowPass) {
			lowPass.cutoffFrequency = 30 + (CurrentPercent - 0.1f) * 4000;

		} else if(Input.GetMouseButtonDown(0)) {
			lowPass = gameObject.AddComponent<AudioLowPassFilter> ();
		}
	}
}
