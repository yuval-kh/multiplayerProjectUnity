using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyManagerSurvival : MonoBehaviour
{
  //  public EnemyManagerSurvival enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeadUpdate()
    {
        /*   if(enemyManager != null)
           {
               enemyManager.EnemyEliminated();
           }*/
        EnemyManagerSurvival.Instance.EnemyEliminated();
    }
}
