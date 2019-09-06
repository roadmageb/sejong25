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
    }

    private void LateUpdate()
    {
        if (transform.eulerAngles.z > 30 && transform.eulerAngles.z < 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 30);
        else if (transform.eulerAngles.z < 330 && transform.eulerAngles.z > 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -30);
    }
}
