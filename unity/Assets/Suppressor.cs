using UnityEngine;
using System.Collections;

public class Suppressor : MonoBehaviour {
	public float Cutoff;
	MusicPlayer player;

	void Start() {
		player = GetComponent<MusicPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Suppressing.CurrentPercent - Cutoff;
		player.SetVolume (Mathf.Clamp01 (dist * 10));
	}
}
