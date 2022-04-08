using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeWallDamage : MonoBehaviourPun,IDamageable
{
    public float health = 50f;


    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }
    public void Die()
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        Destroy(gameObject);
    }

}