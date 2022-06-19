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
    public Button playButton;
    public TextMeshProUGUI playerDisplay;

  //  public Text playerDisplay;


    public void Start()
    {

        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
        }

        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;

    }

    void OnEnable()
    {
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;
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
