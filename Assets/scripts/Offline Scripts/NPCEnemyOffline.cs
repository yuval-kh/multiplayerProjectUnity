using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCEnemyOffline : MonoBehaviour, IDamageable
{
    public float attackDistance = 3f;
    public float movementSpeed;
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
        movementSpeed = SetGameSettings.Instance.getEnemySpeed();
        npcHP = SetGameSettings.Instance.getEnemyHealth();
        npcDamage = SetGameSettings.Instance.getEnemyDamage();
        activateDistance = SetGameSettings.Instance.getActivationDist();

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

    void Update()
    {
        agent.destination = player.position;
        var navMesh = this.GetComponent<NavMeshAgent>();
        float remainingDist = this.GetRemainingDistance(navMesh);
        if (agent.remainingDistance == 0)
            return;
        if (isActivateAtDist && remainingDist >= activateDistance )
        {
            if (Input.GetKeyDown("l"))
            {
                Debug.Log("remain: " + agent.remainingDistance);
            }
            navMesh.isStopped = true;
            return;
        }
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
                        playerScript.TakeDamage(npcDamage);
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

            StatisticsHolder.EnemiesKilled++;

            Debug.Log("NPC dead");
            var enemyManagerSurvival = transform.GetComponent<UpdateEnemyManagerSurvival>();
            if (enemyManagerSurvival != null)
            {
                Debug.Log("in the if");
                enemyManagerSurvival.DeadUpdate();
            }            

                //Destroy the NPC
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            //Slightly bounce the npc dead prefab up
            Destroy(npcDead, 10);
            Destroy(gameObject);
        }
    }
    
    public void setMovementSpeed(int speed)
    {
        movementSpeed = speed;
        if (agent != null)
        {
            agent.speed = speed;
        }
    }



public float GetRemainingDistance( NavMeshAgent nm)
    {
        float distance = 0;
        Vector3[] corners = nm.path.corners;

        if (corners.Length > 2)
        {
            for (int i = 1; i < corners.Length; i++)
            {
                Vector2 previous = new Vector2(corners[i - 1].x, corners[i - 1].z);
                Vector2 current = new Vector2(corners[i].x, corners[i].z);

                distance += Vector2.Distance(previous, current);
            }
        }
        else
        {
            distance = nm.remainingDistance;
        }

        return distance;
    }
}