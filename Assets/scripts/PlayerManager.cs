using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    public Vector3 spawnPoint;
    //  public Vector3 spawnPoint = new Vector3(25, 0, -27);

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        GameObject obj = GameObject.Find("LocationGenerator");
        if(obj != null)
        {
        int counter = 0;
        var scr = obj.GetComponent<LocationGenerator>();
            /*bool isRightLocation = false;
            while (!isRightLocation && counter < 100)
            {
                spawnPoint = scr.generateLocation();
                Collider[] hitColliders = Physics.OverlapSphere(spawnPoint, 3f);
                counter++;
                if (hitColliders.Length == 0)
                    isRightLocation = true;
                Debug.Log(counter + " " + hitColliders.Length);
                //   if (hitColliders.Length == 1) { 
                foreach (var col in hitColliders)
                    {
                        Debug.Log(col.gameObject.name);
                    }
             //   }
                   // isRightLocation = true;
            }
*//*            do
            {
                counter++;
                if (counter > 100)
                    break;
                Debug.Log(spawnPoint);
            } while (!Physics.CheckSphere(spawnPoint, 0.1f));*/
            spawnPoint = scr.generateLocation();
        }
        else
        {
            spawnPoint = new Vector3(25, 0, -27);
        }
        //spawnPoint = new Vector3(25, 0, -27);
        if (pv.IsMine) //every player creates only his own character.
        {
            CreateController();
        }

    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate("Player", spawnPoint, Quaternion.identity);
    }
}
