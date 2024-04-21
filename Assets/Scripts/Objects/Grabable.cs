using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    public PlayTrack GameLogicPlayTrackScript;
    public Camera Camera;
    public bool startGrabbed = true;
    public bool isStickyNote = false;
    private BoxCollider2D Collider;
    private Rigidbody2D rb;
    private float defaultGravityScale;
    private KeyCode grabKey = KeyCode.Mouse0; //default: mouse0, left click
    private bool grabTriggered;
    private bool usesRB = false;
    private Vector3 WorldMousePos = Vector3.zero;
    private Vector3 grabOffset = Vector3.zero;

    public void PlayDestroyAudio()
    {
        GameLogicPlayTrackScript.PointSound();
    }

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
        Collider = GetComponent<BoxCollider2D>();
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
            if (Mathf.Abs(transform.position.x - WorldMousePos.x) <= Collider.bounds.size.x / 2)
            {
                if (Mathf.Abs(transform.position.y - WorldMousePos.y) <= Collider.bounds.size.y / 2)
                {
                    
                    Debug.Log("Grab Success!");
                    grabOffset = transform.position - WorldMousePos;
                    grabTriggered = true;
                    if (usesRB)
                    {
                        rb.gravityScale = 0.0f;
                        rb.velocity = Vector2.zero;
                    }

                    if (isStickyNote)
                    {
                        try
                        {
                            string test = (name.Replace("(Clone)", ""));
                            GameLogicPlayTrackScript.Play(test);
                        }
                        catch
                        {
                            Debug.LogError("Component marked as sticky note not named correctly!");
                        }
                        
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
