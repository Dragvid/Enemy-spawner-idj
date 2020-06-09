using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public float radius;
    public int difficulty;
    public bool multiplier;
    public int objCount;
    public float waveBreak;
    public int waveCount;
    private Vector3 spawnPos;
    private int c ;
    float countdown;
    bool nearPlayer;
    bool inProcess;
    Spawner spawner;
    void Start()
    {
        nearPlayer = false;
        countdown = waveBreak; 
        if(difficulty>0 && multiplier)
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
            spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y + Random.Range(0f, radius), transform.position.z + Random.Range(0f, radius));
            spawner.SpawnFromPool("Enemy", spawnPos, Random.Range(0, 100), difficulty, Quaternion.identity);
            spawnPos = new Vector3(transform.position.x + Random.Range(0f, radius), transform.position.y + Random.Range(0f, radius), transform.position.z + Random.Range(0f, radius));
            spawner.SpawnFromPool("Enemy2", spawnPos, Random.Range(0, 100), difficulty, Quaternion.identity);
            c++;
        }
        if (c >= objCount)
        {
            inProcess = false;
        }

    }
    void Update() {
        //if (waveCount != 0 && nearPlayer==true)
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            nearPlayer = true;
        }
    }
}
