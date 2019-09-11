using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    public string wordText = null; //Text of this word object
    public int wordGrade, wordWeight;

    public TextMesh textMesh; //Text field(text mesh) of this word object


    public void Initiate(string _wordText, Vector2 pos)
    {
        wordText = _wordText;
        textMesh.text = wordText;
        transform.position = pos;
        float wordTyping = WordReader.GetWordTyping(wordText);
        wordGrade = 4 <= wordTyping && wordTyping < 7 ? 3 :
            7 <= wordTyping && wordTyping < 12 ? 2 :
            12 <= wordTyping && wordTyping < 17 ? 1 : 0;
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
