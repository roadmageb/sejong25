using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpace : SingletonBehaviour<WordSpace>
{
    [Header("Prefabs")]
    public TextInputField textInputField;
    [Header("Instances")]
    public List<WordObject> words;
    public List<NameWord> nameWords;
    public TextInputField currentInput;

    public List<string>[] stringWords;
    public float brainWeight = 0; //Current weight of brain
    public int maximumWeight = 200; //Max weight of brain

    public Sprite[,] wordBackgrounds; //Sprites of word background
    public Sprite[] hopaeBackgrounds; //Sprites of hopae background

    public bool isGameOver = false;

    private float gameOverTime = 5; //Time to game over
    private Coroutine gameOverTimer = null;

    private bool isGameOverTimerOn = false; //Check if game over timer is on 
    public float totalTyping = 0, playerTyping = 0; //Total wordTyping / WordTyping per seconds
    public float playerTypingRate = 0;
    public PhaseEnum currentPhase; //Information of current phase.
    public string koreanInput = "";

    /// <summary>
    /// Find and remove word
    /// </summary>
    /// <param name="wordText">Text to remove</param>
    public void RemoveWord(string wordText)
    {
        foreach (WordObject child in words)
        {
            if (child.wordText == wordText)
            {
                child.Destroy();
                WordSpawner.inst.lastNameWordCreated -= PhaseInfo.NameSpawnReduce(currentPhase);
                return;
            }
        }

        //Check edit distance
    }

    public void CreateTextInputField(TextInputField.Callback _enterCallback, Vector2 pos)
    {
        TextInputField temp = Instantiate(textInputField);
        temp.transform.position = pos;
        temp.SetCallback(_enterCallback);
        currentInput = temp;
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
        nameWords = new List<NameWord>();
        wordBackgrounds = new Sprite[4, 5];
        hopaeBackgrounds = new Sprite[6];
        stringWords = new List<string>[4];
        for (int i = 0; i < 4; i++) stringWords[i] = new List<string>();
        currentPhase = PhaseEnum.Start;
        ResourcesLoader.ParseCSVFile();
        ResourcesLoader.LoadImages();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateTextInputField(() => { RemoveWord(koreanInput); }, new Vector2(0, -4));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (brainWeight > maximumWeight && !isGameOverTimerOn) gameOverTimer = StartCoroutine(GameOverTimer(Time.time));
            playerTyping = totalTyping / Time.time * 60;

            //Need to be fixed when server is done, minPlayerTyping is set to 0 and maxPlayerTyping is your playerTyping.
            //playerTypingRate = (playerTyping - (MinPlayerTyping - currentPhase.rateArrangePoint)) / (MaxPlayerTyping - MinPlayerTyping + currentPhase.rateArrangePoint * 2);
            playerTypingRate = (playerTyping - (0 - PhaseInfo.RateArrangePoint(currentPhase))) / (playerTyping - 0 + PhaseInfo.RateArrangePoint(currentPhase) * 2);

        }
    }
}
