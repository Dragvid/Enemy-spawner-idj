using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public float radius;
    //public Vector3 spawnPos;
    private int c ;
    bool born;
    public int total;
    Spawner spawner;
    void Start()
    {
        Spawner spawnInfo = GetComponent<Spawner>();
        //total =spawnInfo.sizeExt;
        born = false;
        spawner = Spawner.instance;
    }

    void FixedUpdate()
    {
        if (!born)
        {
            spawner.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
            c++;
        }
        if (c == total)
        {
            born = true;
        }

    }
}
