using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using System.Linq;
using System;


public class SetGameSettings : MonoBehaviour
{
    public static SetGameSettings Instance;


    public TMP_InputField playerSpeedTextBox;
    public TMP_InputField EnemyHealthTextBox;
    public TMP_InputField EnemySpeedTextBox;
    public TMP_InputField EnemyDamageTextBox;
    public TMP_InputField ActivationDistTextBox;
    public TMP_InputField FirstRoundTextBox;


    int playerSpeed;
    int EnemyHealth;
    int EnemySpeed;
    int EnemyDamage;
    int ActivationDist;
    int FirstRound;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = EnemyHealth = EnemySpeed = EnemyDamage = ActivationDist = FirstRound = -1;
    }
    public bool setSettings()
    {
        string str = playerSpeedTextBox.text;
        Debug.Log("Textbox: ||" + str+ "||");
        int playerSpeed = getNumber(str);
        int EnemyHealth = getNumber(EnemyHealthTextBox.text);
        int EnemySpeed = getNumber(EnemySpeedTextBox.text);
        int EnemyDamage = getNumber(EnemyDamageTextBox.text);
        int ActivationDist = getNumber(ActivationDistTextBox.text);
        int FirstRound = getNumber(FirstRoundTextBox.text);
        if (playerSpeed == -1 || EnemyHealth == -1 || EnemySpeed == -1 || EnemyDamage == -1 ||
            ActivationDist == -1 || FirstRound == -1) 
            return false;
        this.playerSpeed = playerSpeed;
        this.EnemyHealth = EnemyHealth;
        this.EnemySpeed = EnemySpeed;
        this.EnemyDamage = EnemyDamage;
        this.ActivationDist = ActivationDist;
        this.FirstRound = FirstRound;
        
        return true;
    }

    private int getNumber(string numStr)
    {
        int n = -1;
        string TrimmedStr = String.Concat(numStr.Where(c => !Char.IsWhiteSpace(c)));
        Debug.Log("numStr: " + TrimmedStr);
        bool isNumeric = Int32.TryParse(TrimmedStr, out n);
        Debug.Log(isNumeric);
        Debug.Log("TryParse: " + n);
        if (!isNumeric) return -1;  
        else return n;
    }
    public void run(TextMeshProUGUI a, TextMeshProUGUI b, TextMeshProUGUI c, TextMeshProUGUI d)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("playerSpeed: " + playerSpeed);
            Debug.Log("EnemyHealth: " + EnemyHealth);
            Debug.Log("EnemySpeed: " + EnemySpeed);
            Debug.Log("EnemyDamage: " + EnemyDamage);
            Debug.Log("ActivationDist: " + ActivationDist);
            Debug.Log("FirstRound: " + FirstRound);
        }
    }
}
