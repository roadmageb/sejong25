using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : SingletonBehaviour<WordSpawner>
{
    [Header("Prefabs")]
    public GameObject wordObject;

    public float lastNormalWordCreated, lastNameWordCreated;

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

    WordObject CreateNormal(int wordgrade)
    {
        NormalWord temp = Instantiate(wordObject).gameObject.AddComponent<NormalWord>();
        temp.Initiate(WordSpace.inst.stringWords[wordgrade][Random.Range(0, WordSpace.inst.stringWords[wordgrade].Count)]);
        return temp;
    }

    WordObject CreateName(string nameText)
    {
        NameWord temp = Instantiate(wordObject).gameObject.AddComponent<NameWord>();
        temp.Initiate(nameText);
        return temp;
    }

    WordObject CreateAttack(string attackText)
    {
        return null;
    }

    WordObject CreateItem(ItemType itemType)
    {
        return null;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        CreateNormal(3);
        CreateNormal(3);
        CreateNormal(2);
        CreateNormal(2);
        CreateNormal(1);
        lastNormalWordCreated = Time.time;
        lastNameWordCreated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!WordSpace.inst.isGameOver)
        {
            if ((WordSpace.inst.words.Count < 5) || (Time.time - lastNormalWordCreated > PhaseInfo.WordSpawnDelay(WordSpace.inst.currentPhase)))
            {
                CreateNormal(GetRandomGrade());
                CreateNormal(GetRandomGrade());
                CreateNormal(GetRandomGrade());
                CreateNormal(GetRandomGrade());
                CreateNormal(GetRandomGrade());
                CreateNormal(GetRandomGrade());
                lastNormalWordCreated = Time.time;
            }
            if (Time.time - lastNameWordCreated > PhaseInfo.NameSpawnDelay(WordSpace.inst.currentPhase))
            {
                //For test
                CreateName(GameData.hopaeName);
                lastNameWordCreated = Time.time;
            }
        }
    }
}
