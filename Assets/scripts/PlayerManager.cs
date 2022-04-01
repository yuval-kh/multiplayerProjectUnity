using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    public Vector3 spawnPoint = new Vector3(25, 0, -27);

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        spawnPoint = new Vector3(25, 0, -27);
        if (pv.IsMine) //every player creates only his own character.
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate("Player", spawnPoint, Quaternion.identity); // PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
