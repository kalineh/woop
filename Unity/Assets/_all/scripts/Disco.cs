using UnityEngine;
using System.Collections.Generic;

public class Disco
	: MonoBehaviour
{
    public float Step = 1.0f;
    public float Height = 4.0f;
    public Vector3 GridSize = new Vector3(1.0f, 0.5f, 1.0f);
    public Vector3 HalfGridSize = Vector3.zero;

    public int GridCountX = 0;
    public int GridCountY = 0;
    public int GridCountHigh = 0;

    private int PlaneDefaultSize = 8;
    private List<GameObject> items;

    void Start()
    {
        HalfGridSize = GridSize * 0.5f;

        var size = Vector3.Scale(V3._111() * (float)PlaneDefaultSize, transform.localScale);
        var steps = Vector3.Scale(size, GridSize);

        GridCountX = (int)(steps.x / GridSize.x);
        GridCountY = (int)(steps.z / GridSize.z);
        GridCountHigh = (int)(steps.y / GridSize.y);

        items = new List<GameObject>(GridCountX * GridCountY);
    }

    public GameObject Get(int x, int y)
    {
        return items[x + y * GridCountX];
    }
}
