using System.Collections;
using UnityEngine;
using Photon.Pun;

public class SC_Bullet : MonoBehaviour
{
    public float bulletSpeed = 345;
    public float hitForce = 50f;
    public float destroyAfter = 3.5f;

    float currentTime = 0;
    Vector3 newPos;
    Vector3 oldPos;
    bool hasHit = false;

    float damagePoints;

    // Start is called before the first frame update
    IEnumerator Start()
    {
   //     Debug.Log("bullet time");
        newPos = transform.position;
        oldPos = newPos;
     //   Debug.Log("POS AT BULLET is: " + newPos);

        while (currentTime < destroyAfter && !hasHit)
        {
            Vector3 velocity = transform.forward * bulletSpeed;
            newPos += velocity * Time.deltaTime; 
            Vector3 direction = newPos - oldPos;
            float distance = direction.magnitude;
            RaycastHit hit;

            // Check if we hit anything on the way
            if (Physics.Raycast(oldPos, direction, out hit, distance))
            {
          //      if (hit.rigidbody != null)
          //      {
          //          hit.rigidbody.AddForce(direction * hitForce);

                    IDamageable npc = hit.transform.GetComponent<IDamageable>();
                    if (npc != null)
                    {
                    //Apply damage to NPC
                //    Debug.Log(damagePoints);
                    npc.TakeDamage(damagePoints);
                    }
               // }

                newPos = hit.point; //Adjust new position
                StartCoroutine(DestroyBullet());
            }

            currentTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();

            transform.position = newPos;
            oldPos = newPos;
        }

        if (!hasHit)
        {
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet()
    {
        hasHit = true;
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.Destroy(gameObject);
    }

    //Set how much damage this bullet will deal
    public void SetDamage(float points)
    {
        damagePoints = points;
    }
}