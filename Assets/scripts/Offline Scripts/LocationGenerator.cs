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

    // Start is called before the first frame update
    void Start()
    {


        var maze = GameObject.Find("MazeRenderer");
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

    /*    public Vector3 generateLocation()
        {
            var loc = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
            bool isContains = false;
            foreach( var obj in lst)
            {
                var Collider = obj.GetComponent<BoxCollider>();
                if(Collider != null)
                {
                    if (Collider.bounds.Contains(loc))
                    {
                        isContains = true;
                    }
                }
            }
            if (isContains)
                generateLocation();
            return loc;


        }*/
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
    public float getFloorHeight()
    {
        return maxY;


    }


    // Update is called once per frame
    void Update()
    {
         
    }
}
