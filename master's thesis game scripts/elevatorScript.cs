using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorScript : MonoBehaviour
{
    bool elevatorActivated = false;
    bool cartInPlace = false;
    bool closeDoors = false;
    public GameObject doorL, doorR, cart;

    float currentRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (elevatorActivated)
        {
            if(currentRotation < 30f)
            {
                float rotateValue = Time.deltaTime * 30f;
                currentRotation += rotateValue;
                if (closeDoors)
                {
                    rotateValue *= -1;
                }
                doorL.transform.Rotate(new Vector3(0f, -rotateValue, 0f));
                doorR.transform.Rotate(new Vector3(0f, rotateValue, 0f));
            }
            if(!cartInPlace && cart.transform.position.y > 0)
            {
                Vector3 tmp = cart.transform.position;
                tmp.y -= Time.deltaTime;
                cart.transform.position = tmp;
            }
            else
            {
                cartInPlace = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!elevatorActivated)
        {
            elevatorActivated = true;
        }
    }

    void close()
    {
        currentRotation = 0f;
        closeDoors = true;
    }

    void open()
    {
        currentRotation = 0f;
        closeDoors = false;
    }
}
