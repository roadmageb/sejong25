using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpace : SingletonBehaviour<WordSpace>
{
    public List<WordObject> words;
    public float brainWeight = 0; //Current weight of brain
    public int TotalWeight = 200; //Total amount of brain weight


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
