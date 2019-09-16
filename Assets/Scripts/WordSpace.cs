using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpace : SingletonBehaviour<WordSpace>
{
    public List<WordObject> words;

    public List<string>[] stringWords;
    public float brainWeight = 0; //Current weight of brain
    public int maximumWeight = 200; //Max weight of brain

    public Sprite[,] wordBackgrounds; //Sprites of word background

    public bool isGameOver = false;

    private float gameOverTime = 5; //Time to game over
    private Coroutine gameOverTimer = null;

    private bool isGameOverTimerOn = false; //Check if game over timer is on 
    public float totalTyping = 0, playerTyping = 0;


    public void RemoveWord(string wordText)
    {
        foreach (WordObject child in words)
        {
            if (child.wordText == wordText)
            {
                totalTyping += (WordReader.GetWordTyping(child.wordText) + 1);
                words.Remove(child);
                brainWeight -= child.wordWeight;
                Destroy(child.gameObject);
                return;
            }
        }
    }

    public IEnumerator GameOverTimer(float startTime)
    {
        isGameOverTimerOn = true;
        while (Time.time - startTime < gameOverTime)
        {
            if (brainWeight <= maximumWeight)
            {
                isGameOverTimerOn = false;
                StopCoroutine(gameOverTimer);
            }
            yield return null;
        }
        GameOver();
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
    }

    void Awake()
    {
        words = new List<WordObject>();
        wordBackgrounds = new Sprite[4, 5];
        stringWords = new List<string>[4];
        for (int i = 0; i < 4; i++) stringWords[i] = new List<string>();
        ResourcesLoader.ParseCSVFile();
        ResourcesLoader.LoadImages();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (brainWeight > maximumWeight && !isGameOverTimerOn) gameOverTimer = StartCoroutine(GameOverTimer(Time.time));
            //playerTyping = totalTyping / Time.time;
        }
    }
}
