using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private GameObject player;
	// Use this for initialization
	void Start () {
		player = FindPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null)
			player = FindPlayer();

		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) ;
	}

	GameObject FindPlayer(){
		return (GameObject) GameObject.FindGameObjectWithTag("Player");
	}
}
