using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public Sprite[] States;
    private SpriteRenderer sr;
    private Grabable ParentGrabScript;

    // Start is called before the first frame update
    void Start()
    {
        ParentGrabScript = GetComponentInParent<Grabable>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParentGrabScript.IsGrabbed())
        {
            sr.sprite = States[1];
        }
        else
        {
            sr.sprite = States[0];
        }
    }

}
