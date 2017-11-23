using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<CharacterJoint> ().connectedBody = transform.parent.GetComponent<Rigidbody> ();
	}
}
