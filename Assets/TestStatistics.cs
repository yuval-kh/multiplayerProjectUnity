using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatistics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Time played: " + StatisticsHolder.timePlayed);
            Debug.Log("max Wave Reached: " + StatisticsHolder.maxWaveReached);
            Debug.Log("games played: " + StatisticsHolder.gamesPlayed);

            Debug.Log("Enemies Killed: " + StatisticsHolder.EnemiesKilled);
            Debug.Log("total Deaths: " + StatisticsHolder.totalDeaths);

        }
    }
}
