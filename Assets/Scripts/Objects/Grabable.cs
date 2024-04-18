using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public Camera Camera;
    private Rigidbody2D rb;
    private KeyCode grabKey = KeyCode.Mouse0; //default: mouse0, left click
    private bool grabTriggered = false;
    private Vector3 WorldMousePos = Vector3.zero;
    private Vector3 grabOffset = Vector3.zero;

    void UpdateMouseCoords()
    {
        WorldMousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
    }


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (Camera == null)
        {
            Camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (Input.GetKeyDown(grabKey) && !grabTriggered)
        {
            UpdateMouseCoords();
            Debug.Log("Grab Triggered! MousePos = " + WorldMousePos + " | Transform: " + transform.position);
            if (WorldMousePos.x >= (transform.position.x - transform.localScale.x/2) && WorldMousePos.x <= (transform.position.x + transform.localScale.x / 2))
            {
                if (WorldMousePos.y >= (transform.position.y - transform.localScale.x / 2) && WorldMousePos.y <= (transform.position.x + transform.localScale.y / 2))
                {
                    Debug.Log("Grab Success!");
                    grabOffset = transform.position - WorldMousePos;
                    rb.gravityScale = 0.0f;
                    rb.velocity = Vector3.zero;
                    grabTriggered = true;
                }
            }

            
        }
        else if (Input.GetKeyUp(grabKey) && grabTriggered)
        {
            grabOffset = Vector3.zero;
            rb.gravityScale = 1.0f;
            rb.centerOfMass = Vector3.zero;
            grabTriggered = false;
        }


        if (grabTriggered)
        {
            UpdateMouseCoords();
            transform.position = WorldMousePos + grabOffset;
            rb.centerOfMass = grabOffset;
        }
    }
}
