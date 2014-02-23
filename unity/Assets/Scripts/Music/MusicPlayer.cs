using UnityEngine;
using System;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	public event Action OnPlay;

	//Number of beats after the sample loops
	public int LoopLength;
	public float Volume;
	public int Offset;

	int currentBeat = -1;
	

	void OnEnable() {
		AudioSyncher.OnBeat += OnBeat;
		currentBeat = (-1 + Offset) % LoopLength;
	}

	void OnDisable() {
		AudioSyncher.OnBeat -= OnBeat;
	}

	public virtual void SetVolume(float _vol) {

	}

	protected virtual void OnBeat(double _delay) {
		currentBeat = (currentBeat + 1) % LoopLength;
		
		if (currentBeat == 0) {
			OnBeatPlay(_delay);
			StartCoroutine(SendPlayEvent(_delay));
		}
	}

	protected virtual void OnBeatPlay(double _delay) {

	}

	
	IEnumerator SendPlayEvent(double _delay) {
		yield return new WaitForSeconds((float)_delay);
		if (Volume > 0.001f && OnPlay != null)
			OnPlay ();
	}
}

