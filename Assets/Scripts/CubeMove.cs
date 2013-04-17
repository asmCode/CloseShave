using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	private const float MOVE_SPEED = 1.0f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate(Vector3.left * Time.deltaTime * MOVE_SPEED);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate(Vector3.right * Time.deltaTime * MOVE_SPEED);
		}
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate(Vector3.up * Time.deltaTime * MOVE_SPEED);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate(Vector3.down * Time.deltaTime * MOVE_SPEED);
		}		
	}
}
