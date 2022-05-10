using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.AI;

public class TakeWallDamageOffline : MonoBehaviour,IDamageable
{
    public float health = 50f;


    public NavMeshSurface surface;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            gameObject.SetActive(false);
            onDisable();
        }
    }
    public void Die()
    {

        Destroy(gameObject);

    }

    public void AddSurface(NavMeshSurface surface)
    {
        this.surface = surface;
    }


    void onDisable()
    {
        if (surface == null)
            return;
        // the objects with this tag always will be childern of the main object
        GameObject[] players = GameObject.FindGameObjectsWithTag("IgnoreNavBuild");
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                player.transform.parent.gameObject.SetActive(false);
            }
        }
        surface.BuildNavMesh();
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                player.transform.parent.gameObject.SetActive(true);
            }
        }
    }
}
