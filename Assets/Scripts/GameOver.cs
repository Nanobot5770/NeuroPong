using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public AudioSource loseSound;
    public AudioSource bgMusic;
    public Text[] labels;
    public float delay;

    int li = 0;
    float lastTime;

	// Use this for initialization
	void Start () {
        lastTime = Time.time;
        bgMusic.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        if(li < labels.Length && Time.time - lastTime > delay) {
            if(li == 0) { loseSound.Play(); }
            lastTime = Time.time;
            labels[li++].color = new Color(255,255,255,255);
        }
	}
}
