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


    /////////////////////////////////////
    public bool singleFire = false;
    public float fireRate = 0.1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeToReload = 1.5f;
    public float weaponDamage = 15; //How much damage should this weapon deal

    [HideInInspector]
   // public SC_WeaponManager manager;

    float nextFireTime = 0;
    bool canFire = true;
    int bulletsPerMagazineDefault = 0;





    /////////////////////////////////////////


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
                Vector3 pos = transform.position;
                Vector3 forward = camera.transform.forward;
                pv.RPC("shoot", RpcTarget.All,pos, forward);
                //  shoot();
    }

}

}

[PunRPC]
private void shoot(Vector3 pos, Vector3 forward)
{
/*        Vector3 pos = transform.position;
        Vector3 forward = camera.transform.forward;
        pv.RPC("RpcRaycast", RpcTarget.All, pos, forward);*/


                ///////////////////////////
         //       Debug.Log("shoot method!");
        if (!pv.IsMine)
            return;
     //   Debug.Log(Time.time + " is it bigger than " + nextFireTime + "?");
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
      //      Debug.Log("!!!!!!!!!!");

            //Point fire point at the current center of Camera
            Vector3 firePointPointerPosition = pos + forward * 100;
            //   Vector3 firePointPointerPosition = camera.transform.position + camera.transform.forward * 100;
            RaycastHit hit;
                if (Physics.Raycast(pos, forward, out hit, 100))
                {
                    firePointPointerPosition = hit.point;
                }
                firePoint.LookAt(firePointPointerPosition);
                //Fire
                GameObject bulletObject = PhotonNetwork.Instantiate("Bullet", pos, firePoint.rotation);
                SC_Bullet bullet = bulletObject.GetComponent<SC_Bullet>();
        //    Debug.Log("MAKE DAMAGE: "+ weaponDamage);
                //Set bullet damage according to weapon damage value
                bullet.SetDamage(weaponDamage);

        }



        /////////////////////

    }

    [PunRPC]
    public void RpcRaycast (Vector3 position, Vector3 forward)
    {

        RaycastHit shooting;
        if (Physics.Raycast(position, forward, out shooting, range))
        {
           // Debug.Log(shooting.transform);
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
