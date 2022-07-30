using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCEnemyOfflineTutorial : MonoBehaviour, IDamageable
{
    public float attackDistance = 3f;
    public float movementSpeed ;
    public float npcHP ;
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
        movementSpeed = SetGameSettings.Instance.getEnemySpeed();
        npcHP = SetGameSettings.Instance.getEnemyHealth();
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

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
        var navMesh = this.GetComponent<NavMeshAgent>();
        if (agent.remainingDistance == 0)
            return;

        navMesh.isStopped = false;
        isActivateAtDist = false;

        if (agent == null || player == null)
            return;

        Transform childTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        childTransform.position = this.transform.position;
        if (agent.remainingDistance - attackDistance < 0.01f)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.position.z));

        }
        //Move towards he player
        agent.destination = player.position;
    }


        public void TakeDamage(float damage)
    {
        Debug.Log("NPC Took Damage " + npcHP);
        npcHP -= damage;
        if(npcHP <= 0)
        {
            StatisticsHolder.EnemiesKilled++;
            Debug.Log("NPC dead"); 
            //Destroy the NPC
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            //Slightly bounce the npc dead prefab up
            Destroy(npcDead, 10);
            Destroy(gameObject);
        }
    }

    public void setDeadPrefab(GameObject dead)
    {
        this.npcDeadPrefab = dead;
    }

}