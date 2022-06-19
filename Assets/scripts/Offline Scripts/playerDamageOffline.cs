using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class playerDamageOffline : MonoBehaviour, IDamageable
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
          //  Debug.Log("pressed m");
            Die();

        }
    }
    public void TakeDamage(float amount)
    {

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
        StatisticsHolder.totalDeaths++;
        /*      //  Debug.Log("DIE");
                Movement movement = gameObject.GetComponent<Movement>();
                if (movement != null)
                {
                 //   Debug.Log("it's a player");
                //    PhotonNetwork.OfflineMode = false;
                    Destroy(gameObject);
                    SceneManager.LoadScene(2);
                    //PhotonNetwork.LoadLevel(1);
                    //TODO: add here go to the main menu scene or dying in any way
                    return;
                }
                 Destroy(gameObject);*/
        SceneManager.LoadScene(2);
    }
}
