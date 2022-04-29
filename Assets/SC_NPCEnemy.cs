using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class SC_NPCEnemy : MonoBehaviour, IDamageable
{
    public float attackDistance = 3f;
    public float movementSpeed = 4f;
    public float npcHP = 100;
    //How much damage will npc deal to the player
    public float npcDamage = 5;
    public float attackRate = 0.5f;
    public Transform firePoint;
    public GameObject npcDeadPrefab;

  //  [HideInInspector]
  //  public Transform playerTransform;
    [HideInInspector]
    NavMeshAgent agent;
    float nextAttackTime = 0;
    private List<Transform> players;

    // Start is called before the first frame update
    void Start()
    {
        //  if (!PhotonNetwork.IsMasterClient)
        //   return;
        players = new List<Transform>();
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
        if (agent == null || players == null || !players.Any())
            return;
        updateList();
        //      if (!PhotonNetwork.IsMasterClient)
        //          return;
        float closestDist = Vector3.Distance(agent.transform.position, players[0].position);
        Transform minPlayer = players[0];
        foreach (var player in players)
        {
            float currDist = Vector3.Distance(agent.transform.position, player.position);
            if (currDist < closestDist)
            {
                closestDist = currDist;
                minPlayer = player;
            }
        }

        Transform childTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        childTransform.position = this.transform.position;
        if (agent.remainingDistance - attackDistance < 0.01f)
        {
            transform.LookAt(new Vector3(minPlayer.transform.position.x, transform.position.y, minPlayer.position.z));
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

                        IDamageable player = hit.transform.GetComponent<IDamageable>();
                        Debug.Log("Damage to player");
                        // player.TakeDamage(npcDamage);
                    }
                }
            }
        }
        //Move towardst he player
        //     agent.destination = playerTransform.position;

        agent.destination = minPlayer.position;
    }


        public void TakeDamage(float damage)//HERE!!!!
    {
        Debug.Log("NPC Took Damage");
        npcHP -= damage;
        if(npcHP <= 0)
        {
            //Destroy the NPC
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            //Slightly bounce the npc dead prefab up
            //npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
            Destroy(npcDead, 10);
            Destroy(gameObject);
        }
    }
    public void updateList()
    {
        foreach (Transform player in players)
        {
            if (player == null)
            {
                players.RemoveAll(player => player == null);
            }
        }
    }
    public void addPlayer(Transform PlayerTransform)
    {
     //   if (!PhotonNetwork.IsMasterClient)
     //       return;
        //this.playerTransform = PlayerTransform;
        players.Add(PlayerTransform);
    }
}