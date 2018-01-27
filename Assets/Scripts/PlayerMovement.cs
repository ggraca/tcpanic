using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rb;
	bool jumping = false;
	public int jump_multiplier = 30;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("up") && !jumping){
			rb.AddForce(Vector3.up * jump_multiplier);
		}
	}
}
