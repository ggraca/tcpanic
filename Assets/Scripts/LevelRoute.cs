using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoute : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	private Color unreachableColor = Color.black;
	private Color reachableColor = Color.white;

	private int defaultLayer = 0;
	private int selectedLayer = 8;

	private bool selected = false;
	private int reachable = 0;
	private int stars = 0;
	private float score = 0f;

	public int levelNumber = 1;
	public int subLevelNumber = 1;

	public bool isReady = false;
	
	private string levelName = null;
	private string levelReachable = null;
	private string levelStars = null;
	private string levelScore = null;

	private float layerTimer = 0f;

	// Use this for initialization
	void Start () {
		levelName = "level_" + levelNumber + "_" + subLevelNumber;
		levelReachable = levelName + "_reachable";
		levelStars = levelName + "_stars";
		levelScore = levelName + "_score";

		spriteRenderer = GetComponent<SpriteRenderer>();

		reachable = PlayerPrefs.GetInt(levelReachable, reachable);
		if(reachable == 1) {
			spriteRenderer.color = reachableColor;
		} else {
			spriteRenderer.color = unreachableColor;
		}

		stars = PlayerPrefs.GetInt(levelStars, stars);
		score = PlayerPrefs.GetFloat(levelScore, score);
	}

	void Update() {
		layerTimer += Time.deltaTime;
		if(layerTimer > 0.5f) {
			layerTimer = 0f;
			if(selected ) {
				if(gameObject.layer == defaultLayer) {
					gameObject.layer = selectedLayer;
				} else {
					gameObject.layer = defaultLayer;
				}
			}
		}
	}

	public void MakeReachable() {
		reachable = 1;
		PlayerPrefs.SetInt(levelReachable, reachable);
		spriteRenderer.color = reachableColor;
	}

	public void MakeUnreachable() {
		reachable = 0;
		PlayerPrefs.SetInt(levelReachable, reachable);
		spriteRenderer.color = unreachableColor;
	}
	
	public void Select() {
		selected = true;
		gameObject.layer = selectedLayer;
	}

	public void Deselect() {
		selected = false;
		gameObject.layer = defaultLayer;
	}

	public int GetStars() {
		return stars;
	}

	public float GetScore() {
		return score;
	}

	public bool Reachable() {
		return reachable > 0;
	}
}
