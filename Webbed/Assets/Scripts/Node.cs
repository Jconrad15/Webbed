using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private List<Node> connectedNodes;
    public int identifier;

    private Dictionary<int, GameObject> lines;

    void OnEnable()
    {
        connectedNodes = new List<Node>();
        lines = new Dictionary<int, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public Vector2 GetPosition()
    {
        return gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");

        Node otherNode = collision.gameObject.GetComponent<Node>();

        // Return, if the other node is already connected
        if (connectedNodes.Contains(otherNode)) { return; }

        connectedNodes.Add(otherNode);

        CreateLine(otherNode);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Node otherNode = collision.gameObject.GetComponent<Node>();

        // Return, if the other node is not connected
        if (connectedNodes.Contains(otherNode) == false) { return; }

        connectedNodes.Remove(otherNode);

        TryRemoveLine(otherNode);
    }

    private void CreateLine(Node otherNode)
    {
        // The node with the smaller identifier is the one that tracks the lines
        if (identifier < otherNode.identifier)
        {
            GameObject line_GO = new GameObject("line to node " + otherNode.identifier.ToString());
            line_GO.transform.SetParent(gameObject.transform);
            LineRenderer lr = line_GO.AddComponent<LineRenderer>();

            Vector3[] positions = new Vector3[2];
            positions[0] = GetPosition();
            positions[1] = otherNode.GetPosition();

            lr.SetPositions(positions);

            lines.Add(otherNode.identifier, line_GO);
        }
    }

    private void TryRemoveLine(Node otherNode)
    {
        // If line_GO stored in association with other identifier
        if (lines.ContainsKey(otherNode.identifier))
        {
            // Delete line
            GameObject lineToDestroy = lines[otherNode.identifier];
            lines.Remove(otherNode.identifier);

            Destroy(lineToDestroy);
        }
    }

}
