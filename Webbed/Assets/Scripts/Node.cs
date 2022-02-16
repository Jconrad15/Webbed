using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connectedNodes;

    void OnEnable()
    {
        connectedNodes = new List<Node>();
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Node otherNode = collision.gameObject.GetComponent<Node>();

        // Return, if the other node is not connected
        if (connectedNodes.Contains(otherNode) == false) { return; }

        connectedNodes.Remove(otherNode);
    }

}
