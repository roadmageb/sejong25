using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public WordObject wordObject;

    //Positions of initial word object, temporal value
    float minX = -3, maxX = 3, initialY = 5;

    IEnumerator temp()
    {
        for(int i = 0; i < 70; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(wordObject).Initiate("쌀국수", new Vector2(Random.Range(minX, maxX), initialY));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(temp());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Instantiate(wordObject).Initiate("쌀국수", new Vector2(Random.Range(minX, maxX), initialY));
    }
}
