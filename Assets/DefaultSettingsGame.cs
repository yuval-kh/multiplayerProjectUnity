using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefaultSettingsGame
{
    public static int playerSpeed;
    public static int enemyHealth;
    public static int enemySpeed;
    public static int enemyDamage;
    public static int ActivationDistance;
    public static int FirstRound;
    public static int MazeSize;
    public static bool isCalled = false;

/*    public static void initiateValues()
    {
        if (!DBManager.LoggedIn)
        {
            Debug.Log("here");
            playerSpeed = 8;
            enemyHealth = 200;
            enemySpeed = 8;
            enemyDamage = 5;
            ActivationDistance = 10;
            FirstRound = 2;
            MazeSize = 10;
        }
        else
        {
            GetDBStats.Instance.CallGetStats();
        }
    }*/

}
