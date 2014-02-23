using UnityEngine;
using System.Collections;

public class FrequencyBandVisualizer : MonoBehaviour {
	public OutputAudioAnalyser Analyser;
	public Color LeftColor;
	public Color RightColor;
	public Vector2 BarSize;

	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	Vector3[] vertices;
	Color[] colors;
	int[] triangles;
	Vector2[] uvs;
	Mesh mesh;

	// Use this for initialization
	void Start () {
		meshFilter = GetComponent<MeshFilter> ();
		meshRenderer = GetComponent<MeshRenderer> ();
		CreateBars (64, BarSize);
	}

	void CreateBars(int _count, Vector2 _size) {
		vertices = new Vector3[_count * 4];
		colors = new Color[_count * 4];
		uvs = new Vector2[_count * 4];
		triangles = new int[_count * 6];

		for(int i = 0; i < _count; i++) {
			CreateBar(i, _count, _size);
		}

		mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		meshFilter.mesh = mesh;
	}

	void SetBarHeight (int _index, float _percent, Vector2 _size)
	{
		Vector2 pos = new Vector2 (_index * _size.x, 0);
		int baseVertexIndex = _index * 4;
		//Top left
		vertices [baseVertexIndex + 0] = pos - new Vector2(_size.x/2, _size.y * _percent);
		//Bottom left
		vertices [baseVertexIndex + 1] = pos - new Vector2 (_size.x/2, 0);
		//Bottom right
		vertices [baseVertexIndex + 2] = pos + new Vector2 (_size.x/2, 0);
		//Top right
		vertices [baseVertexIndex + 3] = pos + new Vector2(_size.x/2, -_size.y * _percent);
	}

	void CreateBar (int _index, int _count, Vector2 _size) {
		Vector2 pos = new Vector2 (_index * _size.x, 0);
		int baseVertexIndex = _index * 4;
		//Top left
		uvs [baseVertexIndex + 0] = new Vector2 (0, 0);
		//Bottom left
		uvs [baseVertexIndex + 1] = new Vector2 (0, 1);
		//Bottom right
		uvs [baseVertexIndex + 2] = new Vector2 (1, 1);
		//Top right
		uvs [baseVertexIndex + 3] = new Vector2 (1, 0);

		SetBarHeight (_index, 1, _size);

		for (int i = 0; i < 4; i++) {
			colors[baseVertexIndex + i] = Color.Lerp(LeftColor, RightColor, (float)_index/_count);
		}

		int baseIndex = _index * 6;
		triangles [baseIndex + 0] = baseVertexIndex + 0;
		triangles [baseIndex + 1] = baseVertexIndex + 1;
		triangles [baseIndex + 2] = baseVertexIndex + 2;

		triangles [baseIndex + 3] = baseVertexIndex + 0;
		triangles [baseIndex + 4] = baseVertexIndex + 2;
		triangles [baseIndex + 5] = baseVertexIndex + 3;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 64; i++) {
			SetBarHeight(i, Mathf.Clamp(Analyser.Samples[i], 0.01f, 1), BarSize);
		}
		mesh.vertices = vertices;
	}
}
