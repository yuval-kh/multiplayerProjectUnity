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
        SceneManager.LoadScene(2);
    }
}
