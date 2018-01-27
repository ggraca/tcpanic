using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public GameObject prefab;
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
		Color color = level.GetPixel(x, y);

		if(color.a != 0)
			Instantiate(prefab, new Vector3(x, y), Quaternion.identity);
	}
}
