using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{

    [SerializeField]
    private InputField messagesLog;
    public static ChatManager Instance;

    private Queue<string> messages;
    private const int messageCount = 10;
    private PhotonView pv;
    private Text messagePanel;
    private bool isPressed;
    private string messageChat;
    // private List<string> players;




    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        messages = new Queue<string>(messageCount);
        pv = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                AddMessage("The player " + player.NickName + " joined the room");
            }
        }

        messagePanel = GameObject.Find("Canvas").transform.Find("MessagePanel")
            .transform.Find("ChatInput").GetComponent<Text>();
        isPressed = false;
        messageChat = "";

    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                isPressed = true;
                return;
            }

        }
    }

    void OnGUI()
    {
        pv = GetComponent<PhotonView>();
        string myname =  PhotonNetwork.LocalPlayer.NickName;
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.isKey)// && e.keyCode.ToString().Length == 1)
        {
            if(isPressed && e.keyCode == KeyCode.Return)
            {
                isPressed = false;
                AddMessage(myname + ": " + messageChat);
                messageChat = "";
            }
            else if (isPressed )
            {

                if (e.keyCode >= KeyCode.A && e.keyCode <= KeyCode.Z 
                    || e.keyCode >= KeyCode.Keypad0 && e.keyCode <= KeyCode.Keypad9 
                    || e.keyCode == KeyCode.Space
                    || e.keyCode >= KeyCode.Alpha0 && e.keyCode <= KeyCode.Alpha9)
                {

                    char letter;
                    if (e.keyCode == KeyCode.Space)
                        letter = ' ';
                    else
                        letter = e.keyCode.ToString()[e.keyCode.ToString().Length - 1];
                    messageChat +=letter;
                }

            }
        }
    }

    public void AddMessage(string message)
    {
   //     if (!SceneManager.GetActiveScene().name.Equals("Game"))
     //       return;
        messagesLog = GameObject.Find("Canvas").transform.Find("MessagePanel")
              .Find("MessagesInputField").gameObject.GetComponent(typeof(InputField)) as InputField;
        if (messagesLog == null)
            Debug.Log("messagelog is null");
        else
            Debug.Log("messagelog is not null");

        pv.RPC("AddMessage_RPC", RpcTarget.All, message);
    }

    /// <summary>
    /// RPC function to call add message for each client.
    /// </summary>
    /// <param name="message">The message that we want to add.</param>
    [PunRPC]
    void AddMessage_RPC(string message)
    {
        messages.Enqueue(message);
        if (messages.Count > messageCount)
        {
            messages.Dequeue();
        }
        messagesLog.text = "";
        foreach (string m in messages)
        {
            messagesLog.text += m + "\n";
        }
    }
}
