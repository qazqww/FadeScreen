using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    bool state;
    float alpha = 1f;
    int row;
    int col;
    Image image;

    public int Row
    {
        get { return row; }
    }
    public int Col
    {
        get { return col; }
    }

    public void SetNode(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetTransparent(bool b)
    {
        StartCoroutine(Transparent(b));
    }

    public IEnumerator Transparent(bool b)
    {
        state = b;
        if (state)
        {
            while (alpha > 0f)
            {
                Debug.Log("투명화");
                alpha -= 0.01f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return null;
            }            
        }
        else
        {
            while (alpha < 1f)
            {
                Debug.Log("반투명화");
                alpha += 0.01f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return null;
            }            
        }
        
    }
}
