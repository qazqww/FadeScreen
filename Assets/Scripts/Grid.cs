using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int nodeCount = 5;
    public Node nodePrefab;
    public Node[,] nodeArr;

    List<Node> nodes = new List<Node>();

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
            StartCoroutine(TransparentOne(true, nodeCount / 2, nodeCount / 2));
            StartCoroutine(TransparentNear(nodeArr[nodeCount / 2, nodeCount / 2]));
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
                node.transform.localPosition = new Vector3(col * 100, -row * 100, 0);
            }
    }

    IEnumerator TransparentOne(bool b, int row, int col)
    {
        nodeArr[row, col].SetTransparent(b);
        yield return null;
    }

    Node[] FindNear(Node node)
    {
        nodes.Clear();
        for (int row = -1; row <= 1; row++)
            for (int col = -1; col <= 1; col++)
            {
                if (node.Row + row < 0 || node.Row + row >= nodeCount ||
                        node.Col + col < 0 || node.Col + col >= nodeCount)
                    continue;
                if (row == 0 && col == 0)
                    continue;
                nodes.Add(nodeArr[node.Row + row, node.Col + col]);
            }
        return nodes.ToArray();
    }

    IEnumerator TransparentNear(Node node)
    {
        Node[] nodes = FindNear(node);

        for(int i=0; i<nodes.Length; i++)
        {
            Debug.Log(nodes[i]);
            nodes[i].SetTransparent(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
