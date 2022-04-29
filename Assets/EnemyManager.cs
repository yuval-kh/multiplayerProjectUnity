using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    // PhotonView pv;
    public GameObject EnemyPrefab;
    public Vector3 spawnPoint ;
    Transform PlayerTransform;
    GameObject Enemy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()////HERE!!!
    {
        spawnPoint = new Vector3(35, 0, -40);////////////////AAAA////
       // if (PhotonNetwork.IsMasterClient)
      //  {
      //      Enemy = PhotonNetwork.InstantiateRoomObject("Enemy", spawnPoint, Quaternion.identity);
     //   }
       Enemy = Instantiate (EnemyPrefab, spawnPoint, Quaternion.identity);///////////////////////AAA///////////
/*        SC_NPCEnemy en = Enemy.GetComponent<SC_NPCEnemy>();
        en.addPlayer(PlayerTransform);*/
    }

    public void addPlayer(Transform PlayerTransform)
    {
        //     if (!PhotonNetwork.IsMasterClient)
        //        return;
        this.PlayerTransform = PlayerTransform;
        SC_NPCEnemy en = Enemy.GetComponent<SC_NPCEnemy>();
        en.addPlayer(PlayerTransform);
    }
}
