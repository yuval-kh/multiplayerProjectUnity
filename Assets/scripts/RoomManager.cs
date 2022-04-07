using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    /*    private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }*/
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
            //   PhotonNetwork.LeaveRoom();
             DontDestroyOnLoad(this);
            //     SceneManager.LoadScene(0);
           // Destroy(this);
            PhotonNetwork.LoadLevel(2);

        }
    }





}
