using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOffline : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public GameObject _camera;





    void Start()//HERE!!!
    {

        //EnemyManager.Instance.addPlayer(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        _camera.SetActive(true);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * -10 + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);



       
    }
}
