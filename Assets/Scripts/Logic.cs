using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour {

    public static Logic Instance;

    // The Grid itself
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    public static float realWidth = 100f, realHeight = 200f;
    public static float stepX = Mathf.Round(realWidth / 10), stepY = Mathf.Round(realHeight / 20);

    public Transform anchor;

    // Use this for initialization
    void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector2 roundVec2(Vector2 v)
    {
        //return new Vector2(Mathf.Round(v.x),
        //Mathf.Round(v.y));
        float x = v.x - anchor.position.x, y = v.y - anchor.position.y;
        x -= 30; y -= 5;
        //float x = Mathf.Round((v.x - anchor.position.x - 25) / realWidth);
        //float y = Mathf.Round((v.y - anchor.position.y) / realHeight);
        Vector2 v2 = new Vector2(Mathf.Round(x/10), Mathf.Round(y/10));
        Debug.Log(v2);
        return v2;
    }


    public bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }

    public void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }


    public void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    public bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    public void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }
}
