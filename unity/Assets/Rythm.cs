using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rythm : OnPlayBase {
	List<int> measures;

	public int PatternLength = 4;

	int patternIndex;

	public bool Mutate = false;
	public int MutateAfter = 0;
	int mutateIndex = 0;

	// Use this for initialization
	protected override void OnStart ()  {
		Player = GetComponent<MusicPlayer> ();
		measures = new List<int> ();
		for(int i = 0; i < PatternLength; i++) {
			measures.Add( (int)Mathf.Pow(2, Random.Range(1, 5)));
		}
	}
	
	protected override void OnPlay ()
	{
		Player.LoopLength = measures [patternIndex];
		patternIndex = (patternIndex + 1) % PatternLength;
		if (patternIndex == 0) {
			mutateIndex++;
			if(mutateIndex == MutateAfter)
			{
				mutateIndex = 0;
				measures[Random.Range(0, measures.Count)] = (int)Mathf.Pow(2, Random.Range(1, 3));
			}
		}
	}
}
