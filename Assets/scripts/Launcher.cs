using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField playerNameInputField;
    //[SerializeField] TMP_Text titleWelcomeText;




 //  public GameObject roomManager;



    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    //    Instantiate(roomManager, new Vector3(0,0,0), Quaternion.identity);
        if (!PhotonNetwork.IsConnected)
        {
              Debug.Log("Connecting to master...");
            
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("already connected");
            menuManager.Instance.OpenMenu("gameover");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master!");
        PhotonNetwork.JoinLobby();
        // Automatically load scene for all clients when the host loads a scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.NickName == "")
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString(); // Set a default nickname, just as a backup
            menuManager.Instance.OpenMenu("name");
        }
        else
        {
            if (!menuManager.Instance.MenuIsActive("gameover"))
            {

                menuManager.Instance.OpenMenu("title");
            }
        }
        Debug.Log("Joined lobby regular");
    }

    public void SetName()
    {
        string name = playerNameInputField.text;
        if (!string.IsNullOrEmpty(name))
        {
            PhotonNetwork.NickName = name;
      //      titleWelcomeText.text = $"Welcome, {name}!";
            PhotonNetwork.LoadLevel(2);
            Destroy(this);
            return;
            Debug.Log("i hope i wont see this");
            menuManager.Instance.OpenMenu("title");
            playerNameInputField.text = "";
        }
        else
        {
            Debug.Log("No player name entered");
            // TODO: Display an error to the user
        }
    }

}
