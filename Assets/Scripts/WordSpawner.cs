using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public WordObject wordObject;

    float wordSpawnDelay = 4, nameSpawnDelay = 5; //Words spawn delay
    float lastWordCreatedTime;

    //Positions of initial word object, temporal value
    float minX = -3, maxX = 3, initialY = 5;

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
            if ((WordSpace.inst.words.Count < 5) || (Time.time - lastWordCreatedTime > wordSpawnDelay))
            {
                Instantiate(wordObject).Initiate(3, new Vector2(Random.Range(minX, maxX), initialY));
                lastWordCreatedTime = Time.time;
            }
        }
    }
}
