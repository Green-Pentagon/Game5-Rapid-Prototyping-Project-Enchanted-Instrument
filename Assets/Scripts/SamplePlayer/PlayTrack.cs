using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrack : MonoBehaviour
{
    public AudioClip[] tracks;//add tracks in inspector
    private AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        if (tracks.Length == 0)
        {
            Debug.LogError("No tracks set in PlayTrack.cs Script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
