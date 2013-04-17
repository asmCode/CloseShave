using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public Transform lookAtTarget;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (lookAtTarget);
	}
}
