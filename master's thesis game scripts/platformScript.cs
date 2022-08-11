using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{
    float interpolationTime = 0f;
    Vector3 startPos;
    public Vector3 endPos;
    bool kierunek = true;
    Vector3 lastPos = new Vector3(0f, 0f, 0f);
    GameObject player;
    private Rigidbody rb;

    public bool canMove = true;
    public int inputs = 2;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x + endPos.x, startPos.y + endPos.y, startPos.z + endPos.z);
        //endPos = startPos + new Vector3(0f, 5f, 0f);
        player = GameObject.Find("FirstPersonPlayer");
        lastPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            interpolationTime += Time.deltaTime;
            if (interpolationTime > 1f)
            {
                interpolationTime = 0f;
                kierunek = !kierunek;
            }
            if (kierunek)
            {
                //transform.position = Vector3.Lerp(startPos, endPos, interpolationTime);

                rb.MovePosition(Vector3.Lerp(startPos, endPos, interpolationTime));
            }
            else
            {
                //transform.position = Vector3.Lerp(endPos, startPos, interpolationTime);
                rb.MovePosition(Vector3.Lerp(endPos, startPos, interpolationTime));
            }



            if (transform.childCount > 0)
            {
                Vector3 tmp = rb.velocity;
                //tmp.y = 0f;
                player.GetComponent<CharacterController>().Move(tmp * Time.deltaTime);
                //transform.GetChild(0).position = transform.GetChild(0).position + (transform.position - lastPos);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("trigger enter " + other.name);
        if (other.gameObject.ToString().Contains("FirstPersonPlayer"))
        {
            player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.ToString().Contains("FirstPersonPlayer"))
        {
            player.transform.parent = null;
        }
    }


    public void buttonPressed()
    {
        inputs -= 1;
        if (inputs <= 0)
        {
            canMove = true;
        }
    }
}
