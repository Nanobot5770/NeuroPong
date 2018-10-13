using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

    public static Spawner Instance { get; private set; }

    public Image nextPieceDisplay;
	
    //public GameObject[] groups;

    [Header("Prefabs")]
    public GameObject pieceS;
    public GameObject pieceZ;
    public GameObject pieceO;
    public GameObject pieceJ;
    public GameObject pieceI;
    public GameObject pieceL;
    public GameObject pieceT;

    [Header("Images")]
    public Sprite imageS;
    public Sprite imageZ;
    public Sprite imageO;
    public Sprite imageJ;
    public Sprite imageI;
    public Sprite imageL;
    public Sprite imageT;

    private struct PieceParameter
    {
        public string piece;
        public int rotation;

        public PieceParameter(string piece, int rotation)
        {
            this.piece = piece;
            this.rotation = rotation;
        }
    }

    Queue<PieceParameter> nextPieces;

    private bool waitForSpawner = false;

	// Use this for initialization
	void Start () {
        nextPieces = new Queue<PieceParameter>();

        Instance = this;
        waitForSpawner = true;

        //SpawnNext();
	}
	
	// Update is called once per frame
	void Update () {
		if(waitForSpawner && nextPieces.Count > 0)
        {
            waitForSpawner = false;
            SpawnNextPiece();
        }
	}

    public void QueuePiece(string piece, int rotation)
    {
        nextPieces.Enqueue(new PieceParameter(piece, rotation));

        if(waitForSpawner)
        {
            waitForSpawner = false;
            SpawnNextPiece();
        }
        else if(nextPieces.Count == 1)
        {
            UpdateNextPiece();
        }
    }

    public void SpawnNext() {
        if (nextPieces.Count > 0)
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
	
	public void UpdateNextPiece() 
	{
        PieceParameter parameter = nextPieces.Peek();

        nextPieceDisplay.sprite = GetNextImage(parameter);
        nextPieceDisplay.color = Color.white;
    }

    private Sprite GetNextImage(PieceParameter parameter)
    {
        if ("s".Equals(parameter.piece))
        {
            return imageS;
        }
        else if ("z".Equals(parameter.piece))
        {
            return imageZ;
        }
        else if ("j".Equals(parameter.piece))
        {
            return imageJ;
        }
        else if ("l".Equals(parameter.piece))
        {
            return imageL;
        }
        else if ("t".Equals(parameter.piece))
        {
            return imageT;
        }
        else if ("i".Equals(parameter.piece))
        {
            return imageI;
        }
        return imageO;
    }

    private void SpawnNextPiece()
    {
        PieceParameter parameter = nextPieces.Dequeue();

        // Spawn Group at current Position
        GameObject newObj = Instantiate(GetNextPiece(parameter),
                    transform.position,
                    Quaternion.identity,
                   transform.parent);

        newObj.transform.Rotate(0, 0, parameter.rotation);

        nextPieceDisplay.color = new Color(0,0,0,0);
        if(nextPieces.Count > 0) { UpdateNextPiece(); }
    }

    private GameObject GetNextPiece(PieceParameter parameter)
    {
        if("s".Equals(parameter.piece))
        {
            return pieceS;
        }
        else if("z".Equals(parameter.piece))
        {
            return pieceZ;
        }
        else if ("j".Equals(parameter.piece))
        {
            return pieceJ;
        }
        else if ("l".Equals(parameter.piece))
        {
            return pieceL;
        }
        else if ("t".Equals(parameter.piece))
        {
            return pieceT;
        }
        else if ("i".Equals(parameter.piece))
        {
            return pieceI;
        }
        return pieceO;
    }
}
