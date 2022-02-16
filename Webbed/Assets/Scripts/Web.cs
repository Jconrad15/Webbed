using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    private Node nodeA;
    private Node nodeB;

    private LineRenderer lr;

    public void Setup(Node A, Node B)
    {
        lr = gameObject.AddComponent<LineRenderer>();

        nodeA = A;
        nodeB = B;

        nodeA.RegisterOnNodeMoved(OnNodeMoved);
        nodeB.RegisterOnNodeMoved(OnNodeMoved);

        SetLinePoint(A);
        SetLinePoint(B);
    }

    public void DestroyWeb()
    {
        nodeA.UnregisterOnNodeMoved(OnNodeMoved);
        nodeB.UnregisterOnNodeMoved(OnNodeMoved);

        Destroy(gameObject);
        Destroy(this);
    }

    private void OnNodeMoved(int identifier)
    {
        Node movedNode;

        if (identifier == nodeA.identifier)
        {
            movedNode = nodeA;
        }
        else if (identifier == nodeB.identifier)
        {
            movedNode = nodeB;
        }
        else
        {
            Debug.LogError("Web learned of node that it is not connected to.");
            return;
        }

        SetLinePoint(movedNode);

    }

    private void SetLinePoint(Node node)
    {
        // Node A is the first point
        // Node B is the second point
        int index;
        if (node == nodeA)
        {
            index = 0;
        }
        else if (node == nodeB)
        {
            index = 1;
        }
        else
        {
            Debug.LogError("SetLinePoint in Web learned of non-connected node.");
            return;
        }

        lr.SetPosition(index, node.GetPosition());
    }



}
