using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public static Spawner Instance { get; private set; }

    //public GameObject[] groups;

    public GameObject pieceS;
    public GameObject pieceZ;
    public GameObject pieceO;
    public GameObject pieceJ;
    public GameObject pieceI;
    public GameObject pieceL;
    public GameObject pieceT;

    public string nextPiece;
    public int nextRotation;
    public bool hasNextPiece;

    private bool waitForSpawner = false;

	// Use this for initialization
	void Start () {
        Instance = this;

        nextPiece = "";
        nextRotation = 0;

        hasNextPiece = false;
        waitForSpawner = true;

        //SpawnNext();
	}
	
	// Update is called once per frame
	void Update () {
		if(waitForSpawner && hasNextPiece)
        {
            waitForSpawner = false;
            SpawnNextPiece();
        }
	}

    public void SpawnNext() {
        if (hasNextPiece)
        {
            SpawnNextPiece();
        }
        else
        {
            waitForSpawner = true;
        }
        
        // Random Index
        //int i = Random.Range(0, groups.Length);

        

        /*int i = Random.Range(0, letters.Length);
        nextPiece = letters[i];

        i = Random.Range(0, rotations.Length);
        nextRotation = rotations[i];*/
    }

    private void SpawnNextPiece()
    {
        // Spawn Group at current Position
        GameObject newObj = Instantiate(GetNextPiece(),
                    transform.position,
                    Quaternion.identity,
                   transform.parent);

        newObj.transform.Rotate(0, 0, nextRotation);

        hasNextPiece = false;
    }

    private GameObject GetNextPiece()
    {
        if("s".Equals(nextPiece))
        {
            return pieceS;
        }
        else if("z".Equals(nextPiece))
        {
            return pieceZ;
        }
        else if ("j".Equals(nextPiece))
        {
            return pieceJ;
        }
        else if ("l".Equals(nextPiece))
        {
            return pieceL;
        }
        else if ("t".Equals(nextPiece))
        {
            return pieceT;
        }
        else if ("i".Equals(nextPiece))
        {
            return pieceI;
        }
        return pieceO;
    }
}
