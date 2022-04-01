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

            if (pv.IsMine)
            {
                pv.RPC("shoot", RpcTarget.All, null);
            }

        }
    }



    [PunRPC]
    private void shoot()
    {
      //  Debug.Log("BOOM");
        RaycastHit shooting;
        if (Physics.Raycast(transform.position, camera.transform.forward, out shooting, range))
        {
            Debug.Log(shooting.transform);
            Shootable target = shooting.transform.GetComponent<Shootable>();
            if (target != null)
            {
                target.takeDamage(gunDamage);
            }
        }
    }
}
