using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNoteSpawner : MonoBehaviour
{
    public GameObject[] ObjectToSpawn;
    public GameObject StickyNoteParent;
    public BoxCollider SpawnRegion;
    public bool CullAndSpawnNew = true;//will remove the oldest gameobject instance when the queue fills up every spawn delay tick
    public float SpawnDelay = 5.0f;
    public int MaxSpawnCount = 4;

    //private Queue<GameObject> spawnQueue = new Queue<GameObject>();
    private int spawnedInstances = 0;
    private float timerCurrent = 0.0f;
    private Vector3[] SpawnBounds = new Vector3[2];

    private GameObject temp;

    public void StickyDestroyed()
    {
        spawnedInstances--;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MaxSpawnCount < 0)
        {
            MaxSpawnCount = 0;
        }
        if (SpawnDelay <= 0.0f)
        {
            SpawnDelay = 0.1f;
        }
        SpawnBounds[0] = transform.position - (SpawnRegion.size / 2.0f);
        SpawnBounds[1] = transform.position + (SpawnRegion.size / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCurrent >= SpawnDelay)//if time to spawn
        {
            timerCurrent = 0.0f;
            //if (spawnQueue.Count >= MaxSpawnCount && CullAndSpawnNew)//removes first instance spawned by spawner if enabled
            //{
            //    temp = spawnQueue.Dequeue();
            //    Destroy(temp);
            //    temp = null;
            //}

            if (spawnedInstances < MaxSpawnCount)
            {
                Vector3 rndPos = new Vector3(Random.Range(SpawnBounds[0].x, SpawnBounds[1].x), Random.Range(SpawnBounds[0].y, SpawnBounds[1].y), Random.Range(SpawnBounds[0].z, SpawnBounds[1].z));
                spawnedInstances++;
                temp = Instantiate(ObjectToSpawn[Random.Range(0,ObjectToSpawn.Length)], rndPos, transform.rotation);
                temp.transform.SetParent(StickyNoteParent.transform);
                //spawnQueue.Enqueue(temp);
                temp = null;
            }
        }
        else
        {
            timerCurrent += Time.deltaTime;
        }
    }
}
