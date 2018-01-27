﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D rb;

	private int numJumps = 0;
	private bool up = false;
	
	public float maxSpeed = 10f;
	public float moveMultiplier = 10f;
	public float jumpMultiplier = 70f;
	public float fallMultiplier = 1.5f;

	public int maxJumps = 2;

	public GameObject jumpAudioPrefab;
	public GameObject deathAudioPrefab;
	public GameObject routerAudioPrefab;

	private AudioSource jumpAudio;
	private AudioSource deathAudio;
	private AudioSource routerAudio;

	void Start(){
		rb = GetComponent<Rigidbody2D>();

		if(jumpAudioPrefab != null) jumpAudio = Instantiate(jumpAudioPrefab).GetComponent<AudioSource>();
		if(deathAudioPrefab != null) deathAudio = Instantiate(deathAudioPrefab).GetComponent<AudioSource>();
		if(routerAudioPrefab != null) routerAudio = Instantiate(routerAudioPrefab).GetComponent<AudioSource>();
	}

	void Update() {
		if(Input.GetKeyDown("up") && numJumps < maxJumps) {
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

		if(up && numJumps < maxJumps){
			up = false;
			numJumps++;
			Debug.Log(rb.velocity.y);
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(Vector2.up * jumpMultiplier);

			if(jumpAudio != null) jumpAudio.Play();
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
			numJumps = 0;
		}
    }

	public void PlaySound(string s) {
		switch(s) {
			case "death": if(deathAudio != null) deathAudio.Play(); break;
			case "jump": if(jumpAudio != null) deathAudio.Play(); break;
			case "router": if(routerAudio != null) routerAudio.Play(); break;
		}
	}


}