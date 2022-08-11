using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public GameObject marker;
    public GameObject markerG;
    public GameObject explosion;
    public Vector3 direction;
    public AudioClip audioclip;

    float speed = 0f;
    float dropoffTime = 0f; //wartosc x paraboli opadania
    float fallSpeed = 1f;
    Vector3 startPos = new Vector3(0, 0, 0);
    Vector3 directionWithGravity = new Vector3(0, 0, 0);
    Vector3 lastLocation = new Vector3(0, 0, 0);


    void Start()
    {
        startPos = gameObject.transform.position;
        lastLocation = startPos;
    }


    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, startPos) > 100f)
        {
            Destroy(gameObject);
        }
        dropoffTime += Time.deltaTime;
        directionWithGravity = direction;
        directionWithGravity.y -= dropoffTime * dropoffTime;
        Vector3 newpos = startPos + directionWithGravity * dropoffTime * speed;
        gameObject.transform.position = newpos;
        updateCollision();
        lastLocation = gameObject.transform.position;
    }

    private void updateCollision()
    {
        RaycastHit hit;
        Ray ray = new Ray(lastLocation, gameObject.transform.position);

        Debug.DrawRay(lastLocation, lastLocation-gameObject.transform.position, Color.green, 1000);

        LayerMask layerMask2 = LayerMask.GetMask("PLAYER", "PROJECTILE");
        if (Physics.Raycast(ray, out hit, Vector3.Distance(lastLocation, gameObject.transform.position), ~layerMask2))
        {
            collided(hit.point);
        }
    }
    public void setup(Vector3 directionin, float speedin, float fallSpeedin = 1f)
    {
        direction = directionin;
        speed = speedin;
        fallSpeed = fallSpeedin;
    }

    private void OnTriggerEnter(Collider other)
    {
        collided(gameObject.transform.position);
    }

    private void collided(Vector3 locationin)
    {
        Instantiate(explosion, locationin, Quaternion.identity);
        //AudioSource.PlayClipAtPoint(audioclip, gameObject.transform.position, 3f);
        Destroy(gameObject);
    }
}
