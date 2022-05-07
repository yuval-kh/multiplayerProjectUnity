using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBody : MonoBehaviour,IDamageable
{
   // public SC_NPCEnemy myNpc;
    public void TakeDamage(float damage)
    {
        Debug.Log("body take damage");
        transform.parent.gameObject.GetComponent<SC_NPCEnemy>().TakeDamage(damage);
    }


}
