using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public float radius;
    public Vector3 spawnPos;
    private int c ;
    bool inProcess;
    public int total;
    Spawner spawner;
    void Start()
    {
        Spawner spawnInfo = GetComponent<Spawner>();
        spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y + Random.Range(0f, radius), transform.position.z + Random.Range(0f, radius));
        inProcess = false;
        spawner = Spawner.instance;
    }

    void FixedUpdate()
    {
        if (!inProcess)
        {
            spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y + Random.Range(0f, radius), transform.position.z + Random.Range(0f, radius));
            //spawner.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            spawner.SpawnFromPool("Enemy", spawnPos, Quaternion.identity);
            c++;
        }
        if (c == total)
        {
            inProcess = true;
        }

    }
}
