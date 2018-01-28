using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterB : MonoBehaviour {	
	
	private GameLogic gl;

	void Start(){
		gl = (GameLogic) GameObject.FindObjectOfType(typeof(GameLogic));
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(gl.state != "running") return;

		if(coll.CompareTag("Player")) {
			Player player = (Player) GameObject.FindObjectOfType(typeof(Player));
			player.PlaySound("router");

			gl.ChangeMode();
		}
    }
}
