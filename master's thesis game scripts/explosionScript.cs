using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    float scaleGoal = 10f;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale += new Vector3(scaleGoal, scaleGoal, scaleGoal) * Time.deltaTime;
        if (gameObject.transform.localScale.x > 3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.ToString().Contains("poleSphere"))
        {
            //print("collided" + other.name);
            other.SendMessage("colorInput", "red");
        }
    }
}
