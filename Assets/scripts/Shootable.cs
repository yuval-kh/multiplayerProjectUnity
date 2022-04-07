using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shootable : MonoBehaviourPun, IDamageable
{
    public float health = 50f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Debug.Log("pressed m");
            Die();

        }
    }
    public void TakeDamage(float amount)
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        Debug.Log("player TakeDamage");
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("DIE");
        Movement movement = gameObject.GetComponent<Movement>();
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (movement != null)
        {
            if (pv == null)
                return;
            Debug.Log("it's a player");
       ////     if (pv.IsMine)
      ////      {
                Debug.Log("its me - i was kiled");
                PhotonNetwork.Destroy(gameObject);
                Debug.Log("!!!!!!!!!!!!!!!d");
                PhotonNetwork.LeaveRoom();
                Debug.Log("!!!!!!!!!!wwwwwww!!!!!d");
                return;
/*                while (PhotonNetwork.InRoom)
                 //   yield return null;
                SceneManager.LoadScene(2);*/
           //     PhotonNetwork.LoadLevel(2);
        ////    }
        ////    else
        ////    {
        ////        Debug.Log("i killed a player B-)");
        ////    }
        }
        else
            Debug.Log("it's not a player");
         Destroy(gameObject);

        Debug.Log("the type is: " + gameObject.GetType());
        Debug.Log("kill");
    }
}
