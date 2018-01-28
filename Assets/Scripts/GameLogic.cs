using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	private float level_time;
	public float time_left;

	// states: ["holding", "running", "acking", "success"]
	public string state;

	private GameObject routerA;
	private GameObject routerB;

	public GameObject packagePrefab;
	public GameObject ackPrefab;
	public GameObject levelAudioPrefab;

	private AudioSource levelAudio;

	private GameObject player = null;

	public Text ui_timer;
	public Text ui_best_score;

	private int colorid = 0;
	private Color[] colors = {Color.red, Color.green, Color.blue, Color.yellow, Color.white};
	private float colortimer = 0f;

	// Use this for initialization
	public void Setup () {
		if(levelAudioPrefab != null) levelAudio = Instantiate(levelAudioPrefab).GetComponent<AudioSource>();
		if(levelAudio != null && !levelAudio.isPlaying) levelAudio.Play();

		routerA = ((RouterA) GameObject.FindObjectsOfType(typeof(RouterA))[0]).gameObject;
		routerB = ((RouterB) GameObject.FindObjectsOfType(typeof(RouterB))[0]).gameObject;
		SetupLevel();

		LevelLoader gl = (LevelLoader) GameObject.FindObjectOfType(typeof(LevelLoader));
		level_time = gl.level_time;
		if(gl.best != 0f)
			ui_best_score.text = "best: " + gl.best.ToString("0.00");
		else
			ui_best_score.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Restart"))
			SetupLevel();

		if(state == "holding"){
			if(Input.GetAxis("Horizontal") != 0 || Input.GetButton("Jump")){
				state = "running";
			}
		}

		if(state == "running" || state == "acking"){
			// Rainbow mode
			// colortimer += Time.deltaTime;
			// if(colortimer > 1f) {
			// 	colortimer = 0f;
			// 	foreach(GameObject obj in GameObject.FindGameObjectsWithTag("block_sprite")) {
			// 		obj.GetComponent<Renderer>().material.SetColor("_MKGlowColor", colors[colorid]);
			// 	}
			// 	colorid++;
			// 	if(colorid >= colors.Length) colorid=0;
			// }

			time_left -= Time.deltaTime;
			ui_timer.text = time_left.ToString("0.00");

			if(time_left <= 0 || player.transform.position.y < -1){
				SetupLevel();
				return;
			}
		}
	}

	void SetupLevel(){
		state = "holding";
		
		time_left = level_time;
		ui_timer.text = time_left.ToString("0.00");
		
		KillPlayer();
		player = Instantiate(packagePrefab, routerA.transform.Find("spawn").position, Quaternion.identity);
	}

	void StartLevel(){
		if(state != "holding")
			return;
		state = "running";
	}

	void KillPlayer() {
		if(player != null) {
			player.GetComponent<Player>().PlaySound("death");
			Destroy(player);
		}
	}

	public void ChangeMode(){
		if(state != "running")
			return;
		state = "acking";

		Vector2 ack_position = routerB.transform.Find("spawn").position;
		Vector2 ack_velocity = new Vector2();
		
		if(player != null) {
			ack_position = player.transform.position;
			ack_velocity = player.GetComponent<Rigidbody2D>().velocity;
			Destroy(player);
		}
		
		player = Instantiate(ackPrefab, ack_position, Quaternion.identity);
		player.GetComponent<Rigidbody2D>().velocity = ack_velocity;
	}

	public void SaveScore(){
		if(state != "acking")
			return;
		state = "success";

		if(player != null)
			Destroy(player);

		LevelLoader gl = (LevelLoader) GameObject.FindObjectOfType(typeof(LevelLoader));
		if(gl.SaveScore(time_left))
			ui_best_score.text = "best: " + time_left.ToString("0.00");
	}
}
