using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNoteBehaviour : MonoBehaviour
{
    private Grabable Grabable;
    private PlayTrack PlayTrack;
    // Start is called before the first frame update
    void Start()
    {
        PlayTrack = GetComponentInParent<PlayTrack>();
        Grabable = GetComponentInParent<Grabable>();
        Grabable.GameLogicPlayTrackScript = PlayTrack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
