using UnityEngine;
using System;
using System.Collections;

//Plays a sample in time
[RequireComponent(typeof(AudioSource))]
public class SamplePlayer : MusicPlayer {

	//The clip that will be played;
	public AudioClip SampleClip;

	public float Pitch = 1;
	
	//Two sources, to switch in between as PlayScheduled stops playback.
	public AudioSource PrimarySource { get; private set; }
	public AudioSource SecondarySource { get; private set; }

	// Use this for initialization
	void Start () {
		AudioSource[] sources = GetComponents<AudioSource> ();
		PrimarySource = sources [0];
		SecondarySource = sources [1];

		PrimarySource.clip = SampleClip;
		SecondarySource.clip = SampleClip;
	}

	public override void SetVolume (float _vol)
	{
		Volume = _vol;
	}
	

	protected override void OnBeat(double _delay) {
		base.OnBeat (_delay);

		PrimarySource.clip = SecondarySource.clip = SampleClip;
		PrimarySource.volume = SecondarySource.volume = Volume;
		PrimarySource.pitch = SecondarySource.pitch = Pitch;
	}

	protected override void OnBeatPlay (double _delay)
	{
		SecondarySource.PlayScheduled (_delay);
		AudioSource temp = SecondarySource;
		SecondarySource = PrimarySource;
		PrimarySource = temp;
	}

}
