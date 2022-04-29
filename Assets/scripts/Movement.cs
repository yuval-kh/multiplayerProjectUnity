using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public GameObject _camera;




    private PhotonView pv;

    void Start()//HERE!!!
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            Destroy(GetComponentInChildren<CharacterController>());
            Destroy(GetComponentInChildren<Rigidbody>());
        }
        EnemyManager.Instance.addPlayer(this.transform);/////////////////////AAAAAAAAAAAAA//
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine)
        {
            _camera.SetActive(false);
            return;
        }
        _camera.SetActive(true);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");






        //Vector3 move = transform.right * x + transform.forward * z;



        Vector3 move = transform.right * x + transform.up * -10 + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);



       
    }
}
