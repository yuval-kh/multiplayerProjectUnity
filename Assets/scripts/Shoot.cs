using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPun
{
    public Camera camera = null;
    public float range = 100f;
    public float gunDamage = 10f;
    //public float fireRate = 100f; // how fast the gun can shoot
    //public float nextFire = 0f;
    private PhotonView pv;
    public GameObject player;



    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv == null)
            Debug.Log("pv is null");
        else
            Debug.Log("pv is not null");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//was GetButton && Time.time >= nextFire)// if the player pressed to shoot and i can fire(depends on the gun fire rate)
        {
            if (pv == null)
                return;
            if (pv.IsMine)
            {
                shoot();
            }

        }

    }


    private void shoot()
    {
        Vector3 pos = transform.position;
        Vector3 forward = camera.transform.forward;
        pv.RPC("RpcRaycast", RpcTarget.All, pos, forward);

    }

    [PunRPC]
    public void RpcRaycast (Vector3 position, Vector3 forward)
    {

        RaycastHit shooting;
        if (Physics.Raycast(position, forward, out shooting, range))
        {
            Debug.Log(shooting.transform);
            pv = GetComponent<PhotonView>();
            if (pv == null)
                return;
            IDamageable target = shooting.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(gunDamage);
            }
        }
        
    }

    public void RpcAllDamage(IDamageable target)
    {
        if (!pv.IsMine)
            return;
        target.TakeDamage(gunDamage);
    }
}
