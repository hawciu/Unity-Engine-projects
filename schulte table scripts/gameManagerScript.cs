using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class gameManagerScript : MonoBehaviour
{
    int numbersAmount = 10;
    int czasBadania = 180;
    int currentNumber = 1, wrongClicksInaRow = 0;
    public GameObject numberPrefab, uiToggle, uiNumbersAmount, uiClickLog, particlePointer;
    public Canvas uiCanvas, resultCanvas;
    float camHorizontalSize, camVerticalSize, gameStartTime, lastClick;
    public List<string> clickLog = new List<string>();
    bool flashed = false;
    public bool game = false, gameStarted = false;
    List<System.Tuple<int, int>> positions = new List<System.Tuple<int, int>>();
    List<GameObject> numberObjects = new List<GameObject>();
    float timeflash = 5f; //5f;

    void Start()
    {
        camHorizontalSize = Camera.main.orthographicSize * Screen.width / Screen.height;
        camVerticalSize = Camera.main.orthographicSize;
    }

    void resetPositions()
    {
        positions.Clear();
        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                positions.Add(System.Tuple.Create(i, j));
            }
        }
    }

    private void Update()
    {
        if (((Time.time - gameStartTime) > czasBadania)  && gameStarted)
        {
            gameStarted = false;
            game = false;
            uiCanvas.gameObject.SetActive(false);
            clickLog.Add(Time.time + " zakonczenie");
            resultCanvas.gameObject.SetActive(true);
            saveResults();
            //uiClickLog.GetComponent<TextMeshProUGUI>().text = stringTMP;
        }
        if (game && !flashed)
        {
            if (wrongClicksInaRow > 2 || Time.time - lastClick > timeflash)
            {
                clickLog.Add(Time.time + " nr flash");
                flashed = true;
                particlePointer.SetActive(true);
                particlePointer.transform.position = numberObjects[currentNumber - 1].transform.position;
            }
        }
    }

    public void startGame()
    {
        print("startGame");
        resetPositions();
        uiCanvas.gameObject.SetActive(false);
        resetPlanszy();
        for (int i = 0; i < numbersAmount; i++)
        {
            GameObject tmp = Instantiate(numberPrefab);
            tmp.GetComponent<numberScript>().gameManager = gameObject.GetComponent<gameManagerScript>();
            randomizeNumberObject(tmp, i+1);
            numberObjects.Add(tmp);
        }
        if (!gameStarted)
        {
            gameStartTime = Time.time;
        }
        lastClick = Time.time;
        clickLog.Add(gameStartTime + " game started");
        game = true;
        gameStarted = true;
    }

    void randomizeNumberObject(GameObject numberObject, int value)
    {
        float stepX = camHorizontalSize / 2.5f;
        float stepY = camVerticalSize / 2f;
        int positionIndex = Random.Range(0, positions.Count);
        print("RND positionindex" + positionIndex +" positions "+positions.Count);
        int positionX = positions[positionIndex].Item1;
        int positionY = positions[positionIndex].Item2;

        positions.RemoveAt(positionIndex);

        //location
        numberObject.transform.position = new Vector3(-camHorizontalSize + positionX * stepX - stepX / 2,
            -camVerticalSize + positionY * stepY - stepY / 2,
            numberObject.transform.position.z);

        //scale
        float randomizedScale = 1 + Random.Range(1, 3) *0.5f;
        numberObject.transform.localScale = new Vector3(randomizedScale, randomizedScale, 1);

        //transparency
        float randomizedTransparency = 1 - Random.Range(0, 2) * 0.2f;

        //color
        if (uiToggle.GetComponent<Toggle>().isOn)
        {
            int colorRng = Random.Range(0, 3);
            Color randomizedColor = new Color();
            switch(colorRng)
            {
                case 0: //ciemny zielony
                    randomizedColor = new Color(147f / 255f, 177f / 255f, 45f / 255f, randomizedTransparency);
                    break;

                case 1://zolty
                    randomizedColor = new Color(220f / 255f, 175f / 255f, 0f / 255f, randomizedTransparency);
                    break;

                case 2://brazowy
                    randomizedColor = new Color(102f / 255f, 69f / 255f, 22f / 255f, randomizedTransparency);
                    break;                    
            }
            numberObject.GetComponent<TextMeshPro>().color = randomizedColor;
            new Color(72f / 255f, 73f / 255f, 15f / 255f, randomizedTransparency);
        }      
        numberObject.GetComponent<numberScript>().setup(value, positionX, positionY, randomizedScale, randomizedTransparency);
    }    

    public void checkNumber(GameObject numberObject)
    {
        lastClick = Time.time;
        if (numberObject.GetComponent<numberScript>().value == currentNumber)
        {
            wrongClicksInaRow = 0;
            flashed = false;
            particlePointer.SetActive(false);
            Destroy(numberObject);
            if (currentNumber == numbersAmount)
            {
                uiCanvas.gameObject.SetActive(true);
                game = false;
            }
            else
            {
                currentNumber++;
            }
        }
        else
        {
            wrongClicksInaRow++;
        }
    }

    void resetPlanszy()
    {
        currentNumber = 1;
        foreach (GameObject i in numberObjects)
        {
            Destroy(i);
        }
        numberObjects.Clear();
        particlePointer.SetActive(false);
    }

    void saveResults()
    {
        string fileName = "wynik.txt";
        StreamWriter sr = File.CreateText(fileName);
        foreach (string i in clickLog)
        {
            sr.WriteLine(i + ",");
        }
        sr.Close();
    }

    public void quitgaem()
    {
        Application.Quit();
    }
}
