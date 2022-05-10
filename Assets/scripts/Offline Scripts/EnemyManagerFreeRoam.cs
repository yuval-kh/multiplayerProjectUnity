using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyManagerFreeRoam : MonoBehaviour
{
    public static EnemyManagerFreeRoam Instance;
    public GameObject EnemyPrefab;
    public Transform PlayerTransform;
    public Vector3 bias;
    //public Vector3 spawnPoint ;
    GameObject Enemy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
   //     spawnPoint = new Vector3(35, 0, -40);
    //   Enemy = Instantiate (EnemyPrefab, spawnPoint, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) )
        {
            if (PlayerTransform == null)
                Debug.Log("PlayerTransform  somehow is null :-(");
            Debug.Log("pressed I");
            Enemy = Instantiate(EnemyPrefab, PlayerTransform.position + bias, Quaternion.identity);
            var en = Enemy.GetComponent<SC_NPCEnemy>();
            en.addPlayer(PlayerTransform);
        }
    }


}
