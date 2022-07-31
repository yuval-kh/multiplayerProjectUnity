using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootOffline : MonoBehaviour
{
    public Camera camera = null;
    public GameObject player;
    public bool singleFire = false;
    public float fireRate = 0.1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeToReload = 1.5f;
    public int bulletsPerMagazine = 30;
    public float weaponDamage = 15; //How much damage should this weapon deal

    [HideInInspector]
   // public SC_WeaponManager manager;

    float nextFireTime = 0;
    bool canFire = true;
    int bulletsPerMagazineDefault = 30;



    private GameObject screen;
    private GameObject textObject;
    private Text text;



    void Start()
    {

        screen = GameObject.Find("Canvas");
        textObject = screen.transform.Find("BulletsCounter").Find("bulletsText").gameObject;
        text = textObject.GetComponent(typeof(Text)) as Text;

        text.text = bulletsPerMagazineDefault.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
            text.text = bulletsPerMagazine.ToString();
        if (Input.GetButtonDown("Fire1") && singleFire)
        {
            Fire();
        }
        if (Input.GetButton("Fire1") && !singleFire)
        {
            Fire();
        }

    }


private void Fire()
    {
            Debug.Log("SHOOTING");
            Vector3 pos = transform.position;
            Vector3 forward = transform.forward;
              shoot( pos, forward);

    }

private void shoot(Vector3 pos, Vector3 forward)
{

        if (!canFire)
            return;
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;



            if (bulletsPerMagazine > 0)
            {

                //Point fire point at the current center of Camera
                Vector3 firePointPointerPosition = pos + forward * 100;
                RaycastHit hit;
                if (Physics.Raycast(pos, forward, out hit, 100))
                {
                    firePointPointerPosition = hit.point;
                }


                bulletsPerMagazine--;
                Debug.Log(bulletsPerMagazine);

                //Fire
                GameObject bulletObject = Instantiate(bulletPrefab, pos, firePoint.rotation);
                SC_Bullet bullet = bulletObject.GetComponent<SC_Bullet>();
                bullet.SetDamage(weaponDamage);

            }
            else
            {
                StartCoroutine(Reload());
            }

        }

    }

    IEnumerator Reload()
    {        
        canFire = false;
        Debug.Log("Reload");
        yield return new WaitForSeconds(timeToReload);
        bulletsPerMagazine = bulletsPerMagazineDefault;
        canFire = true;
    }



    public void ActivateWeapon(bool activate)
    {
        StopAllCoroutines();
        canFire = true;
        gameObject.SetActive(activate);
    }
}
