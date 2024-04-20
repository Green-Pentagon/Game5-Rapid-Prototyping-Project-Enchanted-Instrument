using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public Camera Camera;
    public bool startGrabbed = true;
    private Rigidbody2D rb;
    private float defaultGravityScale;
    private KeyCode grabKey = KeyCode.Mouse0; //default: mouse0, left click
    private bool grabTriggered;
    private bool usesRB = false;
    private Vector3 WorldMousePos = Vector3.zero;
    private Vector3 grabOffset = Vector3.zero;

    
    public bool IsGrabbed()
    {
        return grabTriggered;
    }
    
    void UpdateMouseCoords()
    {
        WorldMousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
    }


    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D temp))
        {
            rb = temp;
            defaultGravityScale = rb.gravityScale;
            usesRB = true;
            if (startGrabbed)
            {
                rb.gravityScale = 0.0f;
                rb.velocity = Vector2.zero;
            }
        }

        grabTriggered = startGrabbed;
        if (Camera == null)
        {
            Camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseCoords();


        if (Input.GetKeyDown(grabKey) && !grabTriggered)
        {
            //UpdateMouseCoords();
            Debug.Log("Grab Triggered! MousePos = " + WorldMousePos + " | Transform: " + transform.position);
            if (Mathf.Abs(transform.position.x - WorldMousePos.x) <= transform.localScale.x/2)
            {
                if (Mathf.Abs(transform.position.y - WorldMousePos.y) <= transform.localScale.y / 2)
                {
                    Debug.Log("Grab Success!");
                    grabOffset = transform.position - WorldMousePos;
                    grabTriggered = true;
                    if (usesRB)
                    {
                        rb.gravityScale = 0.0f;
                        rb.velocity = Vector2.zero;
                    }
                }
            }

            
        }
        else if (Input.GetKeyUp(grabKey) && grabTriggered)
        {
            grabOffset = Vector3.zero;
            grabTriggered = false;
            if (usesRB)
            {
                rb.gravityScale = defaultGravityScale;
            }
        }


        if (grabTriggered)
        {

            transform.position = Vector2.Lerp(transform.position,WorldMousePos + grabOffset,0.1f);
        }
    }
}
