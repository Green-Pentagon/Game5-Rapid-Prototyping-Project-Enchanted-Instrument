using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class StorerBehaviour : MonoBehaviour
{

    public TextMeshPro StoreReadout;
    private AudioSource associatedSound;
    private int amountHeld = 0;
    private string SOUND_ID;

    public string getSoundID()
    {
        return SOUND_ID;
    }

    // Start is called before the first frame update
    void Start()
    {
        associatedSound = GetComponent<AudioSource>();
        if (associatedSound == null)
        {
            Debug.LogError("No associated sound source has been attached on object for StorerBehaviour.cs!");
            return;
        }
        SOUND_ID = associatedSound.name.Substring(0);
    }

    // Update is called once per frame
    private void Update()
    {
        StoreReadout.text = amountHeld.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered!");
        if (collision.gameObject.tag == "Resource")
        {
            amountHeld++;
            associatedSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
