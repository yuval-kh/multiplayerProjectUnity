using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class EnemyManagerSurvivalOnline : MonoBehaviour
{
    public static EnemyManagerSurvivalOnline Instance;
    public GameObject EnemyPrefab;
    //  public Transform PlayerTransform;
    public Vector3 bias;
    //public Vector3 spawnPoint ;
    GameObject Enemy;

    public float spawnInterval = 2; //Spawn new enemy each n seconds
    private int enemiesPerWave; //How many enemies per wave
    int waveNumber;
    int enemiesToEliminate;
    //How many enemies we already eliminated in the current wave
    int enemiesEliminated;
    int totalEnemiesSpawned = 0;

    //min and max coordinates for generating a random enemy
    float minX, maxX;
    float minY, maxY;
    float minZ, maxZ;
    PhotonView pv;


    private Transform player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {



        pv = gameObject.GetComponent<PhotonView>();
        totalEnemiesSpawned = 0;
        enemiesEliminated = 0;
        waveNumber = SetGameSettings.Instance.getFirstRound();
        enemiesPerWave = 2;
        enemiesToEliminate = waveNumber * enemiesPerWave;

        ///18.06
        ///
        StatisticsHolder.gamesPlayed++;
        StatisticsHolder.maxWaveReached = waveNumber;
        //


        initializeLimitCoordinates();
    }

    private void Update()
    {


        //Spawn enemy 
        if (totalEnemiesSpawned < enemiesToEliminate)
        {
            Vector3 randomPosition = generateRandomPoint();
            GameObject enemy;
            enemy = Instantiate(EnemyPrefab, randomPosition, Quaternion.identity);
            var script = enemy.GetComponent<NPCEnemySurvival>();
            script.isActivateAtDist = true;
            script.activateDistance = 2;

            if (this.player != null)
                script.Addplayer(player);

            totalEnemiesSpawned++;
        }


    }

    private void initializeLimitCoordinates()
    {


        var maze = GameObject.Find("MazeRenderer");
        if (maze == null)
            return;
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
        var locationGenerator = GameObject.Find("LocationGenerator");
        var sc = locationGenerator.GetComponent<LocationGenerator>();
        return sc.generateRandomVector();
    }

    public void EnemyEliminated()
    {
        enemiesEliminated++;

        if (enemiesToEliminate - enemiesEliminated <= 0)
        {
            //Start next wave
            pv.RPC("addWave", RpcTarget.All, null);
           // waveNumber++;
        }
    }
    public void addPlayer(Transform pl)
    {
        this.player = pl;
    }

    [PunRPC]
    public void addWave()
    {
        waveNumber++;
        enemiesToEliminate +=  waveNumber * enemiesPerWave;


        ///18.06
        ///
        StatisticsHolder.maxWaveReached = waveNumber;
        //
    }
}
