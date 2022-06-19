using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDBStats : MonoBehaviour
{
    public static GetDBStats Instance;

    private void Awake()
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
    public void CallGetStats()
    {
        StartCoroutine(GetPlayerStats());
    }
    private IEnumerator GetPlayerStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        WWW www = new WWW("http://localhost/sqlconnect/getStats.php", form);
        yield return www;
        string text = www.text.ToString();
        Debug.Log(text);
        if (www.text[0] == '0')
        {
            var DBStats = text.Split('|');
            DefaultSettingsGame.playerSpeed = int.Parse(DBStats[1]);
            DefaultSettingsGame.enemyHealth = int.Parse(DBStats[2]);
            DefaultSettingsGame.enemySpeed = int.Parse(DBStats[3]);
            DefaultSettingsGame.enemyDamage = int.Parse(DBStats[4]);
            DefaultSettingsGame.ActivationDistance = int.Parse(DBStats[5]);
            DefaultSettingsGame.FirstRound = int.Parse(DBStats[6]);
            DefaultSettingsGame.MazeSize = int.Parse(DBStats[7]);
            DefaultSettingsGame.isCalled = true;
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!DBManager.LoggedIn)
        {
            if (!DefaultSettingsGame.isCalled)
            {
                Debug.Log("here");
                DefaultSettingsGame.playerSpeed = 8;
                DefaultSettingsGame.enemyHealth = 200;
                DefaultSettingsGame.enemySpeed = 8;
                DefaultSettingsGame.enemyDamage = 5;
                DefaultSettingsGame.ActivationDistance = 10;
                DefaultSettingsGame.FirstRound = 2;
                DefaultSettingsGame.MazeSize = 10;
                DefaultSettingsGame.isCalled = true;
            }
        }
        else
        {
            GetDBStats.Instance.CallGetStats();
            if (StatisticsHolder.isCalled == false)
            {
                StatisticsHolder.isCalled = true;
                CallGetStatisticsFromDB();
            }
        }
    }

    public void CallSetStats()
    {
        StartCoroutine(SetPlayerStats());
    }

    private IEnumerator SetPlayerStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);

        form.AddField("PlayerSpeed", DefaultSettingsGame.playerSpeed);
        form.AddField("EnemyHealth", DefaultSettingsGame.enemyHealth);
        form.AddField("EnemySpeed", DefaultSettingsGame.enemySpeed);
        form.AddField("EnemyDamage", DefaultSettingsGame.enemyDamage);
        form.AddField("ActivationDistance", DefaultSettingsGame.ActivationDistance);
        form.AddField("FirstRound", DefaultSettingsGame.FirstRound);
        form.AddField("MazeSize", DefaultSettingsGame.MazeSize);


        WWW www = new WWW("http://localhost/sqlconnect/setStats.php", form);
        yield return www;
        string text = www.text.ToString();
        Debug.Log(text);
        if (www.text[0] == '0')
        {
            Debug.Log("the settings in the DB successfully changed");
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }

    public void CallSetStatistics(string Setting, int value)
    {
        StartCoroutine(SetStatistics(Setting, value));
    }
    private IEnumerator SetStatistics(string setting, int value)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("settingName", setting);
        form.AddField("settingValue", value);
        WWW www = new WWW("http://localhost/sqlconnect/setSetting.php", form);
        yield return www;
        string text = www.text.ToString();
        Debug.Log(text);
        if (www.text[0] == '0')
        {
            Debug.Log("the settings in the DB successfully changed");
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }

    public void CallGetStatisticsFromDB()
    {
        StartCoroutine(GetStatisticsFromDB());
    }

    private IEnumerator GetStatisticsFromDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        WWW www = new WWW("http://localhost/sqlconnect/getStatistics.php", form);
        yield return www;
        string text = www.text.ToString();
        Debug.Log(text);
        if (www.text[0] == '0')
        {
            var DBStats = text.Split('|');
            StatisticsHolder.EnemiesKilled = int.Parse(DBStats[1]);
            StatisticsHolder.timePlayed = int.Parse(DBStats[2]);
            StatisticsHolder.gamesPlayed = int.Parse(DBStats[3]);
            StatisticsHolder.totalDeaths = int.Parse(DBStats[4]);
            StatisticsHolder.maxWaveReached = int.Parse(DBStats[5]);
            StatisticsHolder.bulletsShot = int.Parse(DBStats[6]);
            StatisticsHolder.isCalled = true;
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }



}
