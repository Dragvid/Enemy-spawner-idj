using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public float radius;
    public int difficulty;
    public int objCount;
    private Vector3 spawnPos;
    private int c ;
    bool inProcess;
    Spawner spawner;
    void Start()
    {
        if(difficulty>0)
        objCount *= difficulty;
        Spawner spawnInfo = GetComponent<Spawner>();
        spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y , transform.position.z + Random.Range(0f, radius));
        inProcess = true;
        spawner = Spawner.instance;
        //Debug.Log(spawnInfo.dictionarySize);
    }

    void FixedUpdate()
    {
        if (inProcess)
        {
            //por aqui probabilidade de dar spawn de tipos diferentes
            spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y + Random.Range(0f, radius), transform.position.z + Random.Range(0f, radius));
            spawner.SpawnFromPool("Enemy", spawnPos, difficulty, Quaternion.identity);
            spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y + Random.Range(0f, radius), transform.position.z + Random.Range(0f, radius));
            spawner.SpawnFromPool("Enemy2", spawnPos, difficulty, Quaternion.identity);
            c++;
        }
        if (c >= objCount)
        {
            inProcess = false;
        }

    }
}
