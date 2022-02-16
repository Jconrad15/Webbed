using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;

    private List<Node> nodes;

    private int nodeCount = 10;

    void OnEnable()
    {
        nodes = new List<Node>();

        for (int i = 0; i < nodeCount; i++)
        {
            float x = Random.Range(-8f, 8f);
            float y = Random.Range(-5f, 5f);

            Node node = CreateNode(x, y, i);

            nodes.Add(node);
        }
    }

    private Node CreateNode(float x, float y, int i)
    {
        Vector2 newPosition = new Vector2(x, y);

        GameObject node_GO = Instantiate(nodePrefab, transform);
        node_GO.transform.position = newPosition;

        Node node = node_GO.GetComponent<Node>();
        node.identifier = i;

        return node;
    }

}
