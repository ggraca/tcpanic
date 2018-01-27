using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	public float level_time = 4f;
	public float time_left;

	// states: ["holding", "running", "acking", "success"]
	public string state = "holding";

	public GameObject routerA;
	public GameObject routerB;

	public GameObject packagePrefab;
	public GameObject ackPrefab;
	private GameObject player = null;

	// Use this for initialization
	void Start () {
		SetupLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if(state == "running" || state == "acking"){
			time_left -= Time.deltaTime;
			if(time_left <= 0){
				SetupLevel();
				return;
			}
		}
	}

	void SetupLevel(){
		if(player != null)
			Destroy(player);
		
		time_left = level_time;
		player = Instantiate(packagePrefab, routerA.transform.Find("spawn").position, Quaternion.identity);
	}

	void StartLevel(){
		state = "running";
	}

	void ChangeMode(){
		state = "acking";
		
		if(player != null)
			Destroy(player);
		player = Instantiate(ackPrefab, routerB.transform.Find("spawn").position, Quaternion.identity);

	}

	void SaveScore(){
		state = "success";

		if(player != null)
			Destroy(player);
	}
}
