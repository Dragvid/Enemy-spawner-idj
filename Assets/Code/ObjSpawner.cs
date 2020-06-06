using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public float radius;
    public int difficulty;
    public int objCount;
    public float waveBreak;
    public int waveCount;
    private Vector3 spawnPos;
    private int c ;
    float countdown;
    bool triggered;
    bool inProcess;
    Spawner spawner;
    void Start()
    {
        triggered = false;
        countdown = waveBreak; 
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
    void Update() {
        //if (waveCount != 0 && triggered==true)
        if (waveCount != 0)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0 && !inProcess)
            {
                waveCount--;
                countdown = waveBreak;
                c = 0;
                inProcess = true;
            }
        }
        else
        {
            triggered = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            triggered = true;
        }
    }
}
