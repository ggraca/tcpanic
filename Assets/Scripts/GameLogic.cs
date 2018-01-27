using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	public float level_time = 4f;
	public float time_left;

	// states: ["holding", "running", "acknowledging", "success"]
	public string state = "holding";

	public GameObject routerA;
	private GameObject routerB;

	public GameObject playerPrefab;
	private GameObject player = null;

	// Use this for initialization
	void Start () {
		SetupLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if(state == "running" || state == "acknowledging"){
			time_left -= Time.deltaTime;
			if(time_left <= 0){
				SetupLevel();
				return;
			}
		}
	}

	void SetupLevel(){

		// for all players in scene, call "die"
		if(player != null)
			Destroy(player);
		
		time_left = level_time;
		player = Instantiate(playerPrefab, routerA.transform.Find("spawn").position, Quaternion.identity);
	}

	void StartLevel(){
		state = "running";
	}

	void ChangeMode(){
		state = "acknowledging";
	}

	void SaveScore(){
		state = "success";
	}
}
