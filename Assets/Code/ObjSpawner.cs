using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public float radius;
    
    Spawner spawner;
    void Start()
    {
        spawner = Spawner.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawner.SpawnFromPool("enemy", transform.position, Quaternion.identity);
    }
}
