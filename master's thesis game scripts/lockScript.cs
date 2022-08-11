using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockScript : MonoBehaviour
{
    gameManagerScript gameManager;
    public GameObject door;
    public string keyType;
    public AudioClip audioclip;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.ToString().Contains("FirstPersonPlayer"))
        {
            if (gameManager.hasKey(keyType))
            {
                //gameManager.redKey = false;
                door.GetComponent<doorScript>().SendMessage("buttonPressed");
                AudioSource.PlayClipAtPoint(audioclip, gameObject.transform.position, 0.5f);
                gameObject.SetActive(false);
            }
        }
    }
}
