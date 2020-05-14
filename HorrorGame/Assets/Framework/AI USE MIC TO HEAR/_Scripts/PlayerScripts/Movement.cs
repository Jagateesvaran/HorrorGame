using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float runSpeed;

	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.W)){
			transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

		}
		if(Input.GetKey(KeyCode.A)){
			transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
			
		}
		if(Input.GetKey(KeyCode.S)){
			transform.Translate(Vector3.back * runSpeed * Time.deltaTime);
			
		}
		if(Input.GetKey(KeyCode.D)){
			
			transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
		}
	}
}
