using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int nodeCount = 5;
    public Node nodePrefab;
    public Node[,] nodeArr;

    Transform root;

    void Awake()
    {
        nodePrefab = Resources.Load<Node>("Node");
        root = transform.Find("Root");
        CreateGrid(nodeCount);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Transparent(true));
        else if (Input.GetKeyDown(KeyCode.T))
            StartCoroutine(Transparent(false));
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
                node.transform.localPosition = new Vector3(col * 100, -row * 100, 0 );
            }
    }

    IEnumerator Transparent(bool b)
    {
        for (int row = 0; row < nodeCount; ++row)
            for (int col = 0; col < nodeCount; ++col)
            {
                nodeArr[row, col].SetTransparent(b);
                yield return new WaitForSeconds(0.1f);
            }
    }
}
