using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class LauncherGameOver : MonoBehaviourPunCallbacks
{
    public static LauncherGameOver Instance;


   // [SerializeField] TMP_InputField playerNameInputField;
  //  [SerializeField] TMP_Text titleWelcomeText;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    public static int reloadedNum = 0;



    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        if (reloadedNum == 0)
        {
            menuManager.Instance.OpenMenu("title");
        }
        reloadedNum++;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnectedToMaster()
    { 
        Debug.Log("Connected to masterAA!");
        PhotonNetwork.JoinLobby();
        // Automatically load scene for all clients when the host loads a scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby gameover");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToMainMenu()
    {
        Debug.Log("Start game method in the script LauncherGameOver :-)");
        // 1 is used as the build index of the game scene, defined in the build settings
        // Use this instead of scene management so that *everyone* in the lobby goes into this scene
        Debug.Log("my name is " + PhotonNetwork.LocalPlayer.NickName);
        menuManager.Instance.OpenMenu("title");
    }





    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text);
            menuManager.Instance.OpenMenu("loading");
            roomNameInputField.text = "";
        }
        else
        {
            Debug.Log("No room name entered");
            // TODO: Display an error to the user
        }
    }

    public override void OnJoinedRoom()
    {
        // Called whenever you create or join a room

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        if (PhotonNetwork.IsMasterClient)
        {

            int tempSeed = UnityEngine.Random.Range(0, 100000);

            hash["seed"] = tempSeed;

            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }

        menuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform trans in playerListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < players.Count(); i++)
        {

            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        // Only enable the start button if the player is the host of the room
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        menuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        menuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        menuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        if (roomList == null)
            Debug.Log("roomlist is null");
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                // Don't instantiate stale rooms
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem_GameOver>().SetUp(roomList[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // errorText.text = "Room Creation Failed: " + message;
        menuManager.Instance.OpenMenu("error");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        // 1 is used as the build index of the game scene, defined in the build settings
        // Use this instead of scene management so that *everyone* in the lobby goes into this scene
        PhotonNetwork.LoadLevel(1);
    }




    public void QuitGame()
    {
        Application.Quit();
    }
}
