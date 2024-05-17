using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public string parentName;

        public GameObject prefab;

        public int poolSize;

        public List<GameObject> poolObjects;
    }

    [SerializeField] List<Pool> pools;

    void Awake()
    {
       if(Instance != null && Instance != this)
       {
            Destroy(this.gameObject);
       } 
       else
       {
            Instance = this;
       }
    }
    // Start is called before the first frame update
    void Start()
    {

        GameObject obj;

        foreach (Pool pool in pools)
        {
            GameObject parent = new GameObject(pool.parentName);

            for(int i = 0; i < pool.poolSize; i++)
            {
                obj = Instantiate(pool.prefab);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(false);
                pool.poolObjects.Add(obj);
            }
        }

        
    }

    public GameObject GetPooledObjects(int selectedPool, Vector3 position, Quaternion rotation)
    {
        for (var i = 0; i < pools[selectedPool].poolSize; i++)
        {
            if(!pools[selectedPool].poolObjects[i].activeInHierarchy)
            {
                GameObject objectToSpawn;
                objectToSpawn = pools[selectedPool].poolObjects[i];
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                return objectToSpawn;
            }
        }
        return null;
    }
}
