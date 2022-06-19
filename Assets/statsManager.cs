using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsManager : MonoBehaviour
{
    public static statsManager Instance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if(StatisticsHolder.timePlayed == 0)
        {
            InvokeRepeating("updateTimePlayed", 1f, 60f);
        }
    }

    private void updateTimePlayed()
    {
        StatisticsHolder.timePlayed += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
