using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LocationGenerator : MonoBehaviour
{
    float minX, maxX;
    float minY, maxY;
    float minZ, maxZ;

    List<GameObject> lst;

    Vector3 SamePosition;

    
    [SerializeField]
    bool isSameToAll;


    List<Vector3> staticLocationslst;



    // Start is called before the first frame update
    void Start()
    {


        var maze = GameObject.Find("MazeRenderer");
        if (maze != null)
        {
            minX = minY = minZ = 9999;
            maxX = maxY = maxZ = -9999;
            lst = new List<GameObject>();
            foreach (Transform child in maze.transform)
            {
                lst.Add(child.gameObject);
                var position = child.position;
                if (position.x < minX)
                    minX = position.x;
                if (position.y < minY)
                    minY = position.y;
                if (position.z < minZ)
                    minZ = position.z;

                if (position.x > maxX)
                    maxX = position.x;
                if (position.y > maxY)
                    maxY = position.y;
                if (position.z > maxZ)
                    maxZ = position.z;
            }

            //     transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));

            SamePosition = generateLocation();
        }
        else
        {
            var locations = GameObject.Find("SpawnLocations");
            staticLocationslst = new List<Vector3>();
            foreach (Transform child in locations.transform)
            {
            //    Debug.Log(child.position);
                staticLocationslst.Add(child.position);
            }
            SamePosition = generateLocation();
        }


    }


    public Vector3 generateLocation()
    {
        if (!isSameToAll)
        {
            return generateRandomVector();
        }
        else return SamePosition;
    }

    public Vector3 generateRandomVector()
    {
            var maze = GameObject.Find("MazeRenderer");
        if (maze != null)
        {
            var scr = maze.GetComponent<MazeRenderer>();
            Vector3[,] lst;
            if (scr != null)
            {
                lst = scr.getLocations();
            }
            else
            {
                var scrOffline = maze.GetComponent<MazeRendererOffline>();
                lst = scrOffline.getLocations();
            }
            int rndi = Random.Range(0, lst.GetLength(0));
            int rndj = Random.Range(0, lst.GetLength(1));
            return lst[rndi, rndj];
        }
        else
        {
            int length = staticLocationslst.Count;
            int rndIndex = Random.Range(0, length);
            return staticLocationslst[rndIndex];
        }

    }
    public float getFloorHeight()
    {
        return maxY;


    }


    // Update is called once per frame
    void Update()
    {
         
    }
}
