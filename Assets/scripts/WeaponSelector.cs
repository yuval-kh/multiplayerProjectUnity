using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSelector : MonoBehaviourPun
{
    public int selectedWeapon = 0;
    //  public Canvas can = null;


    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        // pv = this.transform.parent.GetComponent<PhotonView>();
        initializeWeapons();
    }



    // Update is called once per frame
    void Update()
    {
        /*        if (!pv.IsMine)
                {
                    return;
                }
                    int previousWeapon = selectedWeapon;
                if(Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (selectedWeapon >= transform.childCount - 1)
                        selectedWeapon = 0;
                    else
                        selectedWeapon++;
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (selectedWeapon <=0)
                        selectedWeapon = transform.childCount - 1;
                    else
                        selectedWeapon--;
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    selectedWeapon = 0;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
                {
                    selectedWeapon = 1;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
                {
                    selectedWeapon = 2;
                }
                if (Input.GetKeyDown(KeyCode.H) && transform.childCount >= 3)
                {
                    selectedWeapon = -1;
                }
                if (previousWeapon != selectedWeapon)
                {
                    initializeWeapons();
                }*/
      //  if (pv.IsMine)
      //  {
            updateWeapon();
        //    pv.RPC("updateWeapon", RpcTarget.All, null);
     //   }

    }

 //   [PunRPC]
    private void updateWeapon()
    {
        int previousWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.H) && transform.childCount >= 3)
        {
            selectedWeapon = -1;
        }

        if (previousWeapon != selectedWeapon && pv.IsMine)
        {
            pv.RPC("otherPressed", RpcTarget.All, pv.ViewID, selectedWeapon);
           // initializeWeapons();
        }
    }

 //   [PunRPC]
    private void initializeWeapons()
    {

        if (!pv.IsMine)
            Debug.Log("i think the other player pressed it");
        if (selectedWeapon >= 0)
        {
            //can.gameObject.SetActive(true);
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
        else if (selectedWeapon == -1)
        {
            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(false);
              //  if(can != null)
               //     can.gameObject.SetActive(false);

            }
        }

    }

    [PunRPC]
    private void otherPressed(int id, int weapon)
    {

      /*  if (pv.ViewID == id)
            Debug.Log("i think that player" + id + "pressed " + weapona);*/
        selectedWeapon = weapon;
        initializeWeapons();

    }
}
