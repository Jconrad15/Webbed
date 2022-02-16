using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Node : MonoBehaviour
{
    private List<Node> connectedNodes;
    public int identifier;

    private Dictionary<int, Web> webs;

    public Action<int> cbNodeMoved;

    private Rigidbody2D rb;

    private float forceBounds = 0.2f;

    void OnEnable()
    {
        connectedNodes = new List<Node>();
        webs = new Dictionary<int, Web>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float xF = Random.Range(-forceBounds, forceBounds);
        float yF = Random.Range(-forceBounds, forceBounds);
        Vector2 force = new Vector2(xF, yF);

        rb.AddForce(force, ForceMode2D.Impulse);

        cbNodeMoved?.Invoke(identifier);
    }

    public Vector2 GetPosition()
    {
        return gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Exit if collision object is not a node
        if (collision.CompareTag("Node") == false) { return; }

        Node otherNode = collision.gameObject.GetComponent<Node>();

        // Return, if the other node is already connected
        if (connectedNodes.Contains(otherNode)) { return; }

        CreateWeb(otherNode);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Exit if collision object is not a node
        if (collision.CompareTag("Node") == false) { return; }

        Node otherNode = collision.gameObject.GetComponent<Node>();

        // Return, if the other node is not connected
        if (connectedNodes.Contains(otherNode) == false) { return; }

        TryRemoveWeb(otherNode);
    }

    private void CreateWeb(Node otherNode)
    {
        // Add other node to connected nodes list
        connectedNodes.Add(otherNode);

        // The node with the smaller identifier is the one that generates the web
        if (identifier < otherNode.identifier)
        {
            GameObject web_GO = new GameObject("line to node " + otherNode.identifier.ToString());
            web_GO.transform.SetParent(gameObject.transform);
            Web web = web_GO.AddComponent<Web>();

            web.Setup(this, otherNode);

            webs.Add(otherNode.identifier, web);
        }
    }

    private void TryRemoveWeb(Node otherNode)
    {
        _ = connectedNodes.Remove(otherNode);

        // If line_GO stored in association with other identifier
        if (webs.ContainsKey(otherNode.identifier))
        {
            // Delete web
            webs[otherNode.identifier].DestroyWeb();
            webs.Remove(otherNode.identifier);
        }
    }

    public void RegisterOnNodeMoved(Action<int> callbackFunc)
    {
        cbNodeMoved += callbackFunc;
    }

    public void UnregisterOnNodeMoved(Action<int> callbackFunc)
    {
        cbNodeMoved -= callbackFunc;
    }



}
