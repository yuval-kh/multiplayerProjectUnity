using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RandomLocationGenerator : MonoBehaviour
{
    float minX, maxX;
    float minY, maxY;
    float minZ, maxZ;

    bool isSametoAll;

    // Start is called before the first frame update
    void Start()
    {

        var pv = gameObject.GetComponent<PhotonView>();
        if (pv == null)
            return;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("IN THE WRONG IF");

            if (pv == null)
                return;
            if (!pv.AmOwner)
                return;
            isSametoAll = true;
            if (!PhotonNetwork.IsMasterClient)
                return;
        }




        if (!pv.AmOwner)
            return;
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
        Debug.Log("position: X: " + transform.position.x  + "Y: " + transform.position.y + "Z: " + transform.position.z);



        /////////////////
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("IN THE WRONG IF");
            if (PhotonNetwork.IsMasterClient)
            {
          //      var pv = gameObject.GetComponent<PhotonView>();
                float x = transform.position.x;
                float y = transform.position.y;
                float z = transform.position.z;
                if(pv == null)
                {
                    Debug.Log("!!!!!!!!!!!!!!!!11");
                }
                if (pv != null)
                {
                    pv.RPC("generateSameLocation", RpcTarget.All, x, y, z);
                }
            }
        }
        ////////////////
    }

    [PunRPC]
    public void generateSameLocation(float x, float y, float z)
    {
        /*Debug.Log("X: " + x + "  Y: " + y + "z: " + z);
        Debug.Log("X: !!!!!!!!!!!!!!!!!!!!!111111111111111!!!!!!!!!!!!!");
        //    return;
        Vector3 newPosition = new Vector3(x, y, z);
        transform.position = newPosition;*/
        Debug.Log("X: !!!!!!!!!!!!!!!!!!!!!111111111111111!!!!!!!!!!!!!");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 newPosition = new Vector3(x, y, z);
        transform.position = newPosition;
        foreach(var player in players)
        {
            player.transform.position = newPosition;
        }


    }



    // Update is called once per frame
    void Update()
    {
         
    }
}
