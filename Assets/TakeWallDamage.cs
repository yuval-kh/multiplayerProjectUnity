using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeWallDamage : MonoBehaviourPun,IDamageable
{
    public float health = 50f;
  //  PhotonView pv;

    void Awake()
    {
     //   pv = GetComponent<PhotonView>();
    }

    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
     //   pv.RPC("RPC_Damage", RpcTarget.All, damage);
   //     health -= damage; ;
   //     PhotonView pv = gameObject.GetComponent<PhotonView>();
  //      if (health <= 0f)
   //     {
    //        pv.RPC("Die", RpcTarget.All, null);
    //    }
    }
    public void Die()
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        Destroy(gameObject);
    }
   /* public void RPC_Damage(int damage)
    {
        if (!pv.IsMine)
            return;
        health -= damage; ;
       // PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
