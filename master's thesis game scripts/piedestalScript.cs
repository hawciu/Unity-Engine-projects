using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piedestalScript : MonoBehaviour
{
    public float speed = 3f;
    public int inputs = 0;
    public bool buttonHeld = false;
    public bool openGate = false;

    Vector3 startPos = new Vector3();

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (inputs <= 0)
        {
            if(buttonHeld)
            {
                if (startPos.y - transform.position.y < 5f)
                {
                    Vector3 tmp = transform.position;
                    tmp.y -= speed * Time.deltaTime;
                    transform.position = tmp;
                }
            }
            else
            {
                if (startPos.y - transform.position.y >= 0f)
                {
                    Vector3 tmp = transform.position;
                    tmp.y += speed * Time.deltaTime;
                    transform.position = tmp;
                }
            }
        }
    }

    public void buttonPressed()
    {
        inputs -= 1;
    }

    public void buttonHeldFunction(bool ifButtonHeld)
    {
        buttonHeld = ifButtonHeld;
    }
}