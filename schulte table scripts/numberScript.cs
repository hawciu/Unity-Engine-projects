using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class numberScript : MonoBehaviour
{
    public int value, locationX, locationY;
    public float scale, transparency;
    public gameManagerScript gameManager;
    public void setup(int number, int locX, int locY, float sc, float transp)
    {
        value = number;
        locationX = locX;
        locationY = locY;
        scale = sc;
        transparency = transp;

        if (value < 10) //wezszy collider bo jedna cyfra
        {
            BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
            col.size = new Vector2(col.size.x/2, col.size.y);
        }
        gameObject.GetComponent<TextMeshPro>().text = value.ToString();
    }

    private void OnMouseDown()
    {
        gameManager.clickLog.Add(Time.time + " nr: " + value +
            " pos: " + locationX+"/"+locationY + " sc: " + scale +
            " tr: " + transparency);
        gameManager.checkNumber(gameObject);
    }
}
