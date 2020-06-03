using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public int difficulty;
    }

    public static Spawner instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> objpoolDictionary;
    public Dictionary<string, int> difpoolDictionary;
    void Start()
    {
        difpoolDictionary = new Dictionary<string, int>();
        objpoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            objpoolDictionary.Add(pool.tag, objectPool );
            difpoolDictionary.Add(pool.tag, pool.difficulty);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, int dif, Quaternion rotation)
    {
        if (!objpoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool w tag "+tag+" doesnt exist");
            return null;
        }
        if (difpoolDictionary[tag] <= dif) {
            GameObject objectToSpawn = objpoolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            i_PooledObj pooledObj = objectToSpawn.GetComponent<i_PooledObj>();
            if (pooledObj != null)
            {
                pooledObj.OnObjSpawn();
            }
            objpoolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        else
        {
            Debug.Log("não nesta dif");
            return null;
        }
        
    }
}
