using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrack : MonoBehaviour
{
    public AudioClip[] tracks;//add tracks in inspector
    private AudioSource AudioSource;

    public void Play(string trackName)
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].name == trackName)
            {
                AudioSource.clip = tracks[i];
                AudioSource.Play();
            }
        }
    }

    public void PointSound()
    {
        AudioSource.clip = tracks[0];
        AudioSource.Play();
    }

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
