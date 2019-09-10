using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpace : SingletonBehaviour<WordSpace>
{
    public List<WordObject> words;

    public List<string>[] stringWords = new List<string>[3];
    public float brainWeight = 0; //Current weight of brain
    public int TotalWeight = 200; //Total amount of brain weight
    public Sprite[,] wordBackgrounds; //Sprites of word background


    public void RemoveWord(string wordText)
    {
        foreach (WordObject child in words)
        {
            if (child.wordText == wordText)
            {
                words.Remove(child);
                brainWeight -= child.wordWeight;
                Destroy(child.gameObject);
                return;
            }
        }
    }

    void Awake()
    {
        words = new List<WordObject>();
        wordBackgrounds = new Sprite[4, 5];

        for (int grade = 0; grade < 4; grade++)
            for (int length = 0; length < 5; length++)
                wordBackgrounds[grade, length] = Resources.Load<Sprite>("WordBackgrounds/" + grade + "_" + (length + 2));
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
