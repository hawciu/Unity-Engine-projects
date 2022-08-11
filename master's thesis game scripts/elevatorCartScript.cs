
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorCartScript : MonoBehaviour
{
    bool elevatorMove = false;
    bool elevatorActive = true;
    bool room1Loaded = true;
    bool room2Loaded = false;
    gameManagerScript gameManager;
    GameObject player;
    string phase = "";
    float maxSpeed = 30f;
    float currentSpeed = 1f;
    float lastRingSpawn = 0f;
    float speedModifier = 5f; //acceleration/decelleration rate
    float maxSpeedTimer = 0f;
    float stoppingDelay = 0f;
    List<GameObject> rings = new List<GameObject>();
    AudioSource audioSource;

    public AudioClip ding;
    public GameObject doorL, doorR, ring, room1, room2, panelRoom1, panelRoom2;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        player = GameObject.Find("FirstPersonPlayer");
        audioSource = gameObject.GetComponent<AudioSource>();
        panelRoom1.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (elevatorMove)
        {
            switch(phase)
            {
                default:
                    break;

                case "start":
                    if (transform.position.y < 3f)
                    {
                        Vector3 tmp = transform.position;
                        tmp.y += Time.deltaTime;
                        transform.position = tmp;
                    }
                    else
                    {
                        phase = "speeding up";
                    }
                    break;

                case "speeding up":
                    playElevatorSound(currentSpeed);
                    if (room1Loaded)
                    {
                        room1Loaded = false;
                        room1.SetActive(false);
                        panelRoom1.SetActive(true);
                        panelRoom2.SetActive(false);
                    }
                    elevatorRings();
                    if (currentSpeed < maxSpeed)
                    {
                        currentSpeed += speedModifier * Time.deltaTime;
                    }
                    else
                    {
                        maxSpeedTimer += Time.deltaTime;
                        if(maxSpeedTimer > 5f)
                        {
                            phase = "slowing down";
                        }
                    }
                    break;

                case "slowing down":
                    playElevatorSound(currentSpeed);
                    elevatorRings();
                    if (currentSpeed > 1f)
                    {
                        currentSpeed -= speedModifier * Time.deltaTime;
                    }
                    else
                    {
                        maxSpeedTimer += Time.deltaTime;
                        if(maxSpeedTimer > 5f)
                        {
                            phase = "stopping";
                            stoppingDelay = Time.time;
                        }
                    }
                    break;

                case "stopping":
                    playElevatorSound(currentSpeed);
                    if (stoppingDelay + 2f < Time.time)
                    {
                        if (!room2Loaded)
                        {
                            room2Loaded = true;
                            room2.SetActive(true);
                            AudioSource.PlayClipAtPoint(ding, gameObject.transform.position);
                        }
                        GameObject.Find("elevatorTrigger").GetComponent<elevatorScript>().SendMessage("open");
                        elevatorMove = false;
                        elevatorActive = false;
                        phase = "";
                        transform.position = new Vector3(-40f, 0f, 97.75f);
                        doorL.transform.rotation = Quaternion.Euler(-90, 30, 90);
                        doorR.transform.rotation = Quaternion.Euler(-90, 150, 90);
                        Vector3 tmp = transform.position;
                        tmp.y = 0f;
                        transform.position = tmp;
                        player.transform.parent = null;
                        tmp = player.transform.position;
                        tmp.y = 1.2f;
                        player.transform.position = tmp;
                        if (audioSource.isPlaying)
                        {
                            audioSource.Stop();
                        }
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!elevatorMove && elevatorActive)
        {
            if (other.gameObject.ToString().Contains("FirstPersonPlayer"))
            {
                print("elevator cart player collide");
                doorL.transform.rotation = Quaternion.Euler(-90, 0, 90);
                doorR.transform.rotation = Quaternion.Euler(-90, 180, 90);
                GameObject.Find("elevatorTrigger").GetComponent<elevatorScript>().SendMessage("close");
                player.transform.parent = transform;
                elevatorMove = true;
                //phase = "start";
                phase = "speeding up";
            }
        }
    }

    void elevatorRings()
    {
        if (lastRingSpawn + 10f / currentSpeed < Time.time & phase != "stopping")
        {
            GameObject tmp = Instantiate(ring, new Vector3(-40f, 8f, 97.75f), Quaternion.identity);
            rings.Add(tmp);
            lastRingSpawn = Time.time;
        }
        foreach (GameObject i in rings)
        {
            if (i.transform.position.y < -3f)
            {
                continue;
            }
            Vector3 tmp = i.transform.position;
            tmp.y -= currentSpeed * Time.deltaTime;
            i.transform.position = tmp;
        }
    }

    void playElevatorSound(float speed)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.volume = 2f;
        }
        audioSource.pitch = speed / 15f;
    }
}
