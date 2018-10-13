using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSound : MonoBehaviour {

    public static AudioSource Instance;

    // Use this for initialization
    void Start()
    {
        Instance = GetComponent<AudioSource>();
    }
}
