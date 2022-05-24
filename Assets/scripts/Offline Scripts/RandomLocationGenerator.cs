using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLocationGenerator : MonoBehaviour
{
    float minX, maxX;
    float minY, maxY;
    float minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        var maze = GameObject.Find("MazeRenderer");
        minX = minY = minZ = 9999;
        maxX = maxY = maxZ = -9999;
        foreach (Transform child in maze.transform)
        {
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
        Debug.Log("The X is: " + minX + "The y is: "+ minY + "The Z is: " + minZ);
        Debug.Log("MAX:The X is: " + maxX + "The y is: " + maxY + "The Z is: " + maxZ);

        transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
