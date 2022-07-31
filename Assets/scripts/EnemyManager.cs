using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager  Instance;
    public GameObject EnemyPrefab;
    public Vector3 spawnPoint ;
    Transform PlayerTransform;
    GameObject Enemy;

    int enemyCounter;
    private PhotonView pv;
    public deleteEnemies deleteScript;
    List<Transform> players;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        players = new List<Transform>();
        enemyCounter = 0;
        pv = GetComponent<PhotonView>();

    }


    public GameObject makeEnemy(Vector3 spawnPoint)
    {
        Enemy = Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);
        enemyCounter++;
        var script = Enemy.GetComponent<SC_NPCEnemy>();
        if (script != null)     
        {
            script.setList(players);
            script.setMyCounter(enemyCounter);
        }
        GameObject toReturn = Enemy.gameObject;
        return toReturn;    
    }

    public void addPlayer(Transform PlayerTransform)
    {
        this.PlayerTransform = PlayerTransform;
        players.Add(PlayerTransform);
    }

    public void deleteNpcNum(int num)
    {
        if (deleteScript != null)
        {
            deleteScript.deleteNpcNum(num);
        }
    }

}
