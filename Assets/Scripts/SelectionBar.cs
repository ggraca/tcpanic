using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBar : MonoBehaviour {
	
	public float initialPosition = 12f;
	public float increment = -2f;
	public int item = 0;
	public int maxItems = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("down")) {
			item++;
			this.transform.position += new Vector3(0f, increment);
		}

		if(Input.GetKeyDown("up")) {
			item--;
			this.transform.position -= new Vector3(0f, increment);
		}

		if(item >= maxItems) {
			item = 0;
			this.transform.position = new Vector3(this.transform.position.x, initialPosition);
		} else if(item < 0) {
			item = maxItems - 1;
			this.transform.position = new Vector3(this.transform.position.x, initialPosition + increment * (maxItems-1));
		}		
	}
}
