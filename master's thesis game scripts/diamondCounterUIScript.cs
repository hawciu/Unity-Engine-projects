using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class diamondCounterUIScript : MonoBehaviour
{
    public Text diamondCounterText;
    public bool fullUI = true;

    float UIalpha = 1f;
    float lastUpdate = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!fullUI & UIalpha > 0f)
        {
            if (lastUpdate + 2f < Time.time)
            {
                UIalpha -= Time.deltaTime * 5;
                setAlpha(UIalpha);
            }
        }
    }

    void updateAmount(int amount)
    {
        diamondCounterText.text = amount.ToString();
        lastUpdate = Time.time;
        UIalpha = 1f;
        setAlpha(UIalpha);
    }

    void setAlpha(float amount)
    {
        GetComponent<CanvasGroup>().alpha = amount;
    }
}
