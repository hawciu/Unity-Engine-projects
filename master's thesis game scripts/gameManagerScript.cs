using UnityEngine;
using System.IO;

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
    public GameObject ankietaPo;

    GameObject player;
    int diamonds = 0;
    float lastDiamondSound = 0;
    float diamondSoundPitch = 1.0f;
    bool uiType;
    string fileName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonPlayer");
        fileName = "wynik_" + System.DateTime.Now.ToString().Replace("/","-").Replace(":", "-") + ".txt";
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
                writeToLog(Time.time.ToString() + "," + "red key");
                break;

            case "yellowKey":
                yellowKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uiyellowkey.SetActive(true);
                }
                writeToLog(Time.time.ToString() + "," + "yellow key");
                break;

            case "greenKey":
                greenKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uigreenkey.SetActive(true);
                }
                writeToLog(Time.time.ToString() + "," + "green key");
                break;

            case "blueKey":
                blueKey = true;
                AudioSource.PlayClipAtPoint(keypickupclip, player.transform.position);
                if (uiType)
                {
                    uibluekey.SetActive(true);
                }
                writeToLog(Time.time.ToString() + "," + "blue key");
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
                writeToLog(Time.time.ToString() + "," + "diamond");
                break;

            case "gun":
                player.SendMessage("activateGun");
                audiosource.PlayOneShot(gunpickupclip, 2);
                writeToLog(Time.time.ToString() + "," + "gun");
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
        writeToLog(Time.time.ToString() + " " + uiType.ToString());
    }

    public void writeToLog(string textToLog)
    {
        textToLog = textToLog + "," + player.transform.position.ToString();
        string path = fileName;
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(textToLog);
            }
        }
        else
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(textToLog);
            writer.Close();
        }
    }

    public void aktywujAnkietePo(GameObject button)
    {
        button.SetActive(false);
        ankietaPo.SetActive(true);
    }
}
