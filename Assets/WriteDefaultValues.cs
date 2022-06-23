using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteDefaultValues : MonoBehaviour
{
    public TMP_InputField playerSpeedField;
    public TMP_InputField enemyHealthField;
    public TMP_InputField enemySpeedField;
    public TMP_InputField enemyDamageField;
    public TMP_InputField ActivationDistanceField;
    public TMP_InputField FirstRoundField;
    public TMP_InputField MazeSizeField;
    // Start is called before the first frame update

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
        playerSpeedField.text = DefaultSettingsGame.playerSpeed.ToString();
        enemyHealthField.text = DefaultSettingsGame.enemyHealth.ToString();
        enemySpeedField.text = DefaultSettingsGame.enemySpeed.ToString();
        enemyDamageField.text = DefaultSettingsGame.enemyDamage.ToString();
        ActivationDistanceField.text = DefaultSettingsGame.ActivationDistance.ToString();
        FirstRoundField.text = DefaultSettingsGame.FirstRound.ToString();
        MazeSizeField.text = DefaultSettingsGame.MazeSize.ToString();
    }

    public void changeDefaultValues()
    {
        DefaultSettingsGame.playerSpeed = int.Parse(playerSpeedField.text);
        DefaultSettingsGame.enemyHealth = int.Parse(enemyHealthField.text);
        DefaultSettingsGame.enemySpeed = int.Parse(enemySpeedField.text);
        DefaultSettingsGame.enemyDamage = int.Parse(enemyDamageField.text);
        DefaultSettingsGame.ActivationDistance = int.Parse(ActivationDistanceField.text);
        DefaultSettingsGame.FirstRound = int.Parse(FirstRoundField.text);
        DefaultSettingsGame.MazeSize = int.Parse(MazeSizeField.text);
        if (DBManager.LoggedIn)
        {
            GetDBStats.Instance.CallSetStats();
        }
       // DefaultSettingsGame.MazeSize = int.Parse(MazeSizeField.text);
    }


}
