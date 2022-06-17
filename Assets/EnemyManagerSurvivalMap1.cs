using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyManagerSurvivalMap1 : MonoBehaviour
{
    public static EnemyManagerSurvivalMap1 Instance;
    public GameObject EnemyPrefab;
  //  public Transform PlayerTransform;
    public Vector3 bias;
    //public Vector3 spawnPoint ;
    GameObject Enemy;

    public float spawnInterval = 2; //Spawn new enemy each n seconds
    private int enemiesPerWave; //How many enemies per wave
    float nextSpawnTime = 0;
    int waveNumber;
    bool waitingForWave = true;
    float newWaveTimer = 0;
    int enemiesToEliminate;
    //How many enemies we already eliminated in the current wave
    int enemiesEliminated ;
    int totalEnemiesSpawned = 0;
    
    //min and max coordinates for generating a random enemy
    float minX, maxX;
    float minY, maxY;
    float minZ, maxZ;



    [SerializeField]
    EnemyManager manager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        initializeLimitCoordinates();
        //Wait 10 seconds for new wave to start
        newWaveTimer = 10;
     //   waitingForWave = false;


        //  Vector3 randomPosition = generateRandomPoint();
        // GameObject enemy = Instantiate(EnemyPrefab, randomPosition, Quaternion.identity);
        //   var script = enemy.GetComponent<NPCEnemyOffline>();
        // script.isActivateAtDist = true;
        //   script.activateDistance = 2;
        //   enemiesEliminated = 0;
        //        totalEnemiesSpawned = 1;
        //    enemiesToEliminate = 1;

        waitingForWave = true;
        enemiesPerWave = 2;

        waveNumber = SetGameSettings.Instance.getFirstRound();
    }

    private void Update()
    {
        //return;
        if (waitingForWave)
        {
        //    if (newWaveTimer >= 0)
      //      {
        //        newWaveTimer -= Time.deltaTime;
       //     }
         //   else
           // {
                //Initialize new wave
                enemiesToEliminate = waveNumber * enemiesPerWave;
            Debug.Log("wave num: "+ waveNumber);
            Debug.Log("enemies per wave" + enemiesPerWave);
            Debug.Log(enemiesToEliminate + "enemies to eliminate");
                enemiesEliminated = 0;
                totalEnemiesSpawned = 0;
                waitingForWave = false;
           // }
        }
        else
        {
           // if (Time.time > nextSpawnTime)
        //    {
         //       nextSpawnTime = Time.time + spawnInterval;

                //Spawn enemy 
                if (totalEnemiesSpawned < enemiesToEliminate)
                {
                    //Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
                    Vector3 randomPosition = generateRandomPoint();


                GameObject enemy;
                if (manager != null)
                {
                    enemy =manager.makeEnemy(randomPosition);
                }
                else {




                    enemy = Instantiate(EnemyPrefab, randomPosition, Quaternion.identity); }
                    var script = enemy.GetComponent<SC_NPCEnemy>();
                if (script != null)
                {
                    script.isActivateAtDist = true;
                    script.activateDistance = SetGameSettings.Instance.getActivationDist();
                }
                else
                {
                    var scriptOffline = enemy.GetComponent<NPCEnemyOffline>();
                    scriptOffline.isActivateAtDist = true;
                    scriptOffline.activateDistance = SetGameSettings.Instance.getActivationDist();

                }
                //    var npc = enemy.GetComponent<UpdateEnemyManagerSurvival>();
                //       npc.enemyManager = this;
                totalEnemiesSpawned++;
                }
          //  }
        }
    }

    private void initializeLimitCoordinates()
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
    }

    private Vector3 generateRandomPoint()
    {


  //      float rndX = Random.Range(minX, maxX);
        var locationGenerator = GameObject.Find("LocationGenerator");
        
        //if (locationGenerator != null)
        //{
            var sc = locationGenerator.GetComponent<LocationGenerator>();
        var result =  sc.generateRandomVector();
        return result;
   //         float y = sc.getFloorHeight();
        //}
      //    float y = PlayerTransform.position.y; // the y should not be random its the floor of the level here
      //  float rndZ = Random.Range(minZ, maxZ);
     //   return new Vector3(rndX, y, rndZ);
    }

    public void EnemyEliminated()
    {
        Debug.Log("enemy eliminated");
        Debug.Log(enemiesEliminated);
        Debug.Log(enemiesToEliminate);
        enemiesEliminated++;

        if (enemiesToEliminate - enemiesEliminated <= 0)
        {
            //Start next wave
            newWaveTimer = 10;
            waitingForWave = true;
            waveNumber++;
        }
    }
}
