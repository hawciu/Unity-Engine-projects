using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHoldScript : MonoBehaviour
{

    bool pressed = false;
    public GameObject wire, wireActive, gate, buttonModel;
    public AudioClip audioclip1;
    public AudioClip audioclip2;
    //Vector3 startPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        pressed = true;
        //print("trigger in" + other.gameObject.ToString());
        Vector3 tmp = buttonModel.transform.position;
        tmp.y -= 0.3f;
        buttonModel.transform.position = tmp;
        gate.SendMessage("buttonHeldFunction", true);
        wire.SetActive(!pressed);
        wireActive.SetActive(pressed);
        AudioSource.PlayClipAtPoint(audioclip1, gameObject.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        pressed = false;
        //print("trigger out" + other.gameObject.ToString());
        Vector3 tmp = buttonModel.transform.position;
        tmp.y += 0.3f;
        buttonModel.transform.position = tmp;
        gate.SendMessage("buttonHeldFunction", false);
        wire.SetActive(!pressed);
        wireActive.SetActive(pressed);
        AudioSource.PlayClipAtPoint(audioclip2, gameObject.transform.position);
    }

    
}
