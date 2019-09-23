using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public WordObject wordObject;

    float lastWordCreatedTime;

    //Positions of initial word object, temporal value
    float minX = -3, maxX = 3, initialY = 5;

    int GetRandomGrade()
    {
        int randomGrade;
        float temp = Random.value;
        if (temp < PhaseInfo.GradeProb(WordSpace.inst.currentPhase, 0)) randomGrade = 3;
        else if (temp < PhaseInfo.GradeProb(WordSpace.inst.currentPhase, 1)) randomGrade = 2;
        else if (temp < PhaseInfo.GradeProb(WordSpace.inst.currentPhase, 2)) randomGrade = 1;
        else randomGrade = 0;
        return randomGrade;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(wordObject).Initiate(3, new Vector2(Random.Range(minX, maxX), initialY));
        Instantiate(wordObject).Initiate(3, new Vector2(Random.Range(minX, maxX), initialY));
        Instantiate(wordObject).Initiate(2, new Vector2(Random.Range(minX, maxX), initialY));
        Instantiate(wordObject).Initiate(2, new Vector2(Random.Range(minX, maxX), initialY));
        Instantiate(wordObject).Initiate(1, new Vector2(Random.Range(minX, maxX), initialY));
        lastWordCreatedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!WordSpace.inst.isGameOver)
        {
            if ((WordSpace.inst.words.Count < 5) || (Time.time - lastWordCreatedTime > PhaseInfo.WordSpawnDelay(WordSpace.inst.currentPhase)))
            {
                Instantiate(wordObject).Initiate(GetRandomGrade(), new Vector2(Random.Range(minX, maxX), initialY));
                lastWordCreatedTime = Time.time;
            }
        }
    }
}
