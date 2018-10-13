using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {

    // Time since last gravity tick
    float lastFall = 0;

    Logic logic;

    private bool waitForSpawner = false;

    // Use this for initialization
    void Start () {
        logic = Logic.Instance;

        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-Logic.stepX, 0, 0);

            // See if valid
            if (isValidGridPos())
                // Its valid. Update grid.
                updateGrid();
            else
                // Its not valid. revert.
                transform.position += new Vector3(Logic.stepX, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(Logic.stepX, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-Logic.stepX, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //transform.Rotate(0, 0, -90);

            //// See if valid
            //if (isValidGridPos())
            //    // It's valid. Update grid.
            //    updateGrid();
            //else
                //// It's not valid. revert.
                //transform.Rotate(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -Logic.stepY, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, Logic.stepY, 0);

                // Clear filled horizontal lines
                logic.deleteFullRows();

                Spawner.Instance.SpawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }


    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            // need to unit out of real position
            //Vector2 v = Grid.roundVec2(child.position);
            //Debug.Log(child.position);
            Vector2 v = logic.roundVec2(child.position);
            //Debug.Log(v);

            // Not inside Border?
            if (!logic.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Logic.grid[(int)v.x, (int)v.y] != null &&
                Logic.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }


    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Logic.h; ++y)
            for (int x = 0; x < Logic.w; ++x)
                if (Logic.grid[x, y] != null)
                    if (Logic.grid[x, y].parent == transform)
                        Logic.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = logic.roundVec2(child.position);
            Logic.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
