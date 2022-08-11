using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    public GameObject gamemanager;

    private void OnMouseDown()
    {
        if (gamemanager.GetComponent<gameManagerScript>().game)
        {
            gamemanager.GetComponent<gameManagerScript>().clickLog.Add(Time.time + " bg click");
        }
    }       
}
