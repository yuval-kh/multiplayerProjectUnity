using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


using UnityEngine.AI;

public class TakeWallDamage : MonoBehaviourPun,IDamageable
{
    public float health = 50f;


    public NavMeshSurface surface;

    public void TakeDamage(float damage)
    {
   //     Debug.Log("damage " + damage);
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void Die()
    {
        PhotonView pv = gameObject.GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        Destroy(gameObject);

    }

    public void AddSurface(NavMeshSurface surface)
    {
        this.surface = surface;
    }

    void OnDestroy()
    {
        GameObject []players = GameObject.FindGameObjectsWithTag("IgnoreNavBuild");// the objects with this tag always will be childern of the main object
        foreach (GameObject player in players)
        {
            player.transform.parent.gameObject.SetActive(false);
        }
        surface.BuildNavMesh();
        foreach (GameObject player in players)
        {
            player.transform.parent.gameObject.SetActive(true);
        }
    }

}
