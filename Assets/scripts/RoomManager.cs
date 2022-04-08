using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {

            // This is the game scene
            PhotonNetwork.Instantiate( "PlayerManager", Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("oneleftRoomFunction");
        base.OnLeftRoom();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
             DontDestroyOnLoad(this);
            PhotonNetwork.LoadLevel(2);

        }
    }





}
