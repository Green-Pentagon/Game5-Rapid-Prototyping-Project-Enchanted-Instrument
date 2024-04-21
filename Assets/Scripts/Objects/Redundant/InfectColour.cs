using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class InfectColour : MonoBehaviour
{
    public Color defaultColour = Color.white;
    public Color infectedColour = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = infectedColour;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = defaultColour;
        }
    }
}
