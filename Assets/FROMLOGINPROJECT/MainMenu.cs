using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    public Button registerButton; 
    public Button loginButton;
    public Button deleteButton;
    public Button playButton;
    public TextMeshProUGUI playerDisplay;

  //  public Text playerDisplay;


    public void Start()
    {

        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
        }

        UpdateButtoms();

    }

    void OnEnable()
    {
        UpdateButtoms();
    }

    public void UpdateButtoms()
    {
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        deleteButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;
        if (!DBManager.LoggedIn)
        {
            playerDisplay.text = "NOT LOGGED IN";
            playerDisplay.color = new Color(255, 0, 0, 255);
        }
        else
        {
            playerDisplay.text = DBManager.username;
            playerDisplay.color = new Color(0, 255, 0, 255);
        }
    }
    public void Update()
    {
        UpdateButtoms();
    }

    public void GoToRegister()
    {
        menuManager.Instance.OpenMenu("register");
    }

    public void GoTologin()
    {
        menuManager.Instance.OpenMenu("toLogIn");
    }
    public void GoToGame()
    {
        PhotonNetwork.NickName = DBManager.username;
      //  DefaultSettingsGame.initiateValues();
        PhotonNetwork.LoadLevel(2);
        Destroy(this);
    }
}
