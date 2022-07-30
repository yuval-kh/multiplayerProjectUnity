using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatisticsHolder
{
    private static int _EnemiesKilled = 0;
    private static int _timePlayed = 0; // will be int number - the minutes that the player played.
    private static int _gamesPlayed = 0;
    private static int _totalDeaths = 0;
    private static int maxWave = 0;
    private static int _bulletsShot = 0;
    public static bool isCalled = false;


    public static int EnemiesKilled
    {
        get { return _EnemiesKilled; }
        set
        {
            _EnemiesKilled = value;
            if (DBManager.LoggedIn)
            {
                GetDBStats.Instance.CallSetStatistics("EnemiesKilled", value);
            }
        }
    }
    public static int timePlayed
    {
        get { return _timePlayed; }
        set
        {
            _timePlayed = value;
            if (DBManager.LoggedIn)
            {
                GetDBStats.Instance.CallSetStatistics("timePlayed", value);
            }
        }
    }

    public static int gamesPlayed
    {
        get { return _gamesPlayed; }
        set
        {
            _gamesPlayed = value;
            if (DBManager.LoggedIn)
            {
                GetDBStats.Instance.CallSetStatistics("gamesPlayed", value);
            }
        }
    }
    public static int totalDeaths
    {
        get { return _totalDeaths; }
        set
        {
            _totalDeaths = value;
            if (DBManager.LoggedIn)
            {
                GetDBStats.Instance.CallSetStatistics("totalDeaths", value);
            }
        }
    }

    public static int bulletsShot
    {
        get { return _bulletsShot; }
        set
        {
            _bulletsShot = value;
            if (DBManager.LoggedIn)
            {
                GetDBStats.Instance.CallSetStatistics("bulletsShot", value);
            }
        }
    }

    public static float kdRatio
    {
        get
        {
            float toReturn;
            if (totalDeaths == 0) toReturn = 0;
            else toReturn  = EnemiesKilled / (float)totalDeaths;
            return toReturn;
        }
    }
    public static int maxWaveReached {
        get { return maxWave; }
        set
        {
            if (value > maxWave)
            {
                maxWave = value;
                if (DBManager.LoggedIn)
                {
                    GetDBStats.Instance.CallSetStatistics("maxWaveReached", value);
                }
            }
        }
    }
}
