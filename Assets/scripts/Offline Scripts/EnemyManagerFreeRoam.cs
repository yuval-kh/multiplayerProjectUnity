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

    int EnemyHP;
    int EnemySpeed;
    public GameObject npcDeadPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnemyHP = SetGameSettings.Instance.getEnemyHealth();
        EnemySpeed = SetGameSettings.Instance.getEnemySpeed();
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
               // var en = Enemy.GetComponent<SC_NPCEnemy>();
             //   en.addPlayer(PlayerTransform);

            var en = Enemy.GetComponent<NPCEnemyOffline>();
            if (en != null)
            {
                en.isActivateAtDist = false;
            }
            var enTurorial = Enemy.GetComponent<NPCEnemyOfflineTutorial>();
            if (enTurorial != null)
            {
                enTurorial.setDeadPrefab(npcDeadPrefab);
            }
            ///////////////09.06
            //           en.setMovementSpeed(EnemySpeed);

        }
    }


}
