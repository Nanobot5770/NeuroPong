using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    public AudioSource bgMusic;


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Destroy(this.gameObject);
            bgMusic.Play();
        }
    }
}
