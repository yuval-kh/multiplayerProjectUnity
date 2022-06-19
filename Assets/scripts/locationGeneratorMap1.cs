using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locationGeneratorMap1 : MonoBehaviour
{
    List<Vector3> lst;
    Vector3 SamePosition;
    [SerializeField]
    bool isSameToAll;


    // Start is called before the first frame update
    void Start()
    {
        var locations = GameObject.Find("SpawnLocations");
        lst = new List<Vector3>();
        foreach (Transform child in locations.transform)
        {
            Debug.Log(child.position);
            lst.Add(child.position);
        }
        SamePosition = generateLocation();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public Vector3 generateLocation()
    {
        if (!isSameToAll)
        {
            return generateRandomVector();
        }
        else return SamePosition;
    }
    public Vector3 generateRandomVector()
    {
        int length = lst.Count;
        int rndIndex = Random.Range(0, length);
        Debug.Log("index: " + rndIndex);
        Debug.Log("Result: " + lst[rndIndex]);
        return lst[rndIndex];
    }
}
