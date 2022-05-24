using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBody : MonoBehaviour,IDamageable
{
   // public SC_NPCEnemy myNpc;
    public void TakeDamage(float damage)
    {
        Debug.Log("body take damage");
        if (transform.parent.gameObject.GetComponent<SC_NPCEnemy>() != null)
        {
            transform.parent.gameObject.GetComponent<SC_NPCEnemy>().TakeDamage(damage);
        }
        if (transform.parent.gameObject.GetComponent<NPCEnemyOffline>() != null)
        {
            transform.parent.gameObject.GetComponent<NPCEnemyOffline>().TakeDamage(damage);
        }
    }


}
