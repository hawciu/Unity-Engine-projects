using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kapanieScript : MonoBehaviour
{
    public GameObject target;
    public GameObject projectile;

    GameObject projectile2;
    float lastDrop = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastDrop > 3)
        {
            lastDrop = Time.time;
            projectile2 = Instantiate(projectile, target.transform.position, Quaternion.identity);
            projectile2.GetComponent<projectileScript>().setup(target.transform.forward, 3f);
        }
    }
}
