using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public GameObject block;
	public GameObject blockSprite;
	public GameObject routerA;
	public GameObject routerB;
	public Texture2D level;

	void Start () {
		GenerateLevel();

		GameLogic gl = (GameLogic) GameObject.FindObjectsOfType(typeof(GameLogic))[0];
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
}
