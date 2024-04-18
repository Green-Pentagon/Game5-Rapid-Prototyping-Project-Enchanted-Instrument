using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawnerBehaviourScript : MonoBehaviour
{
    public Camera Camera;
    public GameObject ThingToSpawn;
    private int spawnLimit;
    private int spawnCount = 0;
    private KeyCode spawnKey = KeyCode.Mouse1; //default: mouse1, right click
    private bool spawnTriggered = false;

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
        if (spawnCount < spawnLimit)
        {
            if (Input.GetKeyDown(spawnKey) && ! spawnTriggered)
            {
                Instantiate(ThingToSpawn, Camera.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
                spawnTriggered = true;
            }
            else if (Input.GetKeyUp(spawnKey) && spawnTriggered)
            {
                spawnTriggered = false;
            }
        }
    }
}
