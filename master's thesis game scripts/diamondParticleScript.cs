using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondParticleScript : MonoBehaviour
{
    float start = 0f;

    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - start > 3)
        {
            Destroy(gameObject);
        }
    }
}
