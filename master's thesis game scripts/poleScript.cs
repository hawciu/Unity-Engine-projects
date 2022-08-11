using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poleScript : MonoBehaviour
{
    public GameObject theThing, wire, wireActive;
    string currentColor = "white";
    public string targetColor = "red";
    public AudioClip audioclip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void colorInput(string colorIn)
    {
        if (currentColor != colorIn)
        {
            currentColor = colorIn;
            switch (colorIn)
            {
                default:
                    break;

                case "red":
                    gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                                        break;
            }

            if (colorIn == targetColor)
            {
                activateTheThing(true);
                AudioSource.PlayClipAtPoint(audioclip, gameObject.transform.position);
            }
            else
            {
                activateTheThing(false);
            }
        }
    }

    void activateTheThing(bool boolIn)
    {
        if (boolIn)
        {
            theThing.SendMessage("buttonPressed");
        }
        else
        {
            theThing.SendMessage("buttonUnpressed");
        }
        wire.SetActive(!boolIn);
        wireActive.SetActive(boolIn);

    }
}
