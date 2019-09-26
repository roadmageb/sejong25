using System.Collections;
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
        textMesh = transform.Find("WordText").GetComponent<TextMesh>();
        wordText = _wordText;
        wordGrade = WordProcessor.GetWordGrade(wordText);
        textMesh.text = wordText;
        transform.position = new Vector2(Random.Range(minX, maxX), initialY);
        wordWeight = wordGrade == 3 ? 3 : wordGrade == 2 ? 5 : wordGrade == 1 ? 7 : 10;
        wordWeight *= WordProcessor.GetWordTyping(GameData.hopaeName) <= 9 ? 1 : 1 + (WordProcessor.GetWordTyping(GameData.hopaeName) - 9) * 0.1f;
        WordSpace.inst.words.Add(this);
    }

    public virtual void Destroy()
    {
        WordSpace.inst.totalTyping += (WordProcessor.GetWordTyping(wordText) + 1);
        WordSpace.inst.words.Remove(this);
    }

    private void LateUpdate()
    {
        if (transform.eulerAngles.z > 30 && transform.eulerAngles.z < 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 30);
        else if (transform.eulerAngles.z < 330 && transform.eulerAngles.z > 180) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -30);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BrainBorder") { WordSpace.inst.borderTouchingWords++; }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BrainBorder") { WordSpace.inst.borderTouchingWords--; }

    }
}
public class NormalWord : WordObject
{
    public override void Initiate(string _wordText)
    {
        base.Initiate(_wordText);
        GetComponent<SpriteRenderer>().sprite = WordSpace.inst.wordBackgrounds[wordGrade, wordText.Length - 2];
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public override void Destroy()
    {
        base.Destroy();
        Destroy(gameObject);
    }
}

public class NameWord : WordObject
{

    public override void Initiate(string _wordText)
    {
        base.Initiate(_wordText);
        wordWeight = 0;
        GetComponent<SpriteRenderer>().sprite = WordSpace.inst.hopaeBackgrounds[wordText.Length - 2];
        gameObject.AddComponent<PolygonCollider2D>();
        textMesh.transform.position += new Vector3(0.1f, 0);
        textMesh.color = new Color(1, 1, 1);
    }

    public override void Destroy()
    {
        WordSpace.inst.nameWords.Add(this);
        GetComponent<PolygonCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        transform.position = new Vector3(-8, 2.5f - WordSpace.inst.nameWords.Count * 0.5f, 0);
        base.Destroy();
    }
}