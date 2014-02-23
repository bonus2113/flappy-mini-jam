using UnityEngine;  
using System.Collections;  

[RequireComponent(typeof(SamplePlayer))]
public class AudioAnalyser : MonoBehaviour  
{  
	//An AudioSource object so the music can be played  
	private AudioSource aSource;  
	//A float array that stores the audio samples  
	public float[] SamplesRight { get; private set; }
	public float[] SamplesLeft { get; private set; }
	public float[] Samples { get; private set; }
	
	SamplePlayer player;

	void Start() 
	{
		SamplesRight = new float[64];
		SamplesLeft = new float[64];
		Samples = new float[64];
		player = GetComponent<SamplePlayer> ();
	}
	
	void Update ()  
	{  
		aSource = player.PrimarySource;  
		
		//Obtain the samples from the frequency bands of the attached AudioSource  
		aSource.GetSpectrumData(SamplesRight,0,FFTWindow.BlackmanHarris);  
		aSource.GetSpectrumData(SamplesLeft,1,FFTWindow.BlackmanHarris); 

		for(int i = 0; i < Samples.Length; i++) {
			Samples[i] = (SamplesRight[i] + SamplesLeft[i]) / 2;
		}
	}  
}  