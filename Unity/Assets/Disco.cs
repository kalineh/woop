using UnityEngine;
using System.Collections.Generic;

public class Disco
	: MonoBehaviour
{
    public float Step = 1.0f;
    public float Height = 8.0f;
    public float GridSize = 1.0f;
    public float HalfGridSize = 0.5f;
    public Vector3 Up = Vector3.up;

    public int GridCountX = 0;
    public int GridCountY = 0;

    private int PlaneDefaultSize = 5;
    private List<GameObject> items;

    void Start()
    {
        var size = Vector3.Scale(V3._111() * (float)PlaneDefaultSize, transform.localScale);
        var steps = size / GridSize;

        GridCountX = (int)steps.x;
        GridCountY = (int)steps.y;

        items = new List<GameObject>(GridCountX * GridCountY);
    }

    GameObject Get(int x, int y)
    {
        return items[x + y * GridCountX];
    }
}
