﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCEnemyOffline : MonoBehaviour, IDamageable
{
    public float attackDistance = 3f;
    public float movementSpeed = 4f;
    public float npcHP = 100;
    //How much damage will npc deal to the player
    public float npcDamage = 5;
    public float attackRate = 0.5f;
    public Transform firePoint;
    public GameObject npcDeadPrefab;
    [HideInInspector]
    NavMeshAgent agent;
    float nextAttackTime = 0;
    private Transform player;




    public bool isActivateAtDist;
    public float activateDistance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;

        //Set Rigidbody to Kinematic to prevent hit register bug
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        Transform childTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        childTransform.position = this.transform.position;

   //     isActivateAtDist = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
        /////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!?////////////////////////
        //isActivateAtDist = true;
        var navMesh = this.GetComponent<NavMeshAgent>();
        //Debug.Log("The activateDistance is: " + activateDistance);
        //Debug.Log("The remaining distance is: " + agent.remainingDistance);
        //Debug.Log("is activate at dist: " + isActivateAtDist);
        if (agent.remainingDistance == 0)
            return;
        if (isActivateAtDist && agent.remainingDistance >= activateDistance )
        {

            navMesh.isStopped = true;
     //       Debug.Log("dont move");
            return;
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        navMesh.isStopped = false;
        isActivateAtDist = false;

        if (agent == null || player == null)
            return;

        Transform childTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        childTransform.position = this.transform.position;
        if (agent.remainingDistance - attackDistance < 0.01f)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.position.z));
            if (Time.time > nextAttackTime)
            {

                nextAttackTime = Time.time + attackRate;

                //Attack
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                        IDamageable playerScript = hit.transform.GetComponent<IDamageable>();
                        Debug.Log("Damage to player");
                        // playerScript.TakeDamage(npcDamage);
                    }
                }
            }
        }
        //Move towards he player
        agent.destination = player.position;
    }


        public void TakeDamage(float damage)//HERE!!!!
    {
        Debug.Log("NPC Took Damage " + npcHP);
        npcHP -= damage;
        if(npcHP <= 0)
        {
            Debug.Log("NPC dead");
            //HERE THE LAST CHANGES !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!111111111111111111111111
            var enemyManagerSurvival = transform.GetComponent<UpdateEnemyManagerSurvival>();
            if (enemyManagerSurvival != null)
            {
                Debug.Log("in the if");
                enemyManagerSurvival.DeadUpdate();
            }

            //HERE THE LAST CHANGES !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!111111111111111111111111
            
                
                
                
                
                //Destroy the NPC
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            //Slightly bounce the npc dead prefab up
            Destroy(npcDead, 10);
            Destroy(gameObject);
        }
    }

}