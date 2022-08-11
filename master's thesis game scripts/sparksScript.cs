using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparksScript : MonoBehaviour
{
    ParticleSystem ps;
    float lastFire = 0f;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        audiosource = GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastFire > 2f)
        {
            ps.Play();
            audiosource.Play();
            lastFire = Time.time;
        }
    }
}
