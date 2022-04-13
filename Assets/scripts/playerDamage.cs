using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDamage : MonoBehaviourPun, IDamageable
{
    public float health = 50f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.M)) // will work only if im the last player left.
        {
          //  Debug.Log("pressed m");
            Die();

        }
    }
    public void TakeDamage(float amount)
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
      //  Debug.Log("DIE");
        Movement movement = gameObject.GetComponent<Movement>();
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (movement != null)
        {
            if (pv == null)
                return;
         //   Debug.Log("it's a player");
            PhotonNetwork.Destroy(gameObject);
            PhotonNetwork.LeaveRoom();
            return;
        }
         Destroy(gameObject);
    }
}
