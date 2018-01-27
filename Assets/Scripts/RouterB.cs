using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterB : MonoBehaviour {	
	
	private GameLogic gl;

	void Start(){
		gl = (GameLogic) GameObject.FindObjectsOfType(typeof(GameLogic))[0];
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(gl.state != "running") return;

		if(coll.CompareTag("Player")) {
			gl.ChangeMode();
		}
    }
}
