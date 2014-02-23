using UnityEngine;
using System.Collections;

public class OnPlayBase : MonoBehaviour {
	public MusicPlayer Player;
	void Start() {
		OnStart ();
		Player.OnPlay += OnPlay;
	}

	void OnDisable() {
		Player.OnPlay -= OnPlay;
	}

	protected virtual void OnStart() {
		
	}

	protected virtual void OnPlay() {
		
	}
}
