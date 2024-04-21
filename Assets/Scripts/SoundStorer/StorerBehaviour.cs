using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StorerBehaviour : MonoBehaviour
{
    public Sprite[] SpriteStates;
    public TextMeshPro[] StoreReadout;
    public AudioSource[] associatedSounds;
    private SpriteRenderer SR;
    private int[] amountHeld = {0,0,0,0};
    private bool isEmpty = true;
    private string ResourceSoundID;

    private bool IsOrderComplete(string stickyName)
    {
        Dictionary<string, int> AmountNeeded = new Dictionary<string, int>();
        bool isValid = true;
        AmountNeeded.Add("0", 0);
        AmountNeeded.Add("1", 0);
        AmountNeeded.Add("2", 0);
        AmountNeeded.Add("3", 0);
        for (int i = 0; i < stickyName.Length ;i++){
            AmountNeeded[stickyName.Substring(i, 1)] += 1;
        }

        for (int i = 0; i < amountHeld.Length; i++)
        {
            if (amountHeld[i] != AmountNeeded[i.ToString()])
            {
                isValid = false;
                break;
            }
        }
         return isValid;
    }


    IEnumerator UpdateText()
    {
        for (int i = 0; i < amountHeld.Length; i++)
        {
            StoreReadout[i].text = amountHeld[i].ToString();
        }
        yield return null;
    }

    public void Reset()
    {
        for (int i = 0; i < amountHeld.Length; i++)
        {
            amountHeld[i] = 0;
            isEmpty = true;
            SR.sprite = SpriteStates[0];
        }
        StartCoroutine(UpdateText());
    }

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        if (associatedSounds == null)
        {
            Debug.LogError("No associated sound source has been attached on object for StorerBehaviour.cs!");
            return;
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered!");
        if (collision.gameObject.tag == "Resource")
        {
            ResourceSoundID = collision.gameObject.name.Substring(0,1);
            Debug.Log("Sound ID " + ResourceSoundID + " Collided");
            for(int i = 0; i < associatedSounds.Length; i++)
            {
                if (associatedSounds[i].clip.name.Substring(0,1) == ResourceSoundID)
                {
                    if (isEmpty)
                    {
                        SR.sprite = SpriteStates[1];
                        isEmpty = false;
                    }
                    associatedSounds[i].Play();
                    amountHeld[i]++;
                }
            }
            Destroy(collision.gameObject);

            StartCoroutine(UpdateText());
        }
        else if (collision.gameObject.tag == "StickyNote")
        {
            if (IsOrderComplete(collision.gameObject.name))
            {
                Destroy(collision.gameObject);
                Reset();
            }
        }
    }
}
