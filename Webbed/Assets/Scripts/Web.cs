using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    private Node nodeA;
    private Node nodeB;

    private LineRenderer lr;

    private float startWidth;
    private float endWidth;
    private Color32 color = new Color32(173, 173, 184, 255);

    public void Setup(Node A, Node B)
    {
        lr = gameObject.AddComponent<LineRenderer>();
        
        startWidth = Random.Range(0.2f, 0.4f);
        endWidth = Random.Range(0.2f, 0.4f);
        SetLineProperties();

        nodeA = A;
        nodeB = B;

        nodeA.RegisterOnNodeMoved(OnNodeMoved);
        nodeB.RegisterOnNodeMoved(OnNodeMoved);

        SetLinePoint(A);
        SetLinePoint(B);
    }

    private void SetLineProperties()
    {
        // Color
        lr.startColor = color;
        lr.endColor = color;

        // Width
        lr.startWidth = startWidth;
        lr.endWidth = endWidth;

        // Material
        Material whiteDiffuseMat = new Material(Shader.Find("Sprites/Default"));
        lr.material = whiteDiffuseMat;
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
