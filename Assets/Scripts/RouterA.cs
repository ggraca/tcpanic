using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterA : MonoBehaviour {

	private GameLogic gl;

	void Start(){
		gl = (GameLogic) GameObject.FindObjectsOfType(typeof(GameLogic))[0];
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(gl.state != "acking") return;

		if(coll.CompareTag("Player")) {
			gl.SaveScore();
		}
    }
}
