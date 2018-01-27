using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public GameObject block;
	public GameObject blockSprite;
	public Texture2D level;
	
	// Use this for initialization
	void Start () {
		GenerateLevel();
	}

	void GenerateLevel(){
		for(int x = 0; x < level.width; x++){
			for(int y = 0; y < level.height; y++){
				GenerateTile(x, y);
			}
		}
	}

	void GenerateTile(int x, int y){
		if(!PixelHasTerrain(x, y))
			return;

		GameObject parent = Instantiate(block, new Vector3(x, y), Quaternion.identity);
		GameObject sprite;

		if(!PixelHasTerrain(x, y + 1)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.parent = parent.transform;
		}

		if(!PixelHasTerrain(x - 1, y)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.Rotate(0, 0, 90);
			sprite.transform.parent = parent.transform;
		}

		if(!PixelHasTerrain(x, y - 1)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.Rotate(0, 0, 180);
			sprite.transform.parent = parent.transform;
		}

		if(!PixelHasTerrain(x + 1, y)){
			sprite = Instantiate(blockSprite, new Vector3(x, y), Quaternion.identity);
			sprite.transform.Rotate(0, 0, 270);
			sprite.transform.parent = parent.transform;
		}
		
	}

	bool PixelHasTerrain(int x, int y){
		if(x < 0 || x >= level.width)
			return false;
		if(y < 0 || y >= level.height)
			return false;

		Color color = level.GetPixel(x, y);
		return !(color.a == 0);
	}

}
