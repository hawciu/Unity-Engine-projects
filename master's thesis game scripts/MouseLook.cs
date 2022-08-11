using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameObject kula;
    public GameObject marker;
    public GameObject gun;
    public bool canmove = false;

    public bool canShoot = false;
    public bool canLook = false;

    public bool uitype;

    float mouseSensitivity = 5f;
    float pitch;
    float yaw;
    float moveSpeed = 5f;
    float verticalVelocity = 0f;
    //float jumpDecay = 3f; //zwieksza grawitacje o tyle na sekunde
    float mouseDownStart = 0f;
    float bulletSpeed;
    float shootCooldown = 1f;
    float lastShot = 0f;
    float mouseDownDuration = 0f;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 moveVector = new Vector3(0, 0, 0);
    CharacterController playerController;
    Camera playerCamera;
    GameObject barrel;
    List<GameObject> markers = new List<GameObject>();

    public AudioSource audiosource;
    public AudioClip gunCharge;
    public AudioClip gunShot;
    public AudioSource audiosourceWalk;
    public AudioClip jumpclip;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
        audiosource = playerCamera.GetComponent<AudioSource>();
        barrel = GameObject.Find("barrel");
        playerController = gameObject.GetComponent<CharacterController>();
        gun.SetActive(false);

        playerCamera.backgroundColor = Color.black;

        //transform.position = GameObject.Find("start").transform.position;

        //spawn markers
        Vector3 pos = new Vector3(0, 0, 0);
        for (int i = 0; i < 10; i++)
        {
            GameObject tmp = Instantiate(marker, pos, Quaternion.identity);
            markers.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canLook)
        {
            updateMouseLook();
        }
        if (canmove)
        {
            updateMovement();
        }
        if (canShoot)
        {
            shooting();
        }
    }

    void shooting()
    {
        if (Time.time - (lastShot + shootCooldown) > 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (mouseDownDuration == 0f)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        markers[i].SetActive(true);
                    }
                }
                mouseDownDuration += Time.deltaTime;
                bulletSpeed = (mouseDownDuration) * 20f + 5f;
                if (bulletSpeed > 30f)
                {
                    bulletSpeed = 30f;
                }
                audiosource.pitch = bulletSpeed/30;
                if (!audiosource.isPlaying)
                {
                    audiosource.Play();
                }
                drawTrajectory(bulletSpeed);
            }
            if (Input.GetMouseButtonUp(0))
            {
                GameObject a = Instantiate(kula, barrel.transform.position, Quaternion.identity);
                a.GetComponent<projectileScript>().setup(barrel.transform.forward, bulletSpeed);
                lastShot = Time.time;
                mouseDownDuration = 0f;
                audiosource.Stop();
                AudioSource.PlayClipAtPoint(gunShot, gameObject.transform.position, 0.2f);
                for (int i = 0; i < 10; i++)
                {
                    markers[i].SetActive(false);
                }
            }
        }
    }

    void drawTrajectory(float speed, float czas = 1f)
    {
        if (uitype)
        {
            Vector3 start = new Vector3();
            Vector3 end = new Vector3();
            float ilesekund = czas;
            for (int i = 0; i < 10; i++)
            {
                float dropoffTime = i * (ilesekund / 10);
                Vector3 directionWithGravity = barrel.transform.forward;
                directionWithGravity.y -= dropoffTime * dropoffTime * 1f;
                Vector3 newpos = barrel.transform.position + directionWithGravity * dropoffTime * speed;
                if (i == 0)
                {
                    start = newpos;
                }
                else
                {
                    end = newpos;
                    Vector3 location = start + (end - start) / 2;
                    GameObject line = markers[i];
                    line.transform.position = location;
                    float skala = Vector3.Distance(start, end);
                    line.transform.localScale = new Vector3(
                    line.transform.localScale.x,
                    line.transform.localScale.y,
                    skala / 10);
                    line.transform.LookAt(end);
                    start = end;
                }
            }
        }
    }

    void updateMouseLook()
    {
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y"); //pionowo
        yaw += mouseSensitivity * Input.GetAxis("Mouse X"); //poziomo

        pitch = Mathf.Clamp(pitch, -90f, 90f);
        if (yaw < 0f)
        {
            yaw += 360f;
        }
        if (yaw > 360f)
        {
            yaw -= 360f;
        }
        transform.eulerAngles = new Vector3(0f, yaw, 0f);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);
    }

    void updateMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if ((x != 0 || y != 0) & playerController.isGrounded)
        {
            if (!audiosourceWalk.isPlaying)
            {
                audiosourceWalk.Play();
            }
        }
        else
        {
            audiosourceWalk.Stop();
        }
        moveVector = transform.right * x + transform.forward * y;
        moveVector.Normalize();

        if (playerController.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = 2f;// Mathf.Sqrt(jumpDecay * -2f * -20f);
            AudioSource.PlayClipAtPoint(jumpclip, gameObject.transform.position, 0.5f);
        }
        if (verticalVelocity > -2)
        {
            verticalVelocity -= 3f * Time.deltaTime;
        }

        moveVector.y = verticalVelocity;
        //playerController.Move(moveVector * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerController.Move(moveVector * moveSpeed * Time.deltaTime);
    }

    public void activateGun()
    {
        gun.SetActive(true);
        canShoot = true;
    }
}
