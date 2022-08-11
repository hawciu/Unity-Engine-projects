using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEndScript : MonoBehaviour
{
    GameObject player;
    bool ending = false;
    bool end = false;
    Camera playerCamera;

    public GameObject cameraTarget;
    public GameObject endScreen;
    public AudioClip pstryk;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonPlayer");
        playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (ending)
        {
            var step = 1f * Time.deltaTime;
            playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, cameraTarget.transform.position, step);
            Vector3 newDirection = Vector3.RotateTowards(playerCamera.transform.forward, cameraTarget.transform.forward, step, 0.0f);
            playerCamera.transform.rotation = Quaternion.LookRotation(newDirection);

            if (Vector3.Distance(playerCamera.transform.position, cameraTarget.transform.position) < 0.1f)
            {
                ending = false;
                end = true;
            }
        }
        if (end)
        {
            endScreen.SetActive(true);
            AudioSource.PlayClipAtPoint(pstryk, playerCamera.transform.position, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.ToString().Contains("FirstPersonPlayer"))
        {
            player.GetComponent<MouseLook>().canmove = false;
            player.GetComponent<MouseLook>().canShoot = false;
            player.GetComponent<MouseLook>().canLook = false;
            ending = true;
        }
    }
}
