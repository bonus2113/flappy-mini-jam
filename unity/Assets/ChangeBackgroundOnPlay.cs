using UnityEngine;
using System.Collections;

public class ChangeBackgroundOnPlay : OnPlayBase {
	public Color[] AvailableColors;
	int currentIndex = 0;
	protected override void OnPlay ()
	{
		currentIndex = ( currentIndex + Random.Range (1, AvailableColors.Length - 1)) % AvailableColors.Length;
		camera.backgroundColor = AvailableColors [currentIndex];
	}
}
