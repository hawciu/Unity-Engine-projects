using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public int inputs = 2;
    public bool buttonHeld = false;
    public GameObject door;
    public float speed = 3f;
    public float targetPos = 4.6f;

    AudioSource audioSource;
    bool doorMoving = false;
    Vector3 startPos = new Vector3();

    private void Start()
    {
        startPos = door.transform.localPosition;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {/*
        if (inputs <= 0 && buttonHeld)
        {
            if (door.transform.localPosition.x < startPos.x + targetPos)
            {
                print("opengate " + door.transform.localPosition.x);
                Vector3 tmp = new Vector3();
                tmp.x = door.transform.localPosition.x + 0.5f * Time.deltaTime;
                door.transform.localPosition = tmp;
                
            }
        }*/
        doorMoving = false;
        if (inputs <= 0)
        {
            if (buttonHeld)
            {
                if (door.transform.localPosition.y > startPos.y - targetPos)
                {
                    Vector3 tmp = door.transform.localPosition;
                    tmp.y -= speed * Time.deltaTime;
                    door.transform.localPosition = tmp;
                    doorMoving = true;
                }
            }
            else
            {
                if (door.transform.localPosition.y < startPos.y)
                {
                    Vector3 tmp = door.transform.localPosition;
                    tmp.y += speed * Time.deltaTime;
                    door.transform.localPosition = tmp;
                    doorMoving = true;
                }
            }
            if (doorMoving)
            {
                if(!audioSource.isPlaying)
                {
                    print("PLAY");
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    public void buttonPressed()
    {
        inputs -= 1;
    }

    public void buttonUnpressed()
    {
        inputs += 1;
        print("door " + inputs.ToString());
    }

    public void buttonHeldFunction(bool ifButtonHeld)
    {
        buttonHeld = ifButtonHeld;
    }
}
