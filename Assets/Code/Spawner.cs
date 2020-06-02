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
    }

    //public int sizeExt;
    public static Spawner instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        //sizeExt = Pool.size;
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            //sizeExt = pool.size;
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool w tag "+tag+" doesnt exist");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        i_PooledObj pooledObj = objectToSpawn.GetComponent<i_PooledObj>();
        if (pooledObj != null)
        {
            pooledObj.OnObjSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
