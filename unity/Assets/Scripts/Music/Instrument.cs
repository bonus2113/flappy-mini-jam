using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class Instrument : MusicPlayer {
	public AnimationCurve Falloff;
	public AnimationCurve FrequencyOffset;

	public float VibratoAmount = 0.0f;
	public float VibratoFrequency = 0.0f;
	
	System.Random rand = new System.Random();

	WaveGenerator waveGen;

	bool hasWaveGen = false;
	double nextBeat = 0;

	bool isRunning;

	void Start() {
		waveGen = GetComponent<WaveGenerator> ();
		hasWaveGen = waveGen;
		Reset ();
	}
	
	int RandomRange(int _min, int _max) {
		return (int)( rand.NextDouble() * (_max - _min) - 0.001f ) + _min;
	}

	public void Reset() {
		time = float.MinValue;
		nextBeat = float.MaxValue;
		//isRunning = false;
	}

	double time;
	private double sampling_frequency = 48000;
	double baseFrequence;

	protected override void OnBeatPlay (double _delay)
	{
		nextBeat = _delay;
		isRunning = true;
	}

	public override void SetVolume (float _vol)
	{
		Volume = _vol;
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		// update increment in case frequency has changed
		double increment = 1.0f / sampling_frequency;

		for (var i = 0; i < data.Length; i = i + channels)
		{
			time += increment;
			nextBeat -= increment;
			if(nextBeat <= 0)
				time = 0;
			// this is where we copy audio data to make them “available” to Unity
			float val = time >= 0 ? Falloff.Evaluate((float)time) : 0;
			data[i] *= val * Volume + VibratoAmount * (float)Math.Sin(time*VibratoFrequency);
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] *= val * Volume + VibratoAmount * (float)Math.Sin(time*VibratoFrequency);
		}
		if (hasWaveGen) {
			waveGen.frequencyOffsetFactor = Mathf.Clamp01(time >= 0 ? FrequencyOffset.Evaluate((float)time) : 0);
		}
	}

}
