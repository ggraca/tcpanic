using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsMenuLogic : MonoBehaviour {
	public int levelNumber = 1;
	public int numSubLevels = 3;

	private int selectedSubLevel = 1;

	private string levelName = null;

	public Text stars = null;
	public Text score = null;

	private float timer = 0f;

	public GameObject levelAudioPrefab;
	private AudioSource levelAudio;

	// Use this for initialization
	void Start () {
		levelName = "level_" + levelNumber;

		if(levelAudioPrefab != null) levelAudio = Instantiate(levelAudioPrefab).GetComponent<AudioSource>();
		if(levelAudio != null && !levelAudio.isPlaying) levelAudio.Play();
	}

	void Update() {
		if(timer < .5f) {
			timer += Time.deltaTime;
			if(!(timer < .5f)) {
				selectedSubLevel = 1;
				SelectSubLevel(selectedSubLevel);
				bool prevComplete = true;
				for(int i=1; i<= numSubLevels && prevComplete; i++) {
					MakeReachableSubLevel(i);
					prevComplete = getSubLevelStars(i) > 0;
				}
				
			}
			return;
		}

		if(Input.GetKeyDown("right") && selectedSubLevel < numSubLevels) {
			selectedSubLevel++;
			SelectSubLevel(selectedSubLevel);
		}

		if(Input.GetKeyDown("left") && selectedSubLevel > 1) {
			selectedSubLevel--;
			SelectSubLevel(selectedSubLevel);
		}

		if(Input.GetButtonDown("Submit")) {
			switch(selectedSubLevel) {
				case 1: SceneManager.LoadScene("scene1"); break;
				default: break;
			}
		}
	}

	void MakeReachableSubLevel(int subLevel) {
		foreach(LevelRoute r in FindObjectsOfType(typeof(LevelRoute))) {
			if(r.subLevelNumber == subLevel) {
				r.MakeReachable();
			}
		}
	}

	IEnumerator WaitForSecondsWrapper(float secs)
    {
        yield return new UnityEngine.WaitForSeconds(secs);
    }

	void SelectSubLevel(int subLevel) {
		foreach(LevelRoute r in FindObjectsOfType(typeof(LevelRoute))) {
			if(r.subLevelNumber == subLevel) {
				r.Select();
				if(r.GetStars() == 0) {
					if(!r.Reachable()) {
						stars.text = "Route unreachable, complete the previous routes first";
						score.text = "";
					} else {
						stars.text = "Route not yet completed. Enter to try";
						score.text = "";
					}
				} else {
					stars.text = "Awesome! " + r.GetStars() + " Stars";
					score.text = "Highscore: " + r.GetScore();
				}
			} else {
				r.Deselect();
			}
		}
	}

	string subLevelName(int subLevel) {
		return levelName + "_" + subLevel;
	}

	int getSubLevelStars(int subLevel) {
		foreach(LevelRoute r in FindObjectsOfType(typeof(LevelRoute))) {
			if(r.subLevelNumber == subLevel) {
				return r.GetStars();
			}
		}
		return 0;
	}

	
}
