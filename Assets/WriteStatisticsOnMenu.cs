using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteStatisticsOnMenu : MonoBehaviour
{
    public TextMeshProUGUI EnemiesKilledTextBox;
    public TextMeshProUGUI timePlayedTextBox;
    public TextMeshProUGUI gamesPlayedTextBox;
    public TextMeshProUGUI totalDeathsTextBox;
    public TextMeshProUGUI maxWaveTextBox;
    public TextMeshProUGUI bulletsShotTextBox;
    public TextMeshProUGUI kdRatioTextBox;

    void Start()
    {
        setValues();
    }
    void OnEnable()
    {
        setValues();
    }


    private void setValues()
    {
        EnemiesKilledTextBox.text = StatisticsHolder.EnemiesKilled.ToString();
        timePlayedTextBox.text = StatisticsHolder.timePlayed.ToString();
        gamesPlayedTextBox.text = StatisticsHolder.gamesPlayed.ToString();
        totalDeathsTextBox.text = StatisticsHolder.totalDeaths.ToString();
        maxWaveTextBox.text = StatisticsHolder.maxWaveReached.ToString();
        bulletsShotTextBox.text = StatisticsHolder.bulletsShot.ToString();
        kdRatioTextBox.text = StatisticsHolder.kdRatio.ToString();
    }
}
