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
        if (scene.buildIndex == 1 || scene.buildIndex == 5 || scene.buildIndex == 6 || scene.buildIndex == 7
            || scene.buildIndex == 8 || scene.buildIndex == 9)
        {

            // This is the game scene
            PhotonNetwork.Instantiate( "PlayerManager", Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("oneleftRoomFunction");
        base.OnLeftRoom();
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 5 
            || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7
             || SceneManager.GetActiveScene().buildIndex == 8 || SceneManager.GetActiveScene().buildIndex == 9)
        {
         //   ChatManager ch= GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
         //   ch.AddMessage(photonView.Owner.NickName + "has left the room");
             DontDestroyOnLoad(this);
            PhotonNetwork.LoadLevel(2);

        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player other)
    {

        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().buildIndex == 1)
        {
            ChatManager ch = GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
            ch.AddMessage("Player " + other.NickName + " Left Game.");
        }
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().buildIndex == 5)
        {
            ChatManager ch = GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
            ch.AddMessage("Player " + other.NickName + " Left Game.");
        }
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().buildIndex == 6)
        {
            ChatManager ch = GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
            ch.AddMessage("Player " + other.NickName + " Left Game.");
        }
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().buildIndex == 7)
        {
            ChatManager ch = GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
            ch.AddMessage("Player " + other.NickName + " Left Game.");
        }
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().buildIndex == 8)
        {
            ChatManager ch = GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
            ch.AddMessage("Player " + other.NickName + " Left Game.");
        }
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().buildIndex == 9)
        {
            ChatManager ch = GameObject.Find("Canvas").transform.Find("MessagePanel").GetComponent<ChatManager>();
            ch.AddMessage("Player " + other.NickName + " Left Game.");
        }
    }





}
