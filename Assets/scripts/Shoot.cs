using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPun
{
    public Camera camera = null;
    private PhotonView pv;
    public GameObject player;
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
                 pv.RPC("makeBulletToAll", RpcTarget.All,pos, firePoint.rotation, weaponDamage);
/*                GameObject bulletObject = PhotonNetwork.Instantiate("Bullet", pos, firePoint.rotation);
                SC_Bullet bullet = bulletObject.GetComponent<SC_Bullet>();
            Debug.Log("MAKE DAMAGE: "+ weaponDamage);
                //Set bullet damage according to weapon damage value
                bullet.SetDamage(weaponDamage);*/

        }



    }


    [PunRPC]
    private void makeBulletToAll(Vector3 pos, Quaternion rotation,float damage)
    {
  //      if (!pv.IsMine)
   //         return;
        GameObject bulletObject = Instantiate(bulletPrefab, pos, rotation);
        SC_Bullet bullet = bulletObject.GetComponent<SC_Bullet>();
        bullet.SetDamage(damage);


    }
}
