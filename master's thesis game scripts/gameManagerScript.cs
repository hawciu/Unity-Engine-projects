using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    public bool redKey, blueKey, greenKey, yellowKey = false;
    public GameObject diamondCounterUI;
    public AudioSource audiosource;
    public AudioClip diamondpickupclip;
    public AudioClip keypickupclip;
    public AudioClip gunpickupclip;
    public GameObject uiredkey, uiyellowkey, uigreenkey, uibluekey;
    public GameObject minimap;
    public GameObject buttonF;
    public GameObject buttonM;

    GameObject player;
    int diamonds = 0;
    float lastDiamondSound = 0;
    float diamondSoundPitch = 1.0f;
    bool uiType;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonPlayer");
    }

    void pickup(string pickupIn)
    {
        switch (pickupIn)
        {
            default:
                print("DEFAULT SWITCH PICKUP");
                break;

            case "redKey":
                redKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uiredkey.SetActive(true);
                }
                break;

            case "yellowKey":
                yellowKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uiyellowkey.SetActive(true);
                }
                break;

            case "greenKey":
                greenKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uigreenkey.SetActive(true);
                }
                break;

            case "blueKey":
                blueKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uibluekey.SetActive(true);
                }
                break;

            case "diamond":
                diamonds += 1;
                updateDiamondCounter();
                if (Time.time - lastDiamondSound > 0.5f)
                {
                    diamondSoundPitch = 1.0f;
                }
                else
                {
                    diamondSoundPitch += 0.02f;
                    if (diamondSoundPitch > 2)
                    {
                        diamondSoundPitch = 2;
                    }
                }
                lastDiamondSound = Time.time;
                //diamondSoundPitch = Random.Range(1.1f, 1f);
                audiosource.pitch = diamondSoundPitch;
                //audiosource.Play();
                audiosource.PlayOneShot(diamondpickupclip, 2);
                break;

            case "gun":
                player.SendMessage("activateGun");
                audiosource.PlayOneShot(gunpickupclip, 2);
                break;
        }
    }

    void updateDiamondCounter()
    {
        diamondCounterUI.SendMessage("updateAmount", diamonds);
    }

    public bool hasKey(string keyType)
    {
        switch (keyType)
        {
            default:
                return false;

            case "redKey":
                return redKey;

            case "blueKey":
                return blueKey;

            case "yellowKey":
                return yellowKey;

            case "greenKey":
                return greenKey;
        }
    }

    public void setUiType(bool uiTypeIn)
    {
        uiType = uiTypeIn;
        minimap.SetActive(uiTypeIn);
        buttonF.SetActive(false);
        buttonM.SetActive(false);
        diamondCounterUI.SetActive(true);
        if (!uiTypeIn)
        {
            diamondCounterUI.GetComponent<CanvasGroup>().alpha = 0;
        }
        diamondCounterUI.GetComponent<diamondCounterUIScript>().fullUI = uiTypeIn;
        player.GetComponent<MouseLook>().canmove = true;
        player.GetComponent<MouseLook>().canLook = true;
        player.GetComponent<MouseLook>().uitype = uiTypeIn;
        //cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
