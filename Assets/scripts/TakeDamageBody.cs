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
            var sc = transform.parent.gameObject.GetComponent<SC_NPCEnemy>();
            if (sc.isActiveAndEnabled)
            {
                transform.parent.gameObject.GetComponent<SC_NPCEnemy>().TakeDamage(damage);
            }
        }
        if (transform.parent.gameObject.GetComponent<NPCEnemyOffline>() != null)
        {
            var sc = transform.parent.gameObject.GetComponent<NPCEnemyOffline>();
            if (sc.isActiveAndEnabled)
            {
                transform.parent.gameObject.GetComponent<NPCEnemyOffline>().TakeDamage(damage);
            }
        }
       if (transform.parent.gameObject.GetComponent<NPCEnemySurvival>() != null)
        {
            var sc = transform.parent.gameObject.GetComponent<NPCEnemySurvival>();
            if (sc.isActiveAndEnabled)
            {
                transform.parent.gameObject.GetComponent<NPCEnemySurvival>().TakeDamage(damage);
            }
        }
        if (transform.parent.gameObject.GetComponent<NPCEnemyOfflineTutorial>() != null)
        {
            var sc = transform.parent.gameObject.GetComponent<NPCEnemyOfflineTutorial>();
            if (sc.isActiveAndEnabled)
            {
                transform.parent.gameObject.GetComponent<NPCEnemyOfflineTutorial>().TakeDamage(damage);
            }
        }
    }


}
