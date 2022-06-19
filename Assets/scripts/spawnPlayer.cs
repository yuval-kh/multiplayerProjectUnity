using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var locationGenerator = GameObject.Find("LocationGenerator");
        var sc = locationGenerator.GetComponent<LocationGenerator>();
        transform.position = sc.generateLocation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
