using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{
    public float sensetivity = 100f;
    public Transform  Player;
    float rotation = 0f;

   
    public GameObject _camera;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
            float mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;
            rotation -= mouseY;
            rotation = Mathf.Clamp(rotation, -90f, 90f);//disables the mouse to move more then 90 degrees
            transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
            Player.Rotate(Vector3.up * mouseX);
   //     }
    }
}
