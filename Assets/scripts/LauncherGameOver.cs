using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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


    [SerializeField]
    private InputField messagesLog;




    public static int reloadedNum = 0;


    private Queue<string> messages;
    private const int messageCount = 10;


    bool isMultiplayer; // false - singleplayer true multiplayer
    int mapIndex; // 0 - maze 1 - map1 2 - map2
    bool isSurvival; //true - survival false - free roam
    int buildMapIndexMp;

    private void Awake()
    {
        Instance = this;
    }



/*    private void Update()
    {
        AddMessage("hi");
    }
*/






    private void Start()
    {
        //    messages = new Queue<string>(messageCount);


        buildMapIndexMp = -1;
        if (reloadedNum == 0)
        {
            menuManager.Instance.OpenMenu("NEWtitle");
        }
    //    else menuManager.Instance.OpenMenu("gameover");
        reloadedNum++;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.ConnectUsingSettings();

    //    PhotonNetwork.JoinLobby();////////////////////!!!!!!!!!!!!!!1
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
 //   void Update()
 //   {
        
   // }
    public void GoToMainMenu()
    {
        Debug.Log("Start game method in the script LauncherGameOver :-)");
        // 1 is used as the build index of the game scene, defined in the build settings
        // Use this instead of scene management so that *everyone* in the lobby goes into this scene
        Debug.Log("my name is " + PhotonNetwork.LocalPlayer.NickName);
        menuManager.Instance.OpenMenu("NEWtitle");
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
        menuManager.Instance.OpenMenu("NEWtitle");
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
        // here i can insert 5 instead of 1 to build the multiplayer without enemies mode.
        //6 is map 1
        //7 is map 2
        //8 is multiplayer survival map 1
        //9 is multiplayer survival map 2
        if(buildMapIndexMp == -1)
        {
            Debug.LogError("ERROR WITH THE BUILD INDEX NUMBER:buildMapIndexMp");
        }
        PhotonNetwork.LoadLevel(buildMapIndexMp);
    }




    public void goToFreeRoam()
    {
      //  PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("FreeRoam");
    }

    public void goToSurvival()
    {
        //  PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("Survival");
    }



    public void QuitGame()
    {
        Application.Quit();
    }





    public void setLevelOptions()
    {

        GameObject buttom = EventSystem.current.currentSelectedGameObject;
        var buttomText = buttom.GetComponentInChildren<TextMeshProUGUI>().text;
        if (buttomText.Equals("Multiplayer")) isMultiplayer = true;
        if (buttomText.Equals("SinglePlayer")) isMultiplayer = false;
        if (buttomText.Equals("Survival")) isSurvival = true;
        if (buttomText.Equals("Multiplayer Without Enemies")
            || buttomText.Equals("Free Roam"))
        {
            isSurvival = false;
        }
        if (buttomText.Equals("Random Maze"))
        {
            mapIndex = 0;
            generateLevel();
        }
        if (buttomText.Equals("Map 1"))
        {
            mapIndex = 1;
            generateLevel();
        }

        if (buttomText.Equals("Map 2"))
        {
            mapIndex = 2;
            generateLevel();
        }
    }

    private void generateLevel()
    {
      //  if (isMultiplayer)
      //  {
      //      return;
     //   }
     //   else
      //  {
            if (isSurvival)
            {
                switch (mapIndex)
                {
                    case 0:
                        if(!isMultiplayer) goToSurvival();
                        else
                            {
                        buildMapIndexMp = 1;
                        menuManager.Instance.OpenMenu("create_room");
                    
                            }
                        break;
                    case 1:
                    if (!isMultiplayer) goToSurvivalMap1();
                    else
                    {
                        buildMapIndexMp = 8;
                        menuManager.Instance.OpenMenu("create_room");

                    }
                    break;
                    case 2:
                    if (!isMultiplayer) goToSurvivalMap2();
                    else
                    {
                        buildMapIndexMp = 9;
                        menuManager.Instance.OpenMenu("create_room");

                    }
                    break;

                }
            }
            else
            {
                switch (mapIndex)
                {
                    case 0:
                    if (!isMultiplayer) goToFreeRoam();
                    else
                    {
                        buildMapIndexMp = 5;
                        menuManager.Instance.OpenMenu("create_room");

                    }
                    break;
                    case 1:
                    if (!isMultiplayer) goToFreeRoamMap1();
                    else
                    {
                        buildMapIndexMp = 6;
                        menuManager.Instance.OpenMenu("create_room");

                    }
                    break;
                    case 2:
                    if (!isMultiplayer) goToFreeRoamMap2();
                    else
                    {
                        buildMapIndexMp = 7;
                        menuManager.Instance.OpenMenu("create_room");

                    }
                    break;

                }
            }
      //  }
    }
    public void BackButtomAtMaps()
    {
        if (isMultiplayer) menuManager.Instance.OpenMenu("ChooseModeMp");
        else menuManager.Instance.OpenMenu("ChooseModeSp");
    }
    public void goToSurvivalMap1()
    {
        //  PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("SurvivalMap1");
    }
    public void goToSurvivalMap2()
    {
        //  PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("SurvivalMap2");
    }
    public void goToFreeRoamMap1()
    {
        //  PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("FreeRoam 1");
    }
    public void goToFreeRoamMap2()
    {
        //  PhotonNetwork.OfflineMode = true;
        SceneManager.LoadScene("FreeRoam 2");
    }

}
