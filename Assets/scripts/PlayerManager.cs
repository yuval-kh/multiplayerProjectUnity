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
        if (obj != null)
        {
            int counter = 0;
            var scr = obj.GetComponent<LocationGenerator>();
            if (scr != null)
            {
                spawnPoint = scr.generateLocation();
            } else if (obj.GetComponent<locationGeneratorMap1>() != null)
            {
                var sc = obj.GetComponent<locationGeneratorMap1>();
                spawnPoint = sc.generateLocation();
            }
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
