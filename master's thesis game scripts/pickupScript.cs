using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour
{
    gameManagerScript gameManager;
    public string pickupType;
    public GameObject diamondParticle;
    public GameObject redKeyParticle;
    public GameObject yellowKeyParticle;
    public GameObject greenKeyParticle;
    public GameObject blueKeyParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * -100f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.ToString().Contains("FirstPersonPlayer"))
        {
            switch (pickupType)
            {
                case "diamond":
                    Instantiate(diamondParticle, gameObject.transform.position, Quaternion.identity);
                    break;
                case "redKey":
                    Instantiate(redKeyParticle, gameObject.transform.position, Quaternion.identity);
                    break;
                case "yellowKey":
                    Instantiate(yellowKeyParticle, gameObject.transform.position, Quaternion.identity);
                    break;
                case "blueKey":
                    Instantiate(blueKeyParticle, gameObject.transform.position, Quaternion.identity);
                    break;
                case "greenKey":
                    Instantiate(greenKeyParticle, gameObject.transform.position, Quaternion.identity);
                    break;
            }
            gameManager.SendMessage("pickup", pickupType);
            Destroy(gameObject);
        }
    }
}
