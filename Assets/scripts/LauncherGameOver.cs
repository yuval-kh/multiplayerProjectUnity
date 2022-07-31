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


        buildMapIndexMp = -1;
        if (reloadedNum == 0)
        {
            Debug.Log("MAIN");
            UIFunctions scriptUi = this.GetComponent<UIFunctions>();
            scriptUi.openmainMenu();

        }
        reloadedNum++;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            UIFunctions scriptUi = this.GetComponent<UIFunctions>();
            scriptUi.openmainMenu();
        }

    }

    public override void OnConnectedToMaster()
    { 
        Debug.Log("Connected to masterAA!");
        PhotonNetwork.JoinLobby();
        // Automatically load scene for all clients when the host loads a scene
        PhotonNetwork.AutomaticallySyncScene = true;
        UIFunctions scriptUi = this.GetComponent<UIFunctions>();
        scriptUi.openmainMenu();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby gameover");
    }

    public void GoToMainMenu()
    {
        Debug.Log("Start game method in the script LauncherGameOver :-)");
        // 1 is used as the build index of the game scene, defined in the build settings
        // Use this instead of scene management so that *everyone* in the lobby goes into this scene
        Debug.Log("my name is " + PhotonNetwork.LocalPlayer.NickName);
        UIFunctions scriptUi = this.GetComponent<UIFunctions>();
        scriptUi.openmainMenu();
    }





    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text);
            UIFunctions scriptUi = this.GetComponent<UIFunctions>();
            scriptUi.openLoadingScreen();
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
            hash["playerSpeed"] = SetGameSettings.Instance.getPlayerSpeed();
            hash["EnemyHealth"] = SetGameSettings.Instance.getEnemyHealth();
            hash["EnemySpeed"] = SetGameSettings.Instance.getEnemySpeed();
            hash["EnemyDamage"] = SetGameSettings.Instance.getEnemyDamage();
            hash["ActivationDist"] = SetGameSettings.Instance.getActivationDist();
            hash["FirstRound"] = SetGameSettings.Instance.getFirstRound();
            hash["MazeSize"] = SetGameSettings.Instance.getMazeSize();

            ///
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }
        else
        {
            var properties = PhotonNetwork.CurrentRoom.CustomProperties;
            SetGameSettings.Instance.setPlayerSpeed((int)properties["playerSpeed"]);
            SetGameSettings.Instance.setEnemyHealth((int)properties["EnemyHealth"]);
            SetGameSettings.Instance.setEnemySpeed((int)properties["EnemySpeed"]);
            SetGameSettings.Instance.setEnemyDamage((int)properties["EnemyDamage"]);
            SetGameSettings.Instance.setActivationDist((int)properties["ActivationDist"]);
            SetGameSettings.Instance.setFirstRound((int)properties["FirstRound"]);
            SetGameSettings.Instance.setFirstRound((int)properties["MazeSize"]);
        }

        menuManager.Instance.OpenMenu("room");
        UIFunctions scriptUi = this.GetComponent<UIFunctions>();
        scriptUi.openroomsMenu();


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
        UIFunctions scriptUi = this.GetComponent<UIFunctions>();
        scriptUi.openLoadingScreen();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        UIFunctions scriptUi = this.GetComponent<UIFunctions>();
        scriptUi.openLoadingScreen();
    }

    public override void OnLeftRoom()
    {
        menuManager.Instance.OpenMenu("NEWtitle");
        UIFunctions scriptUi = this.GetComponent<UIFunctions>();
        scriptUi.openmainMenu();
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
        SceneManager.LoadScene("FreeRoam");
    }

    public void goToSurvival()
    {
        SceneManager.LoadScene("Survival");
    }



    public void QuitGame()
    {
        Application.Quit();
    }



    public void setLevelOptions(bool isMp, bool Survival, int map)
    {
        isMultiplayer = isMp;
        isSurvival = Survival;
        mapIndex = map;
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
            menuManager.Instance.OpenMenu("room_settings");
        }
        if (buttomText.Equals("Map 1"))
        {
            mapIndex = 1;
            menuManager.Instance.OpenMenu("room_settings");
        }

        if (buttomText.Equals("Map 2"))
        {
            mapIndex = 2;
            menuManager.Instance.OpenMenu("room_settings");
        }
    }

    public void generateLevel()
    {
        bool setSettingResults = SetGameSettings.Instance.setSettingsFromTextBox();
        if (setSettingResults == false)
        {
            return;
        }

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
        SceneManager.LoadScene("SurvivalMap1");
    }
    public void goToSurvivalMap2()
    {
        SceneManager.LoadScene("SurvivalMap2");
    }
    public void goToFreeRoamMap1()
    {
        SceneManager.LoadScene("FreeRoam 1");
    }
    public void goToFreeRoamMap2()
    {
        SceneManager.LoadScene("FreeRoam 2");
    }

}
