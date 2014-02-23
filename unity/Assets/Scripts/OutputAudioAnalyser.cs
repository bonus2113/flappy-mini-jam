using UnityEngine;
using System.Collections;

public class OutputAudioAnalyser : MonoBehaviour {
	AudioListener listener;
	float[] samplesLeft;
	float[] samplesRight;

	float[] samples;

	public float[] Samples { get { return samples; } }

	int sampleCount = 1024;

	// Use this for initialization
	void Start () {
		listener = GetComponent<AudioListener> ();
		samplesLeft = new float[sampleCount];
		samplesRight = new float[sampleCount];
		samples = new float[sampleCount];
	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.GetSpectrumData (samplesLeft, 0, FFTWindow.BlackmanHarris);
		AudioListener.GetSpectrumData (samplesRight, 1, FFTWindow.BlackmanHarris);

		for(int i = 0; i < sampleCount; i++)
		{
			samples[i] = (samplesLeft[i]+samplesRight[i])/2;
		}
	}
}
