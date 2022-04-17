using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class MazeRenderer : MonoBehaviourPun
{

    [SerializeField]
    [Range(1, 100)]
    private int width = 10; //width of the maze

    [SerializeField]
    [Range(1, 100)]
    private int height = 10; //height of the maze

    [SerializeField]
    private float size = 1f; //size of each wall

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    //   public float bias;
    public float Upbias;

    private PhotonView pv;


    public NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        int seed = (int)PhotonNetwork.CurrentRoom.CustomProperties["seed"];
        Debug.Log("The seed is " + seed);
        pv = GetComponent<PhotonView>();
        var maze = MazeGenerator.Generate(width, height,seed); //generates a 2D array of a maze (in my case its using Recursive Backtracker)
        Draw(maze);


        surface.BuildNavMesh();

    }




    private void Draw(WallState[,] maze)
    {
       //  var floor = Instantiate(floorPrefab, transform);
     //  floor.localScale = new Vector3(width, 1, height);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, Upbias, -height / 2 + j); // the position of the the cell
                if (cell.HasFlag(WallState.UP)) // draw up wall of the cell
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);// the position of the wall 
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);// the size of the wall

                    TakeWallDamage dmg = topWall.GetComponent<TakeWallDamage>();
                    dmg.AddSurface(this.surface);

                }

                if (cell.HasFlag(WallState.LEFT)) // draw left wall
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);

                    TakeWallDamage dmg = leftWall.GetComponent<TakeWallDamage>();
                    dmg.AddSurface(this.surface);
                }

                if (i == width - 1) // draw the right part of the maze
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);

                        TakeWallDamage dmg = rightWall.GetComponent<TakeWallDamage>();
                        dmg.AddSurface(this.surface);
                    }
                }

                if (j == 0) // draw the down part of the maze
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);


                        TakeWallDamage dmg = bottomWall.GetComponent<TakeWallDamage>();
                        dmg.AddSurface(this.surface);

                    }
                }
            }

        }

    }

}
