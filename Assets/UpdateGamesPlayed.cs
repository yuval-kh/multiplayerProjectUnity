using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGamesPlayed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StatisticsHolder.gamesPlayed++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
