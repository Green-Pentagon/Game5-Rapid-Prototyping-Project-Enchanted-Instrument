using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawnerBehaviourScript : MonoBehaviour
{
    public Camera Camera;
    public GameObject ThingToSpawn;
    private Queue<GameObject> spawnStack = new Queue<GameObject>();
    private int spawnLimit = 10;
    //private KeyCode spawnKey = KeyCode.Mouse1; //default: mouse1, right click
    //private KeyCode resetKey = KeyCode.R;
    private bool spawnTriggered = false;
    private bool clearingTriggered = false;

    //IEnumerator ClearQueue()
    //{
    //    for (int i = spawnQueue.Count; i > 0; i--){
    //        Destroy(spawnQueue.Dequeue());
    //    }
    //    clearingTriggered = false;
    //    yield return null;
    //}

    public void TriggerSpawn()
    {
        spawnTriggered = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(resetKey) && !clearingTriggered)
        //{
        //    clearingTriggered = true;
        //    if (spawnStack.Count > 0)
        //    {
        //        Destroy(spawnStack.Pop());
        //    }
        //}
        //if (Input.GetKeyUp(resetKey) && clearingTriggered)
        //{
        //    clearingTriggered = false;
        //}
        if (spawnStack.Count >= spawnLimit && spawnTriggered)
        {
            spawnStack.Dequeue();
        }

        if (spawnStack.Count < spawnLimit && !clearingTriggered)
        {
            if (spawnTriggered)
            {
                spawnStack.Enqueue(Instantiate(ThingToSpawn, new Vector3(Camera.ScreenToWorldPoint(Input.mousePosition).x, Camera.ScreenToWorldPoint(Input.mousePosition).y, 0.0f), transform.rotation));
                spawnTriggered = false;
            }
            //if (Input.GetKeyDown(spawnKey) && ! spawnTriggered)
            //{
            //    spawnStack.Push(Instantiate(ThingToSpawn, new Vector3(Camera.ScreenToWorldPoint(Input.mousePosition).x,Camera.ScreenToWorldPoint(Input.mousePosition).y, 0.0f), transform.rotation));
            //    spawnTriggered = true;
            //}
            //else if (Input.GetKeyUp(spawnKey) && spawnTriggered)
            //{
            //    spawnTriggered = false;
            //}
        }

        if (spawnStack.Count > 0)//in the event that if a game object is destroyed but not cleared out of the stack.
        {
            if (spawnStack.Peek().gameObject == null)
            {
                spawnStack.Dequeue();
            }
        }
    }
}
