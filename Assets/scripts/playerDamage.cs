using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerDamage : MonoBehaviourPun, IDamageable
{
    public float health = 100;
    private GameObject screen;
    private GameObject sliderObject;
    private Slider slider;



    private void Start()
    {
        screen = GameObject.Find("Canvas");
        sliderObject = screen.transform.Find("HealthSlider").gameObject;
        slider = sliderObject.GetComponent(typeof(Slider)) as Slider;
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.M)) // will work only if im the last player left.
        {
            PhotonView pv = gameObject.GetComponent<PhotonView>();
            if (pv.IsMine)
                Die();
            //  Debug.Log("pressed m");

        }
    }
    public void TakeDamage(float amount)
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        health -= amount;
        slider.value = health;
        Debug.Log("The health is: " + health);
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

        pv.RPC("updateStats", RpcTarget.All);

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            var list = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in list)
            {
                enemy.SetActive( false);
            }
        }
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

    [PunRPC]
    void updateStats()
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (pv == null) return;
        if (pv.IsMine) StatisticsHolder.totalDeaths++;
        else StatisticsHolder.EnemiesKilled++;
    }
}
