﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    //Positions of initial word object, temporal value
    float minX = -3, maxX = 3, initialY = 5;
    public string wordText = null; //Text of this word object
    public int wordGrade;
    public float wordWeight;

    public TextMesh textMesh; //Text field(text mesh) of this word object

    /// <summary>
    /// Create word object by text
    /// </summary>
    /// <param name="_wordText">Text of the word object</param>
    /// <param name="pos">Initial position of the word object</param>
    public virtual void Initiate(string _wordText)
    {
        wordText = _wordText;
        wordGrade = WordProcessor.GetWordGrade(wordText);
        textMesh.text = wordText;
        transform.position = new Vector2(Random.Range(minX, maxX), initialY);
        wordWeight = wordGrade == 3 ? 3 : wordGrade == 2 ? 5 : wordGrade == 1 ? 7 : 10;
        wordWeight *= WordProcessor.GetWordTyping(PlayerData.hopaeName) <= 9 ? 1 : 1 + (WordProcessor.GetWordTyping(PlayerData.hopaeName) - 9) * 0.1f;
        GetComponent<SpriteRenderer>().sprite = WordSpace.inst.wordBackgrounds[wordGrade, wordText.Length - 2];
        gameObject.AddComponent<PolygonCollider2D>();
        WordSpace.inst.brainWeight += wordWeight;
        WordSpace.inst.words.Add(this);
    }

    public virtual void Destroy()
    {
        WordSpace.inst.totalTyping += (WordProcessor.GetWordTyping(wordText) + 1);
        WordSpace.inst.words.Remove(this);
        WordSpace.inst.brainWeight -= wordWeight;
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (transform.eulerAngles.z > 30 && transform.eulerAngles.z < 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 30);
        else if (transform.eulerAngles.z < 330 && transform.eulerAngles.z > 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -30);
    }
}
public class NormalWord : WordObject
{

}

public class NameWord : WordObject
{

    public override void Initiate(string _wordText)
    {
        base.Initiate(_wordText);
        wordWeight = 0;

    }
}