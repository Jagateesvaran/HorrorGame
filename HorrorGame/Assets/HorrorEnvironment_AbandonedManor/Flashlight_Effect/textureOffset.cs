using UnityEngine;
using System.Collections;

public class textureOffset : MonoBehaviour {

	public float offsetSpeed = 0.1f;
	public string propertyName = "";
	private Material myMat;

	void Start()
	{
		myMat = gameObject.GetComponent<Renderer> ().material;
	}

	void Update()
	{
		myMat.SetTextureOffset (propertyName,new Vector2( myMat.GetTextureOffset(propertyName).x + Time.deltaTime*offsetSpeed, 0f) );
	}
}
