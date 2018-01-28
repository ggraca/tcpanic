using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterB : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll) {
		GameLogic gl = (GameLogic) GameObject.FindObjectOfType(typeof(GameLogic));
		
		if(gl == null || gl.state != "running") return;

		if(coll.CompareTag("Player")) {
			Player player = (Player) GameObject.FindObjectOfType(typeof(Player));
			player.PlaySound("router");

			gl.ChangeMode();
		}
    }
}
