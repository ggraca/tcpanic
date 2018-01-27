using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rb;
	AudioSource au;

	int jump = 0;
	bool up = false;
	
	public float maxSpeed = 10f;
	public float moveMultiplier = 10f;
	public float jumpMultiplier = 70f;
	public float fallMultiplier = 1.5f;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		au = GetComponent<AudioSource>();
	}

	void Update() {
		if(Input.GetKeyDown("up") && jump < 2) {
			up = true;
		}
	}

	// FixedUpdate should be used for physics update i.e. RigidBody
	// https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
	void FixedUpdate () {
		if(Input.GetKey("right")) {
			rb.AddForce(Vector2.right * moveMultiplier);
		}

		if(Input.GetKey("left")) {
			rb.AddForce(Vector2.left * moveMultiplier);
		}

		if(up && jump < 2){
			up = false;
			jump++;
			Debug.Log(rb.velocity.y);
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(Vector2.up * jumpMultiplier);
			au.Play();
		}

		if(rb.velocity.y < 0) {
			rb.AddForce(Vector2.down * fallMultiplier);
		}

		if(rb.velocity.x > maxSpeed) {
			rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
		} else if (rb.velocity.x < -maxSpeed) {
			rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.collider.CompareTag("floor")) {
			jump = 0;
		}
    }


}
