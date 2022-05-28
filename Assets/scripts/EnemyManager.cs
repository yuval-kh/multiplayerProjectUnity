using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager  Instance;
   //  PhotonView pv;
    public GameObject EnemyPrefab;
    public Vector3 spawnPoint ;
    Transform PlayerTransform;
    GameObject Enemy;

    ///// 
    int enemyCounter;
    private PhotonView pv;
    public deleteEnemies deleteScript;
    ////

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ////////
        enemyCounter = 0;
        pv = GetComponent<PhotonView>();
       
        ////////




        spawnPoint = new Vector3(35, 0, -40);
               Enemy = Instantiate (EnemyPrefab, spawnPoint, Quaternion.identity);
        /////////////
        // Enemy = PhotonNetwork.Instantiate ("Enemy", spawnPoint, Quaternion.identity);
        enemyCounter++;
        var script = Enemy.GetComponent<SC_NPCEnemy>();
        if( script != null)
        {
            script.setMyCounter(enemyCounter);
        }


        ///////////
    }

    public void addPlayer(Transform PlayerTransform)
    {
        this.PlayerTransform = PlayerTransform;
        SC_NPCEnemy en = Enemy.GetComponent<SC_NPCEnemy>();
        en.addPlayer(PlayerTransform);
    }


    /////////////////////////
    public void deleteNpcNum(int num)
    {
        //this.pv.RPC("deleteEnemytoAll", RpcTarget.All, num);
               if (deleteScript != null)
                {
                    deleteScript.deleteNpcNum(num);
                }
    }
  /*  [PunRPC]
    public void deleteEnemytoAll(int num)
    {
        Debug.Log(" i have to delete the " + num + "enemy");
    }*/


    /////////////////////////
}
