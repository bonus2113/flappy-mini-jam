using UnityEngine;
using System.Collections.Generic;

public static class ListExtension {
	public static T GetRandom<T>(this List<T> _list) {
		return _list[Random.Range(0, _list.Count)];
	}

	public static T GetLast<T>(this List<T> _list) {
		return _list[_list.Count - 1];
	}
}
