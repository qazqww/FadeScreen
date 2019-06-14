using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int rowCount = 5;
    public int colCount = 5;
    public Node nodePrefab;
    public Node[,] nodeArr;

    List<Node> nodes = new List<Node>();
    List<Node> fadeOrder = new List<Node>();

    Transform root;

    void Awake()
    {
        nodePrefab = Resources.Load<Node>("Node");
        root = transform.Find("Root");
        CreateGrid(rowCount, colCount);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //    StartCoroutine(Transparent(true));
        //else if (Input.GetKeyDown(KeyCode.T))
        //    StartCoroutine(Transparent(false));

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetOrder();
        }
        else if (Input.GetKey(KeyCode.T))
        {
            StartCoroutine(Execute(fadeOrder));
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < fadeOrder.Count; i++)
                Debug.Log(fadeOrder[i]);
        }
    }

    void CreateGrid(int rowCount, int colCount)
    {
        this.rowCount = rowCount;
        this.colCount = colCount;
        int count = 0;
        nodeArr = new Node[rowCount, colCount];
        
        for (int row = 0; row < rowCount; ++row)
            for (int col = 0; col < colCount; ++col)
            {
                Node node = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity, root);
                nodeArr[row, col] = node;
                node.name = "Node : " + count++;
                node.SetNode(row, col);
                node.transform.localPosition = new Vector3(col * 100 - (colCount - 1) * 50, -(row * 100 - (rowCount - 1) * 50), 0);
            }
    }

    void TransparentOne(bool b, int row, int col)
    {
        nodeArr[row, col].SetTransparent(b);
    }

    void SetOrder()
    {
        fadeOrder.Clear();
        int rowCenter = rowCount / 2;
        int colCenter = colCount / 2;
        fadeOrder.Add(nodeArr[rowCenter, colCenter]);
        for (int i = 1; i <= Mathf.Max(rowCenter, colCenter); i++)
        {
            for (int row = rowCenter - i; row <= rowCenter + i; row++)
                for (int col = colCenter - i; col <= colCenter + i; col++)
                {
                    if (row < 0 || row >= rowCount || col < 0 || col >= colCount)
                        continue;
                    if(row == rowCenter - i || row == rowCenter + i)
                        fadeOrder.Add(nodeArr[row, col]);
                    else
                        if (col == colCenter - i || col == colCenter + i)
                            fadeOrder.Add(nodeArr[row, col]);
                }
        }
    }

    IEnumerator Execute(List<Node> nodes)
    {
        for(int i=0; i<nodes.Count; i++)
        {
            nodes[i].SetTransparent(true);
            yield return new WaitForSeconds(0.001f);
        }
    }
}
