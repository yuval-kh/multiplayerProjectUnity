using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using System.Linq;
using System;


public class SetGameSettings : MonoBehaviour
{
    public static SetGameSettings Instance;

    [SerializeField]
    private TMP_InputField playerSpeedTextBox;
    [SerializeField]
    private TMP_InputField EnemyHealthTextBox;
    [SerializeField]
    private TMP_InputField EnemySpeedTextBox;
    [SerializeField]
    private TMP_InputField EnemyDamageTextBox;
    [SerializeField]
    private TMP_InputField ActivationDistTextBox;
    [SerializeField]
    private TMP_InputField FirstRoundTextBox;
    [SerializeField]
    private TMP_InputField MazeSizeTextBox;


    int playerSpeed;
    int EnemyHealth;
    int EnemySpeed;
    int EnemyDamage;
    int ActivationDist;
    int FirstRound;
    int MazeSize;


    private void Awake()
    {
      
        if(playerSpeedTextBox == null) Destroy(this);
        if (Instance == null)
        {
            Instance = this;
        }
        if(Instance.playerSpeedTextBox == null)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }


    // Start is called before the first frame update
    void Start()
    {
        if (playerSpeedTextBox == null) Destroy(this);
        playerSpeed = EnemyHealth = EnemySpeed = EnemyDamage = ActivationDist = FirstRound = MazeSize =-1;
    }
    public bool setSettingsFromTextBox()
    {
        if (Instance != this) return false ;
        string str = playerSpeedTextBox.text;
        Debug.Log("Textbox: ||" + str+ "||");
        int playerSpeed = getNumber(str);
        int EnemyHealth = getNumber(EnemyHealthTextBox.text);
        int EnemySpeed = getNumber(EnemySpeedTextBox.text);
        int EnemyDamage = getNumber(EnemyDamageTextBox.text);
        int ActivationDist = getNumber(ActivationDistTextBox.text);
        int FirstRound = getNumber(FirstRoundTextBox.text);
        int MazeSize = getNumber(MazeSizeTextBox.text);
        if (playerSpeed == -1 || EnemyHealth == -1 || EnemySpeed == -1 || EnemyDamage == -1 ||
            ActivationDist == -1 || FirstRound == -1 || MazeSize == -1) 
            return false;
        this.playerSpeed = playerSpeed;
        this.EnemyHealth = EnemyHealth;
        this.EnemySpeed = EnemySpeed;
        this.EnemyDamage = EnemyDamage;
        this.ActivationDist = ActivationDist;
        this.FirstRound = FirstRound;
        this.MazeSize = MazeSize;
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
        if (Instance != this) return;
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("playerSpeed: " + playerSpeed);
            Debug.Log("EnemyHealth: " + EnemyHealth);
            Debug.Log("EnemySpeed: " + EnemySpeed);
            Debug.Log("EnemyDamage: " + EnemyDamage);
            Debug.Log("ActivationDist: " + ActivationDist);
            Debug.Log("FirstRound: " + FirstRound);
            Debug.Log("MazeSize: " + MazeSize);
        }
    }

    public int getPlayerSpeed() { return this.playerSpeed; }
    public int getEnemyHealth() { return this.EnemyHealth; }
    public int getEnemySpeed() { return this.EnemySpeed; }
    public int getEnemyDamage() { return this.EnemyDamage; }
    public int getActivationDist() { return this.ActivationDist; }
    public int getFirstRound() { return this.FirstRound; }
    public int getMazeSize() { return this.MazeSize; }

    public void setPlayerSpeed(int playerSpeed) { this.playerSpeed = playerSpeed; }
    public void setEnemyHealth(int EnemyHealth) { this.EnemyHealth = EnemyHealth; }
    public void setEnemySpeed(int EnemySpeed) { this.EnemySpeed = EnemySpeed; }
    public void setEnemyDamage(int EnemyDamage) { this.EnemyDamage = EnemyDamage; }
    public void setActivationDist(int ActivationDist) { this.ActivationDist = ActivationDist; }
    public void setFirstRound(int FirstRound) { this.FirstRound = FirstRound; }

    public void setMazeSize(int value) { this.MazeSize = value; }
}
