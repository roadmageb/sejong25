using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    public string wordText = null; //Text of this word object
    public int wordGrade, wordWeight;

    public TextMesh textMesh; //Text field(text mesh) of this word object

    /// <summary>
    /// Create word object by word grade
    /// </summary>
    /// <param name="_wordGrade">Grade of the word object</param>
    /// <param name="pos">Initial position of the word object</param>
    public void Initiate(int _wordGrade, Vector2 pos)
    {
        Initiate(WordSpace.inst.stringWords[_wordGrade][Random.Range(0, WordSpace.inst.stringWords[_wordGrade].Count)], pos);
    }

    /// <summary>
    /// Create word object by text
    /// </summary>
    /// <param name="_wordText">Text of the word object</param>
    /// <param name="pos">Initial position of the word object</param>
    public void Initiate(string _wordText, Vector2 pos)
    {
        wordText = _wordText;
        wordGrade = WordReader.GetWordGrade(wordText);
        textMesh.text = wordText;
        transform.position = pos;
        wordWeight = wordGrade == 3 ? 3 : wordGrade == 2 ? 5 : wordGrade == 1 ? 7 : 10;
        GetComponent<SpriteRenderer>().sprite = WordSpace.inst.wordBackgrounds[wordGrade, wordText.Length - 2];
        gameObject.AddComponent<PolygonCollider2D>();
        WordSpace.inst.brainWeight += wordWeight;
        WordSpace.inst.words.Add(this);
    }

    private void LateUpdate()
    {
        if (transform.eulerAngles.z > 30 && transform.eulerAngles.z < 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 30);
        else if (transform.eulerAngles.z < 330 && transform.eulerAngles.z > 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -30);
    }
}
