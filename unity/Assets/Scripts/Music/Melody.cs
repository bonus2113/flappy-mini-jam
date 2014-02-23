using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(WaveGenerator), typeof(Instrument))]
public class Melody : OnPlayBase {
	WaveGenerator generator;
	MelodyMachine machine;

	public int LowLevel = 1;
	public int HighLevel = 6;
	public int Octave;
	public NoteName BaseNote;
	public bool IsMinor;
	public bool Repeat;
	public int PatternLength = 0;
	public bool Mutate;
	public int MutateAfter;

	int patternIndex = 0;
	int patternCounter = 0;

	List<Note> melody = new List<Note>();

	Note current;

	// Use this for initialization
	protected override void OnStart () {
		generator = GetComponent<WaveGenerator>();

		Player = GetComponent<Instrument> ();

		uint octave = (uint)Random.Range(LowLevel, HighLevel);
		List<MelodyTansitionInfo> transitions = new List<MelodyTansitionInfo>();

		List<Note> scale = MusicGenerator.GenerateScale (BaseNote, Octave, !IsMinor);

		for(int i = 0; i < scale.Count; i++) {
			transitions.Add( new MelodyTansitionInfo(i, Random.Range(0, scale.Count), 1));
		}
		
		int transitionCount = Random.Range(7, 15);
		for(int i = 0; i < transitionCount; i++) {
			transitions.Add( new MelodyTansitionInfo( 
                         Random.Range(0, scale.Count), 
                         Random .Range(0, scale.Count), 
                         Random.value * 2));
			
		}
		
		machine = new MelodyMachine(scale, transitions);
		machine.SetOctave(octave);

		if (Repeat && PatternLength > 0) {
			for(int i = 0; i < PatternLength; i++) {
				melody.Add(machine.CurrentNote);
				machine.Next();
			}
		}
	}

	protected override void OnPlay ()
	{
		if (Repeat && melody.Count > 0) {
			generator.frequencyOffset = (float)melody[patternIndex].GetFrequency() - generator.frequency;
			generator.frequency = (float)melody[patternIndex].GetFrequency();
			patternIndex = (patternIndex + 1) % PatternLength;
			if(Mutate && patternIndex == 0) {
				patternCounter = (patternCounter + 1) % MutateAfter;
				if(patternCounter == 0) {
					int mutateIndex = Random.Range(0, melody.Count);
					machine.Next();
					melody[mutateIndex] = machine.CurrentNote;
				}
			}
		} else {
			machine.Next ();
			generator.frequencyOffset = (float)machine.CurrentFrequency - generator.frequency;
			generator.frequency = (float)machine.CurrentFrequency;
		}
	}
}
