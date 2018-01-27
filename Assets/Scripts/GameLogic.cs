using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	public float level_time = 4f;
	public float time_left;

	// states: ["holding", "running", "acking", "success"]
	public string state;

	public GameObject routerA;
	public GameObject routerB;

	public GameObject packagePrefab;
	public GameObject ackPrefab;
	private GameObject player = null;

	public Text ui_timer;

	// Use this for initialization
	void Start () {
		SetupLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if(state == "holding"){
			if(	Input.GetKeyDown("up") || Input.GetKeyDown("left") || Input.GetKeyDown("right")){
				state = "running";
			}
		}

		if(state == "running" || state == "acking"){
			time_left -= Time.deltaTime;
			ui_timer.text = time_left.ToString();

			if(time_left <= 0){
				SetupLevel();
				return;
			}
		}
	}

	void SetupLevel(){
		state = "holding";

		if(player != null)
			Destroy(player);
		
		time_left = level_time;
		ui_timer.text = time_left.ToString();
		player = Instantiate(packagePrefab, routerA.transform.Find("spawn").position, Quaternion.identity);
	}

	void StartLevel(){
		if(state != "holding")
			return;
		state = "running";
	}

	public void ChangeMode(){
		if(state != "running")
			return;
		state = "acking";
		
		if(player != null)
			Destroy(player);
		player = Instantiate(ackPrefab, routerB.transform.Find("spawn").position, Quaternion.identity);

	}

	public void SaveScore(){
		if(state != "acking")
			return;
		state = "success";

		if(player != null)
			Destroy(player);
	}
}
