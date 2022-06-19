using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class deleteEnemies : MonoBehaviour
{
    private PhotonView pv;



    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }



    public void deleteNpcNum(int num)
    {
        this.pv.RPC("deleteEnemytoAll", RpcTarget.All, num);
    }

    [PunRPC]
    public void deleteEnemytoAll(int num)
    {
        EnemyManagerSurvival.Instance.EnemyEliminated();



        Debug.Log(" i have to delete the " + num + "enemy");
        var list = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in list)
        {
            SC_NPCEnemy sc = enemy.GetComponent<SC_NPCEnemy>();
            if ( sc != null)
            {
                int Enemynum = sc.getMyCounter();
                if (Enemynum == num)
                    sc.TakeDamage(200);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
