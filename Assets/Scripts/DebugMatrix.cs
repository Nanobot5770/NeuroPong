using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMatrix : MonoBehaviour {

    public static DebugMatrix Instance;
    public static Text txt;

	// Use this for initialization
	void Start () {
        Instance = this;
        txt = GameObject.Find("Debug Matrix").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void UpdateMatrix(Transform[,] m) {
        string s = "";
        for (int r = 19; r >= 0; r--) {
            for (int c = 0; c < 10; c++) {
                s += m[c, r] != null ? " o " : " - ";
            }
            s += "\n";
        }
        txt.text = s;
    }
}
