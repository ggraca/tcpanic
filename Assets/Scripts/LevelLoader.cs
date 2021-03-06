﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public GameObject block;
	public GameObject blockSprite;
	public GameObject routerA;
	public GameObject routerB;
	
	public string level_name = "tutorial";
	public Texture2D level;
	public float level_time = 12f;
	public float bronze = 0f;
	public float silver = 2f;
	public float gold = 4f;
	public float best = 0f;

	void Start () {
		LoadScore();
		GenerateLevel();
		GameLogic gl = (GameLogic) GameObject.FindObjectOfType(typeof(GameLogic));
		if(gl != null)
			gl.Setup();
	}

	void GenerateLevel(){
		for(int x = 0; x < level.width; x++){
			for(int y = 0; y < level.height; y++){
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile(int x, int y){
		if(GenerateRouters(x, y))
			return;

		if(!PixelIsTerrain(x, y))
			return;

		GameObject parent = Instantiate(block, new Vector3(x, y), Quaternion.identity);
		GameObject sprite;

		if(!PixelIsTerrain(x, y + 1)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.parent = parent.transform;
		}

		if(!PixelIsTerrain(x - 1, y)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.Rotate(0, 0, 90);
			sprite.transform.parent = parent.transform;
		}

		if(!PixelIsTerrain(x, y - 1)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.Rotate(0, 0, 180);
			sprite.transform.parent = parent.transform;
		}

		if(!PixelIsTerrain(x + 1, y)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.Rotate(0, 0, 270);
			sprite.transform.parent = parent.transform;
		}

		parent.transform.parent = transform;
	}

	bool PixelIsTerrain(int x, int y){
		if(x < 0 || x >= level.width)
			return false;
		if(y < 0 || y >= level.height)
			return false;

		Color color = level.GetPixel(x, y);
		return (color == Color.black);
	}

	bool GenerateRouters(int x, int y){
		Color color = level.GetPixel(x, y);
		
		if(color == Color.green){
			Instantiate(routerA, new Vector3(x, y + 0.5f), Quaternion.identity);
			return true;
		}

		if(color == Color.red){
			Instantiate(routerB, new Vector3(x, y + 0.5f), Quaternion.identity);
			return true;
		}
		return false;
	}

	public bool SaveScore(float score){
		if(score <= best) return false;
		best = score;

		int stars = 0;
		if(score > bronze) stars += 1;
		if(score > silver) stars += 1;
		if(score > gold) stars += 1;
			
		PlayerPrefs.SetFloat(level_name + "_score", score);
		PlayerPrefs.SetInt(level_name + "_stars", stars);
		PlayerPrefs.Save();

		return true;
	}

	void LoadScore(){
		float previous_score = PlayerPrefs.GetFloat(level_name + "_score", best);
		if(previous_score > best) best = previous_score;
	}
}

