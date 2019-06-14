using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    bool state = false; // 투명화가 시작된 상태인가
    bool transforming; // 투명화가 진행중인 상태인가
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
        if (state == b) return;
        StartCoroutine(Transparent(b));
    }

    public IEnumerator Transparent(bool b)
    {
        state = b;
        transforming = b;
        if (transforming)
        {
            while (alpha > 0f)
            {
                Debug.Log("투명화");
                alpha -= 0.05f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return null;
            }            
        }
        else
        {
            while (alpha < 1f)
            {
                Debug.Log("반투명화");
                alpha += 0.05f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return null;
            }            
        }
        
    }
}
