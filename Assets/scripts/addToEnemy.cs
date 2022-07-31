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

        if (!isRightLevel()) return;
        var pv = gameObject.GetComponent<PhotonView>();
        

        if (pv.IsMine)
        {
            EnemyManagerSurvivalOnline.Instance.addPlayer(this.transform);
        }
    }

    private bool isRightLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 8
            || SceneManager.GetActiveScene().buildIndex == 9)
            return true;
        else return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
