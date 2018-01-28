using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour {
	public GameObject routerA;
	public GameObject routerB;

	public GameObject levelAudioPrefab;

	private AudioSource levelAudio;

	private int colorid = 0;
	private Color[] colors = {Color.red, Color.green, Color.blue, Color.yellow, Color.white};
	private float colortimer = 0f;

	// Use this for initialization
	void Start () {
		if(levelAudioPrefab != null) levelAudio = Instantiate(levelAudioPrefab).GetComponent<AudioSource>();
		if(levelAudio != null && !levelAudio.isPlaying) levelAudio.Play();
	}

	void ChangeColor(Color col) {
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("block_sprite")) {
			obj.GetComponent<Renderer>().material.SetColor("_MKGlowColor", col);
		}
	}

	void Update() {
		colortimer += Time.deltaTime;
		if(colortimer > 5f) {
			colortimer = 0f;
			ChangeColor(colors[colorid]);
			colorid++;
			if(colorid >= colors.Length) colorid=0;
		}

		if(Input.GetButtonDown("Submit")) {
			SelectionBar sb = (SelectionBar) GameObject.FindObjectOfType(typeof(SelectionBar));
			switch(sb.item) {
				case 0: SceneManager.LoadScene("scene1"); break;
				case 1: Application.Quit(); break;
				default: break;
			}
		}
	}

	// public void ChangeMode(){
	// 	if(state != "running")
	// 		return;
	// 	state = "acking";

	// 	Vector2 ack_position = routerB.transform.Find("spawn").position;
	// 	Vector2 ack_velocity = new Vector2();
		
	// 	if(player != null) {
	// 		ack_position = player.transform.position;
	// 		ack_velocity = player.GetComponent<Rigidbody2D>().velocity;
	// 		Destroy(player);
	// 	}
		
	// 	player = Instantiate(ackPrefab, ack_position, Quaternion.identity);
	// 	player.GetComponent<Rigidbody2D>().velocity = ack_velocity;
	// }
}
