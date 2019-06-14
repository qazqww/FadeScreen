using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int nodeCount = 5;
    public Node nodePrefab;
    public Node[,] nodeArr;

    List<Node> nodes = new List<Node>();
    List<Node> fadeOrder = new List<Node>();

    Transform root;

    void Awake()
    {
        nodePrefab = Resources.Load<Node>("Node");
        root = transform.Find("Root");
        CreateGrid(nodeCount);
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

    void CreateGrid(int nodeCount)
    {
        this.nodeCount = nodeCount;
        int count = 0;
        nodeArr = new Node[nodeCount, nodeCount];
        
        for (int row = 0; row < nodeCount; ++row)
            for (int col = 0; col < nodeCount; ++col)
            {
                Node node = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity, root);
                nodeArr[row, col] = node;
                node.name = "Node : " + count++;
                node.SetNode(row, col);
                node.transform.localPosition = new Vector3(col * 100 - (nodeCount - 1) * 50, -(row * 100 - (nodeCount - 1) * 50), 0);
            }
    }

    void TransparentOne(bool b, int row, int col)
    {
        nodeArr[row, col].SetTransparent(b);
    }

    void SetOrder()
    {
        fadeOrder.Clear();
        int center = nodeCount / 2;
        fadeOrder.Add(nodeArr[center, center]);
        for (int i = 1; i <= center; i++)
        {
            for (int row = center - i; row <= center + i; row++)
                for (int col = center - i; col <= center + i; col++)
                {
                    if(row == center - i || row == center + i)
                        fadeOrder.Add(nodeArr[row, col]);
                    else
                        if (col == center - i || col == center + i)
                            fadeOrder.Add(nodeArr[row, col]);
                }
        }
    }

    IEnumerator Execute(List<Node> nodes)
    {
        for(int i=0; i<nodes.Count; i++)
        {
            nodes[i].SetTransparent(true);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
