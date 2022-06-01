using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class addToEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            return;
        var pv = gameObject.GetComponent<PhotonView>();
        

        if (pv.IsMine)
        {
            EnemyManagerSurvivalOnline.Instance.addPlayer(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
