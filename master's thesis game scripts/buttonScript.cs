using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    bool pressed = false;

    public AudioClip audioclip;
    public GameObject wire, wireActive, gate, buttonModel;
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
        if (!pressed)
        {
            pressed = true;
            //print("trigger" + other.gameObject.ToString());
            Vector3 tmp = buttonModel.transform.position;
            tmp.y -= 0.3f;
            buttonModel.transform.position = tmp;
            gate.SendMessage("buttonPressed");
            wire.SetActive(false);
            wireActive.SetActive(true);
            AudioSource.PlayClipAtPoint(audioclip, gameObject.transform.position);
        }
    }
}
