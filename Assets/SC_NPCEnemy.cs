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

    [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    NavMeshAgent agent;
    float nextAttackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
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
        Transform childTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        childTransform.position = this.transform.position;
        if (agent.remainingDistance - attackDistance < 0.01f)
        {
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
            if (Time.time > nextAttackTime)
            {
                
                nextAttackTime = Time.time + attackRate;

                //Attack
                RaycastHit hit;
                if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
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
        agent.destination = playerTransform.position;
    //    if (playerTransform == null)
   //         Debug.Log("its null");
   //     else
   //         Debug.Log("Its not null");
        //Always look at player
        //transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
    }

    public void TakeDamage(float damage)//HERE!!!!
    {
        Debug.Log("NPC Took Damage");
     //   npcHP -= damage;
     //   if(npcHP <= 0)
      //  {
      //      //Destroy the NPC
     //       GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            //Slightly bounce the npc dead prefab up
    //        npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
    //        Destroy(npcDead, 10);
     //       Destroy(gameObject);
     //   }
    }
    public void addPlayer(Transform PlayerTransform)
    {
        this.playerTransform = PlayerTransform;
    }
}