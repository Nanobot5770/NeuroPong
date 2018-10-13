using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    public Animation instructions;

    float startTime;
    bool dipslayed;

	// Use this for initialization
	void Start () {
        GetComponent<Animation>().Play();

        startTime = Time.time;
	}

    private void Update()
    {
        if(Time.time - startTime > 12) {
            instructions.Play();
        }
    }
}
